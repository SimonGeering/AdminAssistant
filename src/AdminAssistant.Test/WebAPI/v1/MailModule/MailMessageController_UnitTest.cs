#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.MailModule;
using AdminAssistant.DomainModel.Modules.MailModule.CQRS;
using Ardalis.Result;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant.WebAPI.v1.MailModule
{
    public class MailMessageController_GetMailMessages
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Returns_Status200OK_With_AListOfMailMessages_Given_NoArguments()
        {
            // Arrange
            var documents = new List<MailMessage>()
            {
                Factory.MailMessage.WithTestData(10).Build(),
                Factory.MailMessage.WithTestData(20).Build()
            };

            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAutoMapper(typeof(MappingProfile));

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<MailMessageQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<IEnumerable<MailMessage>>.Success(documents)));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<MailMessageController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<MailMessageController>().GetMailMessages().ConfigureAwait(false);

            // Assert
            response.Value.Should().BeNull();
            response.Result.Should().NotBeNull();
            response.Result.Should().BeOfType<OkObjectResult>();

            var result = (OkObjectResult)response.Result!;
            result.Value.Should().BeAssignableTo<IEnumerable<MailMessageResponseDto>>();

            //var value = ((IEnumerable<CurrencyResponseDto>)result.Value).ToArray();
            //value.Should().HaveCount(currencies.Count);

            //var expected = currencies.ToArray();
            //for (int i = 0; i < expected.Length; i++)
            //{
            //    value[i].CurrencyID.Should().Be(expected[i].CurrencyID);
            //    value[i].Symbol.Should().Be(expected[i].Symbol);
            //    value[i].DecimalFormat.Should().Be(expected[i].DecimalFormat);
            //}
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
