#pragma warning disable CA1707 // Identifiers should not contain underscores
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.UI.Modules.Accounts.BankAccountEditDialog;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AdminAssistant.UI.Modules.Accounts;

namespace AdminAssistant.Accounts.Modules.Accounts.UI
{
    public class BankAccountEditDialogViewModel_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void Have_AnEditHeader_GivenAnExistingValidBankAccount()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAdminAssistantUI();
            services.AddAdminAssistantClientSideDomainModel();
            var container = services.BuildServiceProvider();

            // Act
            var vm = container.GetRequiredService<IBankAccountEditDialogViewModel>();
            container.GetRequiredService<IAccountsStateStore>().OnEditAccount(TestData.BankAccountBuilder.WithTestData(bankAccountID: 20).Build());

            // Assert
            vm.HeaderText.Should().Be(BankAccountEditDialogViewModel.EditBankAccountHeader);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Have_ACreateHeader_GivenANewBankAccount()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAdminAssistantUI();
            services.AddAdminAssistantClientSideDomainModel();
            var container = services.BuildServiceProvider();

            // Act
            var vm = container.GetRequiredService<IBankAccountEditDialogViewModel>();
            container.GetRequiredService<IAccountsStateStore>().OnEditAccount(TestData.BankAccountBuilder.WithTestData(bankAccountID: Constants.NewRecordID).Build());

            // Assert
            vm.HeaderText.Should().Be(BankAccountEditDialogViewModel.NewBankAccountHeader);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
