// #pragma warning disable CA1707 // Identifiers should not contain underscores
// using AdminAssistant.Domain;
// using AdminAssistant.Modules.DocumentsModule;
// using AdminAssistant.Modules.DocumentsModule.Queries;
// using AdminAssistant.WebAPI.v1.DocumentsModule;
// using Microsoft.AspNetCore.Mvc;
// using MappingProfile = AdminAssistant.WebAPI.v1.MappingProfile;
//
// namespace AdminAssistant.Test.WebAPI.v1.DocumentsModule;
//
// public sealed class DocumentController_UnitTest_Should
// {
//     [Fact]
//     [Trait("Category", "Unit")]
//     public async Task Return_Status200OK_With_AListOfDocuments_Given_NoArguments()
//     {
//         // Arrange
//         var documents = new List<Document>()
//             {
//                 Factory.Document.WithTestData(10).Build(),
//                 Factory.Document.WithTestData(20).Build()
//             };
//
//         var services = new ServiceCollection();
//         services.AddMockServerSideLogging();
//         services.AddAutoMapper(typeof(MappingProfile));
//
//         var mockMediator = new Mock<IMediator>();
//         mockMediator.Setup(x => x.Send(It.IsAny<DocumentQuery>(), It.IsAny<CancellationToken>()))
//                     .Returns(Task.FromResult(Result<IEnumerable<Document>>.Success(documents)));
//
//         services.AddTransient((sp) => mockMediator.Object);
//         services.AddTransient<AdminAssistant.WebAPI.v1.DocumentsModule.DocumentController>();
//
//         // Act
//         var response = await services.BuildServiceProvider().GetRequiredService<DocumentController>().GetDocuments(default);
//
//         // Assert
//         response.Value.Should().BeNull();
//         response.Result.Should().NotBeNull();
//         response.Result.Should().BeOfType<OkObjectResult>();
//
//         var result = (OkObjectResult)response.Result!;
//         result.Value.Should().BeAssignableTo<IEnumerable<DocumentResponseDto>>();
//
//         #pragma warning disable S125 // Sections of code should not be commented out
//         //var value = ((IEnumerable<CurrencyResponseDto>)result.Value).ToArray();
//         //value.Should().HaveCount(currencies.Count);
//
//         //var expected = currencies.ToArray();
//         //for (int i = 0; i < expected.Length; i++)
//         //{
//         //    value[i].CurrencyID.Should().Be(expected[i].CurrencyID);
//         //    value[i].Symbol.Should().Be(expected[i].Symbol);
//         //    value[i].DecimalFormat.Should().Be(expected[i].DecimalFormat);
//         //}
//         #pragma warning restore S125 // Sections of code should not be commented out
//     }
// }
// #pragma warning restore CA1707 // Identifiers should not contain underscores
