// ReSharper disable InconsistentNaming
#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.Infrastructure.EntityFramework;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Test;

public class ServiceCollection_Should()
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task BeAbleToInstantiateAllRegisteredServerSideTypes()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantServerSideProviders();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();
        services.AddAdminAssistantServerSideInfra();
        // Mocks ...
        services.AddMocksOfExternalServerSideDependencies();
        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestDb"));
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        var serviceProvider = services.BuildServiceProvider();

        // Act
        var result = new List<object>();

        using var scope = serviceProvider.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        foreach (var serviceDescriptor in services)
        {
            Guard.Against.Null(serviceDescriptor.ServiceType);
            Guard.Against.NullOrEmpty(serviceDescriptor.ServiceType.FullName);

            try
            {
                if (serviceDescriptor.ServiceType.FullName.Contains("MediatR", StringComparison.InvariantCulture)) //||
                    continue;

                var instance = scopedProvider.GetRequiredService(serviceDescriptor.ServiceType);
                instance.ShouldNotBeNull();
                instance.ShouldBeAssignableTo(serviceDescriptor.ServiceType);
                result.Add(instance);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to instantiate '{serviceDescriptor.ServiceType.FullName}'", ex);
            }
        }

        // Assert
        var expectedInstanceCountLessExclusions = services.Count(x => x.ServiceType.FullName?.Contains("MediatR", StringComparison.InvariantCulture) == false);
        result.Count.ShouldBe(expectedInstanceCountLessExclusions);
        await Task.CompletedTask;
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task BeAbleToInstantiateAllRegisteredClientSideTypes()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideProviders();
        services.AddAdminAssistantClientSideDomainModel();
        services.AddAdminAssistantUI();
        services.AddAdminAssistantApiClient();
        // Mocks ...
        services.AddMockClientSideLogging();

        var serviceProvider = services.BuildServiceProvider();

        // Act
        var result = new List<object>();

        foreach (var serviceDescriptor in services)
        {
            Guard.Against.Null(serviceDescriptor.ServiceType);
            Guard.Against.NullOrEmpty(serviceDescriptor.ServiceType.FullName);

            try
            {
                // HACK:
                if (serviceDescriptor.ServiceType.FullName.Contains("IOptions", StringComparison.InvariantCulture) ||
                    serviceDescriptor.ServiceType.FullName.Contains("ILogger", StringComparison.InvariantCulture) ||
                    serviceDescriptor.ServiceType.FullName.Contains("DefaultTypedHttpClientFactory", StringComparison.InvariantCulture) ||
                    serviceDescriptor.ServiceType.FullName.Contains("ITypedHttpClientFactory", StringComparison.InvariantCulture))
                    continue;

                var instance = serviceProvider.GetRequiredService(serviceDescriptor.ServiceType);
                instance.ShouldNotBeNull();
                instance.ShouldBeAssignableTo(serviceDescriptor.ServiceType);
                result.Add(instance);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to instantiate '{serviceDescriptor.ServiceType.FullName}'", ex);
            }
        }

        // Assert
        var expectedInstanceCountLessExclusions = services.Count(x => x.ServiceType.FullName?.Contains("IOptions", StringComparison.InvariantCulture) == false &&
                                                                      x.ServiceType.FullName?.Contains("ILogger", StringComparison.InvariantCulture) == false &&
                                                                      x.ServiceType.FullName?.Contains("DefaultTypedHttpClientFactory", StringComparison.InvariantCulture) == false &&
                                                                      x.ServiceType.FullName?.Contains("ITypedHttpClientFactory", StringComparison.InvariantCulture) == false) ;
        result.Count.ShouldBe(expectedInstanceCountLessExclusions);
        await Task.CompletedTask;
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
