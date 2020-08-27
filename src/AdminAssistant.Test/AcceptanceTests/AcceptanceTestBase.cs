#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
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
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
