#pragma warning disable CA1707 // Identifiers should not contain underscores
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant
{
    public class ServiceCollection_Should
    {
        [Trait("Category", "Unit")]
        public async Task BeAbleToInstantiateAllRegisteredServerSideTypes()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            var services = new ServiceCollection();
            services.AddMocksOfExternalDependencies();
            services.AddAdminAssistantServerServices(mockConfiguration.Object);

            // Mock any dependent services provided by the IWebHostBuilder ...
            // While this limits the scope of the test to AdminAssistant dependencies,
            // it is a compromise vs Spinning up TestHosts etc.
            services.AddTransient(x => mockConfiguration.Object);

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
            result.Should().HaveCount(services.Count);
            await Task.CompletedTask.ConfigureAwait(false);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task BeAbleToInstantiateAllRegisteredClientSideTypes()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMocksOfExternalDependencies();
            services.AddAdminAssistantClientServices();

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
