#pragma warning disable S125
// #if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
// #pragma warning disable CA1707 // Identifiers should not contain underscores
//
// using AdminAssistant.Infrastructure.EntityFramework.Model.Core;
// using AdminAssistant.Modules.CoreModule;
// using AdminAssistant.Modules.CoreModule.Infrastructure.DAL;
// using AdminAssistant.UI.Shared.WebAPIClient.v1;
//
// namespace AdminAssistant.Test.WebAPI.v1.CoreModule;
//
// [Collection("SequentialDBBackedTests")]
// public sealed class Currency_Post_Should : IntegrationTestBase
// {
//     [Fact]
//     [Trait("Category", "Integration")]
//     public async Task Return_ANewlyCreatedCurrency_Given_AValidCurrency()
//     {
//         // Arrange
//         await ResetDatabaseAsync();
//
//         var request = new CurrencyCreateRequestDto() { DecimalFormat = "0.00", Symbol = "Moo" };
//
//         // Act
//         var response = await Container.GetRequiredService<IAdminAssistantWebAPIClient>().PostCurrencyAsync(request);
//
//         // Assert
//         response.CurrencyID.Should().BeGreaterThan(0);
//         response.DecimalFormat.Should().Be(request.DecimalFormat);
//         response.Symbol.Should().Be(request.Symbol);
//     }
// }
//
// [Collection("SequentialDBBackedTests")]
// public sealed class Currency_Put_Should : IntegrationTestBase
// {
//     [Fact]
//     [Trait("Category", "Integration")]
//     public async Task Return_ANewlyUpdatedCurrency_Given_AValidExistingCurrency()
//     {
//         // Arrange
//         await ResetDatabaseAsync();
//
//         var dal = Container.GetRequiredService<ICurrencyRepository>();
//         var aud = await dal.SaveAsync(new Currency() { DecimalFormat = CoreSchema.DefaultCurrencyDecimalFormat, Symbol = "AUD" }, default);
//
//         var request = new CurrencyUpdateRequestDto()
//         {
//             CurrencyID = aud.CurrencyID.Value,
//             DecimalFormat = "0.0",
//             Symbol = aud.Symbol
//         };
//
//         // Act
//         var response = await Container.GetRequiredService<IAdminAssistantWebAPIClient>().PutCurrencyAsync(request);
//
//         // Assert
//         response.CurrencyID.Should().Be(request.CurrencyID);
//         response.DecimalFormat.Should().Be(request.DecimalFormat);
//         response.Symbol.Should().Be(request.Symbol);
//     }
// }
//
// [Collection("SequentialDBBackedTests")]
// public class Currency_GetById_Should : IntegrationTestBase
// {
//     [Fact]
//     [Trait("Category", "Integration")]
//     public async Task Return_ACurrency_Given_CurrencyID()
//     {
//         // Arrange
//         await ResetDatabaseAsync();
//
//         var dal = Container.GetRequiredService<ICurrencyRepository>();
//         var aud = await dal.SaveAsync(new Currency() { DecimalFormat = CoreSchema.DefaultCurrencyDecimalFormat, Symbol = "AUD" }, default);
//
//         // Act
//         var response = await Container.GetRequiredService<IAdminAssistantWebAPIClient>().GetCurrencyByIdAsync(aud.CurrencyID.Value);
//
//         // Assert
//         response.CurrencyID.Should().Be(aud.CurrencyID.Value);
//         response.DecimalFormat.Should().Be(aud.DecimalFormat);
//         response.Symbol.Should().Be(aud.Symbol);
//     }
// }
//
// [Collection("SequentialDBBackedTests")]
// public class Currency_Get_Should : IntegrationTestBase
// {
//     [Fact]
//     [Trait("Category", "Integration")]
//     public async Task Return_AllCurrencies_Given_NoParameters()
//     {
//         // Arrange
//         await ResetDatabaseAsync();
//
//         // Act
//         var response = await Container.GetRequiredService<IAdminAssistantWebAPIClient>().GetCurrencyAsync();
//
//         // Assert
//         response.Should().HaveCount(3);
//         response.Should().ContainSingle(x => x.Symbol == "GBP");
//         response.Should().ContainSingle(x => x.Symbol == "EUR");
//         response.Should().ContainSingle(x => x.Symbol == "USD");
//     }
// }
// #pragma warning restore CA1707 // Identifiers should not contain underscores
// #endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
#pragma warning restore S125
