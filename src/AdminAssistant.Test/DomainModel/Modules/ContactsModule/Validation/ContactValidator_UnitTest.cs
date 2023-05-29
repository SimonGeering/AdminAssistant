#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.ContactsModule;
using AdminAssistant.DomainModel.Modules.ContactsModule.Validation;
using FluentValidation;

namespace AdminAssistant.Test.DomainModel.Modules.ContactsModule.Validation;

public sealed class ContactValidator_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_IsValid_GivenAValidContact()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var contact = Factory.Contact.WithTestData(20)
                                     .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IContactValidator>().ValidateAsync(contact).ConfigureAwait(false);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenAContactWithAnEmptyFirstName()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var contact = Factory.Contact.WithTestData(20)
                                     .WithFirstName(string.Empty)
                                     .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IContactValidator>().ValidateAsync(contact).ConfigureAwait(false);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEmptyValidator" && x.PropertyName == nameof(Contact.FirstName));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenAContact_WithAFirstName_LongerThanMaxLength()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var contact = Factory.Contact.WithTestData(20)
                                     .WithFirstName(new string('x', Contact.FirstNameMaxLength + 1))
                                     .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IContactValidator>().ValidateAsync(contact).ConfigureAwait(false);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "MaximumLengthValidator" && x.PropertyName == nameof(Contact.FirstName));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenAContactWithAnEmptyLastName()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var contact = Factory.Contact.WithTestData(20)
                                     .WithLastName(string.Empty)
                                     .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IContactValidator>().ValidateAsync(contact).ConfigureAwait(false);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEmptyValidator" && x.PropertyName == nameof(Contact.LastName));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenAContact_WithALastName_LongerThanMaxLength()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var contact = Factory.Contact.WithTestData(20)
                                     .WithLastName(new string('x', Contact.LastNameMaxLength + 1))
                                     .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IContactValidator>().ValidateAsync(contact).ConfigureAwait(false);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "MaximumLengthValidator" && x.PropertyName == nameof(Contact.LastName));
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
