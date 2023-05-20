#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
using System.Reflection;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.DAL.EntityFramework.Model;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Core;
using AdminAssistant.Infra.Providers;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AdminAssistant.Test;

[Collection("SequentialDBBackedTests")]
public abstract class IntegrationTestBase : IDisposable
{
    private readonly IHost _testServer;
    private readonly Respawn.Checkpoint _checkpoint;
    private readonly string _connectionString;
    private readonly HttpClient _httpClient;

    protected IntegrationTestBase()
    {
        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
#if DEBUG
                    logging.AddConsole();
                logging.AddDebug();

                logging.AddFilter("Default", LogLevel.Information)
                        .AddFilter(ILoggingProvider.ServerSideLogCategory, LogLevel.Debug)
                        .AddFilter("Microsoft", LogLevel.Warning)
                        .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);
#else
                    logging.AddFilter("Default", LogLevel.Warning)
                           .AddFilter(Framework.Providers.ILoggingProvider.ServerSideLogCategory, LogLevel.Warning)
                           .AddFilter("Microsoft", LogLevel.Warning)
                           .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);

                    // TODO: Configure production logging.
#endif
                })
            .ConfigureAppConfiguration((hostingContext, configBuilder) =>
            {
                configBuilder.AddUserSecrets(Assembly.GetExecutingAssembly());

                // Get config from UserSecrets so we can refer to it when setting up Test config setting overrides below ...
                var baseConfig = configBuilder.Build();

                // Switch out the DB for a Test DB by convention - assumes a '_TestDB' suffix to prod DB name ...
                var configSettings = baseConfig.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>() ?? throw new NullReferenceException("Failed to load configuration settings");
                var connectionStringFromUserSecrets = configSettings.ConnectionString;
                // TODO: Update this to use connection string builder so it is not hard coded to assume Application Name from config.
                var testConnectionString = connectionStringFromUserSecrets.Replace("Initial Catalog=AdminAssistant", "Initial Catalog=AdminAssistant_Test", StringComparison.InvariantCulture);

                configBuilder.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    [$"{nameof(ConfigurationSettings)}:{nameof(ConfigurationSettings.ConnectionString)}"] = testConnectionString
                });
            })
            .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseStartup<Blazor.Server.Startup>();
                 webBuilder.ConfigureTestServices(ConfigureTestServices());
                 webBuilder.UseTestServer();
             });

        _testServer = hostBuilder.Start();

        // TODO: Update this to use connection string builder so it is not hard coded to assume Application Name from config.
        _connectionString = _testServer.Services.GetRequiredService<IApplicationDbContext>().ConnectionString.Replace("Application Name=AdminAssistant;", "Application Name=AdminAssistant_TestDBReset", StringComparison.InvariantCulture);

        _checkpoint = new Respawn.Checkpoint
        {
            // Ignore system tables and anything that was populated by the EF seed data...
            TablesToIgnore = new[]
            {
                "sysdiagrams",
                "__EFMigrationsHistory",
                "tblObjectType"
            },
            WithReseed = true
        };

        Container = _testServer.Services;
        _httpClient = _testServer.GetTestClient();
    }

    protected IServiceProvider Container { get; }
    protected List<BankAccountTypeEntity> BankAccountTypes { get; private set; } = new List<BankAccountTypeEntity>();
    protected List<CurrencyEntity> Currencies { get; private set; } = new List<CurrencyEntity>();
    protected UserProfileEntity UserProfile { get; private set; } = new UserProfileEntity();
    protected OwnerEntity CompanyOwner { get; private set; } = new OwnerEntity();
    protected OwnerEntity PersonalOwner { get; private set; } = new OwnerEntity();

    protected async Task ResetDatabaseAsync()
    {
        await _checkpoint.Reset(_connectionString).ConfigureAwait(false);

        // Test Seed data ...
        var dbContext = Container.GetRequiredService<IApplicationDbContext>();
        var dateTimeProvider = Container.GetRequiredService<IDateTimeProvider>();

        BankAccountTypes = await SeedBankAccountTypeTestData(dbContext);
        Currencies = await SeedCurrencyTestData(dbContext);
        UserProfile = await SeedUserProfileTestData(dbContext, dateTimeProvider).ConfigureAwait(false);
        CompanyOwner = await SeedCompanyOwnerTestData(dbContext, dateTimeProvider, UserProfile).ConfigureAwait(false);
        PersonalOwner = await SeedPersonalOwnerTestData(dbContext, dateTimeProvider, UserProfile).ConfigureAwait(false);
    }

    protected virtual Action<IServiceCollection> ConfigureTestServices() => services =>
    {
            // Register the WebAPIClient using the test httpClient ...
            services.AddTransient<IAdminAssistantWebAPIClient>((sp) =>
        {
            Guard.Against.Null(_httpClient.BaseAddress, "httpClient.BaseAddress");
            return new AdminAssistantWebAPIClient(_httpClient) { BaseUrl = _httpClient.BaseAddress.ToString() };
        });
        services.AddAutoMapper(typeof(MappingProfile));
    };

    private async Task<List<BankAccountTypeEntity>> SeedBankAccountTypeTestData(IApplicationDbContext dbContext)
    {
        var testBankAccountTypes = AccountsSchema.GetBankAccountTypesSeedData();
        dbContext.BankAccountTypes.AddRange(testBankAccountTypes);
        await dbContext.SaveChangesAsync().ConfigureAwait(false);
        return testBankAccountTypes.ToList();
    }

    private async Task<List<CurrencyEntity>> SeedCurrencyTestData(IApplicationDbContext dbContext)
    {
        var testCurrencies = CoreSchema.GetCurrencySeedData();
        dbContext.Currencies.AddRange(testCurrencies);
        await dbContext.SaveChangesAsync().ConfigureAwait(false);
        return testCurrencies.ToList();
    }

    private async Task<UserProfileEntity> SeedUserProfileTestData(IApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        var testUserProfile = new UserProfileEntity()
        {
            SignOn = "TestUser",
            Audit = GetAuditForCreate(dateTimeProvider)
        };
        dbContext.UserProfiles.Add(testUserProfile);

        await dbContext.SaveChangesAsync().ConfigureAwait(false);
        return testUserProfile;
    }

    private async Task<OwnerEntity> SeedCompanyOwnerTestData(IApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider, UserProfileEntity testUserProfile)
    {
        var testCompanyOwner = new OwnerEntity()
        {
            Company = new CompanyEntity()
            {
                Name = "ACME Corporation",
                CompanyNumber = "12345678910",
                VATNumber = "zz1224324543",
                DateOfIncorporation = DateTime.Today,
                Audit = GetAuditForCreate(dateTimeProvider),
                UserProfile = testUserProfile
            }
        };
        dbContext.Owners.Add(testCompanyOwner);
        await dbContext.SaveChangesAsync().ConfigureAwait(false);
        return testCompanyOwner;
    }

    private async Task<OwnerEntity> SeedPersonalOwnerTestData(IApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider, UserProfileEntity testUserProfile)
    {
        var testPersonalOwner = new OwnerEntity()
        {
            PersonalDetails = new PersonalDetailsEntity()
            {
                Audit = GetAuditForCreate(dateTimeProvider),
                UserProfile = testUserProfile
            }
        };
        dbContext.Owners.Add(testPersonalOwner);
        await dbContext.SaveChangesAsync().ConfigureAwait(false);
        return testPersonalOwner;
    }

    private AuditEntity GetAuditForCreate(IDateTimeProvider dateTimeProvider)
        => new() { CreatedBy = "TestUser", CreatedOn = dateTimeProvider.UtcNow };

    #region IDisposable

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // Clean up DB if tests failed ...
                ResetDatabaseAsync().ConfigureAwait(false);

                // dispose managed state (managed objects)
                _testServer.Dispose();
                _httpClient.Dispose();
            }

            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
            disposedValue = true;
        }
    }

#pragma warning disable S125 // Sections of code should not be commented out
    // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    //~IntegrationTestBase()
    //{
    //    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //    Dispose(disposing: false);
    //}
#pragma warning restore S125 // Sections of code should not be commented out

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion // IDisposable
}
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
