#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Shared;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using Ardalis.GuardClauses;

namespace AdminAssistant.Test;

public class ServiceCollection_Should
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
        services.AddAdminAssistantServerSideInfra(new ConfigurationSettings() { ConnectionString = "FakeConnectionString", DatabaseProvider = "SQLServerLocalDB" });

        var serviceProvider = services.BuildServiceProvider();

        // Act
        var result = new List<object>();

        foreach (var serviceDescriptor in services)
        {
            Guard.Against.Null(serviceDescriptor.ServiceType);
            Guard.Against.NullOrEmpty(serviceDescriptor.ServiceType.FullName);

            try
            {
                if (serviceDescriptor.ServiceType.FullName.Contains("MediatR", StringComparison.InvariantCulture))
                    continue;

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
