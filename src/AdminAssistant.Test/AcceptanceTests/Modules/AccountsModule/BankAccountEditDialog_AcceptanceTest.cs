#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.UI.Modules.AccountsModule;
using AdminAssistant.UI.Modules.AccountsModule.BankAccountEditDialog;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

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

            var vm = this.Container.GetService<IBankAccountEditDialogViewModel>();
            await vm.OnInitializedAsync().ConfigureAwait(false);

            var accountsStateStore = this.Container.GetService<IAccountsStateStore>();

            // Act
            accountsStateStore.OnEditAccount(new BankAccount());

            // Assert
            vm.Currencies.Should().NotBeEmpty();
            vm.BankAccountTypes.Should().NotBeEmpty();

            var savedBankAccounts = await this.Container.GetService<IBankAccountRepository>().GetListAsync().ConfigureAwait(false);
            savedBankAccounts.Should().BeEmpty();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task CloseWithoutSaving_WhenCancelButtonIsClicked()
        {
            await this.ResetDatabaseAsync().ConfigureAwait(false);

            var vm = this.Container.GetService<IBankAccountEditDialogViewModel>();
            await vm.OnInitializedAsync().ConfigureAwait(false);

            var accountsStateStore = this.Container.GetService<IAccountsStateStore>();
            accountsStateStore.OnEditAccount(new BankAccount());

            // Act
            vm.OnCancelButtonClick();

            // Assert
            vm.ShowDialog.Should().BeFalse();

            var savedBankAccounts = await this.Container.GetService<IBankAccountRepository>().GetListAsync().ConfigureAwait(false);
            savedBankAccounts.Should().BeEmpty();
        }

        [Fact(Skip="WIP")]
        [Trait("Category", "Integration")]
        public async Task OnlyEnableSave_WhenNoValidationErrorsShown()
        {
            // Arrange
            await this.ResetDatabaseAsync().ConfigureAwait(false);

            var vm = this.Container.GetService<IBankAccountEditDialogViewModel>();
            await vm.OnInitializedAsync().ConfigureAwait(false);

            var accountsStateStore = this.Container.GetService<IAccountsStateStore>();

            // Act
            accountsStateStore.OnEditAccount(new BankAccount());

            // Assert
            //vm.AccountNameValidationMessage
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
