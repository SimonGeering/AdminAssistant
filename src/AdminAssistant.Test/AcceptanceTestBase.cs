// #if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
//
#pragma warning disable S125
// namespace AdminAssistant.Test;
#pragma warning restore S125
//
// [Collection("SequentialDBBackedTests")]
// public abstract class AcceptanceTestBase : IntegrationTestBase
// {
//     protected override Action<IServiceCollection> ConfigureTestServices() => services =>
//     {
//         base.ConfigureTestServices().Invoke(services);
//
//         services.AddAdminAssistantClientSideProviders();
//         services.AddAdminAssistantClientSideDomainModel();
//         services.AddAdminAssistantUI();
//     };
// }
// #endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
