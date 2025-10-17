#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.AccountsModule.Commands;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Queries;
using AdminAssistant.WebAPI.v1.AccountsModule;
using Microsoft.AspNetCore.Mvc;
using MappingProfile = AdminAssistant.WebAPI.v1.MappingProfile;

namespace AdminAssistant.Test.WebAPI.v1.AccountsModule;

public sealed class BankController_Put_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status200OK_Given_AValidBank()
    {
        // Arrange
        var bank = Factory.Bank.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankUpdateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<Bank>.Success(bank)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        var container = services.BuildServiceProvider();
        var bankRequest = new BankUpdateRequestDto()
        {
            BankID = bank.BankID.Value,
            BankName = bank.BankName.Value
        };

        // Act
        var response = await container.GetRequiredService<BankController>().BankPut(bankRequest, default);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeAssignableTo<BankResponseDto>();

        var value = (BankResponseDto)result.Value!;
        value.BankID.ShouldBe(bank.BankID.Value);
        value.BankName.ShouldBe(bank.BankName.Value);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status404NotFound_Given_ANonExistentBankID()
    {
        // Arrange
        var bank = Factory.Bank.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankUpdateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<Bank>.NotFound()));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        var container = services.BuildServiceProvider();

        var mapper = container.GetRequiredService<IMapper>();
        var bankRequest = mapper.Map<BankUpdateRequestDto>(bank);

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankController>().BankPut(bankRequest, default);

        // Assert
        response.Result.ShouldBeOfType<NotFoundObjectResult>();
        response.Value.ShouldBeNull();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status422UnprocessableEntity_Given_AnInvalidBank()
    {
        // Arrange
        var validationErrors = new List<ValidationError>()
            {
                new ValidationError() { Identifier="ExampleErrorCode", ErrorMessage="ExampleErrorMessage", Severity=ValidationSeverity.Error },
                new ValidationError() { Identifier="ExampleErrorCode2", ErrorMessage="ExampleErrorMessage2", Severity=ValidationSeverity.Error }
            };
        var bank = Factory.Bank.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankUpdateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<Bank>.Invalid(validationErrors)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        var container = services.BuildServiceProvider();

        var mapper = container.GetRequiredService<IMapper>();
        var bankRequest = mapper.Map<BankUpdateRequestDto>(bank);

        // Act
        var response = await container.GetRequiredService<BankController>().BankPut(bankRequest, default);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<UnprocessableEntityObjectResult>();

        var result = (UnprocessableEntityObjectResult)response.Result!;
        result.Value.ShouldNotBeNull();
        var errors = (SerializableError)result.Value!;

        foreach (var expectedErrorDetails in validationErrors)
        {
            var messages = (string[])errors[expectedErrorDetails.Identifier];
            messages.ShouldContain(expectedErrorDetails.ErrorMessage);
        }
    }
}

public class BankController_BankPost_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status201CreatedAtRoute_Given_AValidBank()
    {
        // Arrange
        var bank = Factory.Bank.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankCreateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<Bank>.Success(bank)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        var container = services.BuildServiceProvider();

        var bankRequest = new BankCreateRequestDto()
        {
            BankName = bank.BankName.Value
        };

        // Act
        var response = await container.GetRequiredService<BankController>().BankPost(bankRequest, default);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<CreatedAtRouteResult>();

        var result = (CreatedAtRouteResult)response.Result!;
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeAssignableTo<BankResponseDto>();

        var value = (BankResponseDto)result.Value!;
        value.BankID.ShouldBe(bank.BankID.Value);
        value.BankName.ShouldBe(bank.BankName.Value);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status422UnprocessableEntity_Given_AnInvalidBank()
    {
        // Arrange
        var validationErrors = new List<ValidationError>()
            {
                new ValidationError() { Identifier="ExampleErrorCode", ErrorMessage="ExampleErrorMessage", Severity=ValidationSeverity.Error },
                new ValidationError() { Identifier="ExampleErrorCode2", ErrorMessage="ExampleErrorMessage2", Severity=ValidationSeverity.Error }
            };
        var bank = Factory.Bank.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankCreateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<Bank>.Invalid(validationErrors)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        var container = services.BuildServiceProvider();

        var mapper = container.GetRequiredService<IMapper>();
        var bankRequest = mapper.Map<BankCreateRequestDto>(bank);

        // Act
        var response = await container.GetRequiredService<BankController>().BankPost(bankRequest, default);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<UnprocessableEntityObjectResult>();

        var result = (UnprocessableEntityObjectResult)response.Result!;
        result.Value.ShouldNotBeNull();
        var errors = (SerializableError)result.Value!;

        foreach (var expectedErrorDetails in validationErrors)
        {
            var messages = (string[])errors[expectedErrorDetails.Identifier];
            messages.ShouldContain(expectedErrorDetails.ErrorMessage);
        }
    }
}

public class BankController_BankGetById_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Ok200Bank_With_ABank_Given_AnExistingBankID()
    {
        // Arrange
        var bank = Factory.Bank.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankByIDQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<Bank>.Success(bank)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankController>().BankGetById(bank.BankID.Value, default);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.ShouldBeAssignableTo<BankResponseDto>();

        result.Value.ShouldNotBeNull();
        var value = (BankResponseDto)result.Value!;
        value.BankID.ShouldBe(bank.BankID.Value);
        value.BankName.ShouldBe(bank.BankName.Value);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status404NotFound_Given_ANonExistentBankID()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankByIDQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<Bank>.NotFound()));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankController>().BankGetById(10, default);

        // Assert
        response.Result.ShouldBeOfType<NotFoundResult>();
        response.Value.ShouldBeNull();
    }
}

public class BankController_BankGet_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status200Ok_WithAListOfBank_Given_NoArguments()
    {
        // Arrange
        var banks = new List<Bank>()
            {
                Factory.Bank.WithTestData(10).WithBankName("Acme Bank PLC").Build(),
                Factory.Bank.WithTestData(20).WithBankName("Acme Building Society").Build()
            };

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<IEnumerable<Bank>>.Success(banks)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankController>().BankGet(default);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.ShouldBeAssignableTo<IEnumerable<BankResponseDto>>();

        result.Value.ShouldNotBeNull();
        var value = ((IEnumerable<BankResponseDto>)result.Value!).ToArray();
        value.Length.ShouldBe(banks.Count);

        var expected = banks.ToArray();
        for (var index = 0; index < expected.Length; index++)
        {
            value[index].BankID.ShouldBe(expected[index].BankID.Value);
            value[index].BankName.ShouldBe(expected[index].BankName.Value);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
