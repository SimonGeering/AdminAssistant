#pragma warning disable CA1707 // Identifiers should not contain underscores
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdminAssistant.Framework
{
    public class ServiceCollection_Should
    {
        [SkippableFact]
        [Trait("Category", "Unit")]
        public async Task BeAbleToInstantiateAllRegisteredClientSideTypes()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMocksOfExternalClientSideDependencies();
            services.AddClientFrameworkServices();

            var serviceProvider = services.BuildServiceProvider();

            // Act
            var result = new List<object>();

            foreach (var serviceDescriptor in services)
            {
                var instance = serviceProvider.GetService(serviceDescriptor.ServiceType);
                instance.Should().NotBeNull();
                instance.Should().BeAssignableTo(serviceDescriptor.ServiceType);
                result.Add(instance);
            }

            // Assert
            result.Should().HaveCount(services.Count);
            await Task.CompletedTask.ConfigureAwait(false);
        }

        [SkippableFact]
        [Trait("Category", "Unit")]
        public async Task BeAbleToInstantiateAllRegisteredServerSideTypes()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMocksOfExternalServerSideDependencies();
            services.AddServerFrameworkServices();

            var serviceProvider = services.BuildServiceProvider();

            // Act
            var result = new List<object>();

            foreach (var serviceDescriptor in services)
            {
                Guard.Against.Null(serviceDescriptor.ServiceType, "serviceDescriptor.ServiceType");
                Guard.Against.NullOrEmpty(serviceDescriptor.ServiceType.FullName, "serviceDescriptor.ServiceType.FullName");

                try
                {
                    if (serviceDescriptor.ServiceType.FullName.Contains("MediatR", StringComparison.InvariantCulture))
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
            var expectedInstanceCountLessExclusions = services.Count(x => x.ServiceType.FullName?.Contains("MediatR", StringComparison.InvariantCulture) == false);
            result.Should().HaveCount(expectedInstanceCountLessExclusions);
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
