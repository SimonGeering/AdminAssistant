#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.UI.Modules.AccountsModule;
using Microsoft.Toolkit.Mvvm.Messaging;
using static AdminAssistant.Test.TestConstants;

namespace AdminAssistant.Test.UI.Modules.AccountsModule;

[Collection("SequentialDBBackedTests")]
public sealed class BankAccountEditDialog_Should : AcceptanceTestBase
{
    [Fact]
    [Trait("Category", "Acceptance")]
    public async Task ShowAnNewBankAccount_WhenOpenedForCreate()
    {
        // Arrange
        await ResetDatabaseAsync().ConfigureAwait(false);

        var vm = Container.GetRequiredService<IBankAccountEditDialogViewModel>();
        await vm.OnInitializedAsync().ConfigureAwait(false);

        var messenger = Container.GetRequiredService<IMessenger>();

        // Act
        messenger.Send(new EditBankAccountMessage(new BankAccount()));

        // Assert
        vm.BankAccountID.Should().Be(Constants.NewRecordID);

        vm.BankAccountTypeID.Should().Be(Constants.UnknownRecordID);
        vm.BankAccountTypes.Should().NotBeEmpty();

        vm.CurrencyID.Should().Be(Constants.UnknownRecordID);
        vm.Currencies.Should().NotBeEmpty();

        vm.AccountName.Should().BeEmpty();
        vm.IsBudgeted.Should().BeFalse();
        vm.OpeningBalance.Should().Be(Zero);
        vm.CurrentBalance.Should().Be(Zero);
        vm.OpenedOn.Should().Be(default);

        vm.HeaderText.Should().Be(IBankAccountEditDialogViewModel.NewBankAccountHeader);
        vm.ShowDialog.Should().BeTrue();

        var savedBankAccounts = await Container.GetRequiredService<IBankAccountRepository>().GetListAsync().ConfigureAwait(false);
        savedBankAccounts.Should().BeEmpty();
    }

    [Fact]
    [Trait("Category", "Acceptance")]
    public async Task CloseWithoutSaving_WhenCancelButtonIsClicked()
    {
        await ResetDatabaseAsync().ConfigureAwait(false);

        var vm = Container.GetRequiredService<IBankAccountEditDialogViewModel>();
        await vm.OnInitializedAsync().ConfigureAwait(false);

        var messenger = Container.GetRequiredService<IMessenger>();
        messenger.Send(new EditBankAccountMessage(new BankAccount()));

        // Act
        await vm.Cancel.ExecuteAsync(null).ConfigureAwait(true);

        // Assert
        vm.ShowDialog.Should().BeFalse();

        var savedBankAccounts = await Container.GetRequiredService<IBankAccountRepository>().GetListAsync().ConfigureAwait(false);
        savedBankAccounts.Should().BeEmpty();
    }

    #pragma warning disable S125 // Sections of code should not be commented out
    //[Fact(Skip="WIP")]
    //[Trait("Category", "Integration")]
    //public async Task OnlyEnableSave_WhenNoValidationErrorsShown()
    //{
    //    await this.ResetDatabaseAsync().ConfigureAwait(false);

    //    var vm = this.Container.GetRequiredService<IBankAccountEditDialogViewModel>();
    //    await vm.OnInitializedAsync().ConfigureAwait(false);

    //    var messenger = this.Container.GetRequiredService<IMessenger>();
    //    messenger.Send(new EditBankAccountMessage(new BankAccount()));

    //    // Act
    //    await vm.Cancel.ExecuteAsync(null).ConfigureAwait(true);

    //    // Assert
    //    //vm.AccountNameValidationMessage
    //}
    #pragma warning restore S125 // Sections of code should not be commented out
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
