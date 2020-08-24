using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdminAssistant.AcceptanceTests
{
    [Collection("SequentialDBBackedTests")]
    public abstract class AcceptanceTestBase : IntegrationTestBase
    {
        protected override Action<IServiceCollection> ConfigureTestServices() => services =>
        {
            base.ConfigureTestServices().Invoke(services);

            services.AddAdminAssistantClientSideProviders();
            services.AddAdminAssistantClientSideDomainModel();
            services.AddAdminAssistantUI();
        };
    }
}
