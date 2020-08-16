#pragma warning disable CA1707 // Identifiers should not contain underscores
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Infrastructure;
using Ardalis.GuardClauses;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant
{
    public class ServiceCollection_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task BeAbleToInstantiateAllRegisteredServerSideTypes()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMocksOfExternalServerSideDependencies();

            services.AddServerFrameworkServices();
            services.AddAdminAssistantServerSideDomainModel();
            services.AddAdminAssistantServerSideDAL(new ConfigurationSettings() { ConnectionString= "FakeConnectionString", DatabaseProvider = "SQLServerLocalDB" });

            var serviceProvider = services.BuildServiceProvider();

            // Act
            var result = new List<object>();

            foreach (var serviceDescriptor in services)
            {
                Guard.Against.Null(serviceDescriptor.ServiceType, "serviceDescriptor.ServiceType");
                Guard.Against.NullOrEmpty(serviceDescriptor.ServiceType.FullName, "serviceDescriptor.ServiceType.FullName");

                try
                {
                    if (serviceDescriptor.ServiceType.FullName.StartsWith("MediatR", StringComparison.InvariantCulture))
                        continue;

                    var instance = serviceProvider.GetService(serviceDescriptor.ServiceType);
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
            var expectedInstanceCountLessExclusions = services.Count(x => x.ServiceType.FullName?.StartsWith("MediatR", StringComparison.InvariantCulture) == false);
            result.Should().HaveCount(expectedInstanceCountLessExclusions);
            await Task.CompletedTask.ConfigureAwait(false);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task BeAbleToInstantiateAllRegisteredClientSideTypes()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMocksOfExternalClientSideDependencies();
            services.AddTransient((sp) => new Mock<UI.Shared.WebAPIClient.v1.IAdminAssistantWebAPIClient>().Object);

            services.AddClientFrameworkServices();
            services.AddAdminAssistantClientSideDomainModel();
            services.AddAdminAssistantUI();

            var serviceProvider = services.BuildServiceProvider();

            // Act
            var result = new List<object>();

            foreach (var serviceDescriptor in services)
            {
                try
                {
                    var instance = serviceProvider.GetService(serviceDescriptor.ServiceType);
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
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
