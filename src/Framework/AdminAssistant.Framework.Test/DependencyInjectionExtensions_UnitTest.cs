#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdminAssistant.Framework
{
    public class ServiceCollection_Should
    {
        [SkippableFact]
        [Trait("Category", "Unit")]
        public async Task BeAbleToInstantiateAllRegisteredTypes()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddFrameworkServices();

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
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
