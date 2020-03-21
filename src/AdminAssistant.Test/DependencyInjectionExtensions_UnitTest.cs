#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.Blazor.Client;
using FluentAssertions;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdminAssistant
{
    public class ServiceCollection_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task BeAbleToInstantiateAllRegisteredClientSideTypes()
        {
            // Arrange
            var builder = WebAssemblyHostBuilder.CreateDefault(null);
            builder.Services.Clear(); // Don't want to test blazor framework stuff.
            builder.ConfigureServices();
           
            var serviceProvider = await Task.Run(() => builder.Services.BuildServiceProvider()).ConfigureAwait(false);

            // Act
            var result = new List<object>();

            foreach (var serviceDescriptor in builder.Services)
            {                
                var instance = serviceProvider.GetService(serviceDescriptor.ServiceType);
                instance.Should().NotBeNull();
                instance.Should().BeAssignableTo(serviceDescriptor.ServiceType);
                result.Add(instance);
            }

            // Assert
            result.Should().HaveCount(builder.Services.Count);
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
