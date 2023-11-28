#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using AdminAssistant.WebAPI.v1;
using AdminAssistant.WebAPI.v1.AccountsModule;
using Microsoft.AspNetCore.Mvc;

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
                    .Returns(Task.FromResult(Result<Bank>.Success(bank)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        var container = services.BuildServiceProvider();
        var bankRequest = new BankUpdateRequestDto()
        {
            BankID = bank.BankID.Value,
            BankName = bank.BankName
        };

        // Act
        var response = await container.GetRequiredService<BankController>().BankPut(bankRequest, default);

        // Assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.Should().NotBeNull();
        result.Value.Should().BeAssignableTo<BankResponseDto>();

        var value = (BankResponseDto)result.Value!;
        value.BankID.Should().Be(bank.BankID.Value);
        value.BankName.Should().Be(bank.BankName);
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
                    .Returns(Task.FromResult(Result<Bank>.NotFound()));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        var container = services.BuildServiceProvider();

        var mapper = container.GetRequiredService<IMapper>();
        var bankRequest = mapper.Map<BankUpdateRequestDto>(bank);

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankController>().BankPut(bankRequest, default);

        // Assert
        response.Result.Should().BeOfType<NotFoundObjectResult>();
        response.Value.Should().BeNull();
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
                    .Returns(Task.FromResult(Result<Bank>.Invalid(validationErrors)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        var container = services.BuildServiceProvider();

        var mapper = container.GetRequiredService<IMapper>();
        var bankRequest = mapper.Map<BankUpdateRequestDto>(bank);

        // Act
        var response = await container.GetRequiredService<BankController>().BankPut(bankRequest, default);

        // Assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<UnprocessableEntityObjectResult>();

        var result = (UnprocessableEntityObjectResult)response.Result!;
        result.Value.Should().NotBeNull();
        var errors = (SerializableError)result.Value!;

        foreach (var expectedErrorDetails in validationErrors)
        {
            var messages = (string[])errors[expectedErrorDetails.Identifier];
            messages.Should().Contain(expectedErrorDetails.ErrorMessage);
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
                    .Returns(Task.FromResult(Result<Bank>.Success(bank)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        var container = services.BuildServiceProvider();

        var bankRequest = new BankCreateRequestDto()
        {
            BankName = bank.BankName
        };

        // Act
        var response = await container.GetRequiredService<BankController>().BankPost(bankRequest, default);

        // Assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<CreatedAtRouteResult>();

        var result = (CreatedAtRouteResult)response.Result!;
        result.Value.Should().NotBeNull();
        result.Value.Should().BeAssignableTo<BankResponseDto>();

        var value = (BankResponseDto)result.Value!;
        value.BankID.Should().Be(bank.BankID.Value);
        value.BankName.Should().Be(bank.BankName);
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
                    .Returns(Task.FromResult(Result<Bank>.Invalid(validationErrors)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        var container = services.BuildServiceProvider();

        var mapper = container.GetRequiredService<IMapper>();
        var bankRequest = mapper.Map<BankCreateRequestDto>(bank);

        // Act
        var response = await container.GetRequiredService<BankController>().BankPost(bankRequest, default);

        // Assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<UnprocessableEntityObjectResult>();

        var result = (UnprocessableEntityObjectResult)response.Result!;
        result.Value.Should().NotBeNull();
        var errors = (SerializableError)result.Value!;

        foreach (var expectedErrorDetails in validationErrors)
        {
            var messages = (string[])errors[expectedErrorDetails.Identifier];
            messages.Should().Contain(expectedErrorDetails.ErrorMessage);
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
                    .Returns(Task.FromResult(Result<Bank>.Success(bank)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankController>().BankGetById(bank.BankID.Value, default);

        // Assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.Should().BeAssignableTo<BankResponseDto>();

        result.Value.Should().NotBeNull();
        var value = (BankResponseDto)result.Value!;
        value.BankID.Should().Be(bank.BankID.Value);
        value.BankName.Should().Be(bank.BankName);
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
                    .Returns(Task.FromResult(Result<Bank>.NotFound()));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankController>().BankGetById(10, default);

        // Assert
        response.Result.Should().BeOfType<NotFoundResult>();
        response.Value.Should().BeNull();
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
                    .Returns(Task.FromResult(Result<IEnumerable<Bank>>.Success(banks)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankController>().BankGet(default);

        // Assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.Should().BeAssignableTo<IEnumerable<BankResponseDto>>();

        result.Value.Should().NotBeNull();
        var value = ((IEnumerable<BankResponseDto>)result.Value!).ToArray();
        value.Should().HaveCount(banks.Count);

        var expected = banks.ToArray();
        for (var index = 0; index < expected.Length; index++)
        {
            value[index].BankID.Should().Be(expected[index].BankID.Value);
            value[index].BankName.Should().Be(expected[index].BankName);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
