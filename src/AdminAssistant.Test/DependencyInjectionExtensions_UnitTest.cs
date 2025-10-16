#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Shared;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace AdminAssistant.Test;

public class ServiceCollection_Should(ITestOutputHelper output)
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task BeAbleToInstantiateAllRegisteredServerSideTypes()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();
        services.AddAdminAssistantServerSideProviders();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();
        services.AddAdminAssistantServerSideInfra();

        foreach (var s in services.Where(s => s.ServiceType == typeof(ApplicationDbContext)))
        {
            output.WriteLine($"Registered: {s.ImplementationType?.Name ?? "Factory"} for {s.ServiceType.Name}");
        }
        foreach (var s in services.Where(s => s.ServiceType == typeof(ApplicationDbContext)))
        {
            output.WriteLine($"[DI DEBUG] Lifetime: {s.Lifetime}, ImplType: {s.ImplementationType?.FullName ?? "Factory"}, ImplFactory: {(s.ImplementationFactory != null ? "Yes" : "No")}");
        }

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
                if (serviceDescriptor.ServiceType.FullName.Contains("MediatR", StringComparison.InvariantCulture) ||
                    typeof(DbContext).IsAssignableFrom(serviceDescriptor.ServiceType))
                    continue;

                var instance = scopedProvider.GetRequiredService(serviceDescriptor.ServiceType);
                instance.Should().NotBeNull();
                instance.Should().BeAssignableTo(serviceDescriptor.ServiceType);
                result.Add(instance);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to instantiate '{serviceDescriptor.ServiceType.FullName}'", ex);
            }
        }

        // Assert
        var expectedInstanceCountLessExclusions = services.Count(x => x.ServiceType.FullName?.Contains("MediatR", StringComparison.InvariantCulture) == false);
        result.Should().HaveCount(expectedInstanceCountLessExclusions);
        await Task.CompletedTask;
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task BeAbleToInstantiateAllRegisteredClientSideTypes()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMocksOfExternalClientSideDependencies();
        services.AddTransient((sp) => new Mock<IAdminAssistantWebAPIClient>().Object);

        services.AddAdminAssistantClientSideProviders();
        services.AddAdminAssistantClientSideDomainModel();
        services.AddAdminAssistantUI();

        var serviceProvider = services.BuildServiceProvider();

        // Act
        var result = new List<object>();

        foreach (var serviceDescriptor in services)
        {
            try
            {
                var instance = serviceProvider.GetRequiredService(serviceDescriptor.ServiceType);
                instance.Should().NotBeNull();
                instance.Should().BeAssignableTo(serviceDescriptor.ServiceType);
                result.Add(instance);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to instantiate '{serviceDescriptor.ServiceType.FullName}'", ex);
            }
        }

        // Assert
        result.Should().HaveCount(services.Count);
        await Task.CompletedTask;
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
