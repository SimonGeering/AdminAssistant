#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.UI.Modules.AccountsModule;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using Xunit;
using static AdminAssistant.TestConstants;

namespace AdminAssistant.AcceptanceTests.Modules.AccountsModule
{
    [Collection("SequentialDBBackedTests")]
    public class BankAccountEditDialog_Should : AcceptanceTestBase
    {
        [Fact]
        [Trait("Category", "Integration")]
        public async Task ShowAnNewBankAccount_WhenOpenedForCreate()
        {
            // Arrange
            await this.ResetDatabaseAsync().ConfigureAwait(false);

            var vm = this.Container.GetRequiredService<IBankAccountEditDialogViewModel>();
            await vm.OnInitializedAsync().ConfigureAwait(false);

            var messenger = this.Container.GetRequiredService<IMessenger>();

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

            var savedBankAccounts = await this.Container.GetRequiredService<IBankAccountRepository>().GetListAsync().ConfigureAwait(false);
            savedBankAccounts.Should().BeEmpty();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task CloseWithoutSaving_WhenCancelButtonIsClicked()
        {
            await this.ResetDatabaseAsync().ConfigureAwait(false);

            var vm = this.Container.GetRequiredService<IBankAccountEditDialogViewModel>();
            await vm.OnInitializedAsync().ConfigureAwait(false);

            var messenger = this.Container.GetRequiredService<IMessenger>();
            messenger.Send(new EditBankAccountMessage(new BankAccount()));

            // Act
            await vm.Cancel.ExecuteAsync(null).ConfigureAwait(true);

            // Assert
            vm.ShowDialog.Should().BeFalse();

            var savedBankAccounts = await this.Container.GetRequiredService<IBankAccountRepository>().GetListAsync().ConfigureAwait(false);
            savedBankAccounts.Should().BeEmpty();
        }

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
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
