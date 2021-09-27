#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Diagnostics.CodeAnalysis;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Core;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.DocumentsModule;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Documents;

namespace AdminAssistant.Infra.DAL;

public class DALMappingProfile_Should
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public DALMappingProfile_Should()
    {
        _configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

        _mapper = _configuration.CreateMapper();
    }

    [Fact]
    [Trait("Category", "Unit")]
    [SuppressMessage("Style", "IDE0022:Use expression body for methods", Justification = "One line test")]
    public void HaveValidConfiguration()
    {
        // Arrange

        // Act
        _configuration.AssertConfigurationIsValid();

        // Assert
    }

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData(typeof(BankEntity), typeof(Bank))]
    [InlineData(typeof(BankAccountEntity), typeof(BankAccount))]
    [InlineData(typeof(BankAccountTypeEntity), typeof(BankAccountType))]
    [InlineData(typeof(BankAccountTransactionEntity), typeof(BankAccountTransaction))]
    [InlineData(typeof(Bank), typeof(BankEntity))]
    [InlineData(typeof(BankAccount), typeof(BankAccountEntity))]
    [InlineData(typeof(BankAccountType), typeof(BankAccountTypeEntity))]
    [InlineData(typeof(BankAccountTransaction), typeof(BankAccountTransactionEntity))]
    public void ShouldSupportAccountsSchemaMappingFromSourceToDestination(Type source, Type destination)
    {
        // Arrange
        var instance = Activator.CreateInstance(source);

        // Act
        var result = _mapper.Map(instance, source, destination);

        // Assert
        result.Should().NotBeNull();
    }

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData(typeof(DocumentEntity), typeof(Document))]
    [InlineData(typeof(Document), typeof(DocumentEntity))]
    public void ShouldSupportDocumentsSchemaMappingFromSourceToDestination(Type source, Type destination)
    {
        // Arrange
        var instance = Activator.CreateInstance(source);

        // Act
        var result = _mapper.Map(instance, source, destination);

        // Assert
        result.Should().NotBeNull();
    }

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData(typeof(CurrencyEntity), typeof(Currency))]
    [InlineData(typeof(Currency), typeof(CurrencyEntity))]
    public void ShouldSupportCoreSchemaMappingFromSourceToDestination(Type source, Type destination)
    {
        // Arrange
        var instance = Activator.CreateInstance(source);

        // Act
        var result = _mapper.Map(instance, source, destination);

        // Assert
        result.Should().NotBeNull();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
