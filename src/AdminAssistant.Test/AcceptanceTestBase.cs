#pragma warning disable S125
// #if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
//
// namespace AdminAssistant.Test;
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
#pragma warning restore S125
