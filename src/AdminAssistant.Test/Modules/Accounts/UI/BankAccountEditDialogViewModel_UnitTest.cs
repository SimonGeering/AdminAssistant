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
using AdminAssistant.Framework.Providers;
using Moq;
using System.Threading;

namespace AdminAssistant.Accounts.Modules.Accounts.UI
{
    public class BankAccountEditDialogViewModel_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Have_All_Lookups_Populated_Given_TheScreenHasLoaded()
        {
            // Arrange
            var mockHttpClientJsonProvider = new Mock<IHttpClientJsonProvider>();

            mockHttpClientJsonProvider
                .Setup(x => x.GetFromJsonAsync<BankAccountType[]>("api/v1/BankAccountType", It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new BankAccountType[]
                {
                    TestData.BankAccountTypeBuilder.WithTestData().Build()
                }));

            mockHttpClientJsonProvider
                .Setup(x => x.GetFromJsonAsync<Currency[]>("api/v1/Currency", It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Currency[]
                {
                    TestData.CurrencyBuilder.WithTestData().Build()
                }));

            var services = new ServiceCollection();
            services.AddMocksOfExternalDependencies();
            services.AddAdminAssistantClientServices();
            services.AddTransient((sp) => mockHttpClientJsonProvider.Object);

            // Act
            var vm = services.BuildServiceProvider().GetRequiredService<IBankAccountEditDialogViewModel>();
            await vm.OnInitializedAsync().ConfigureAwait(false);

            // Assert
            vm.BankAccountTypes.Should().NotBeNullOrEmpty();
            vm.Currencies.Should().NotBeNullOrEmpty();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Have_AnEditHeader_GivenAnExistingValidBankAccount()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMocksOfExternalDependencies();
            services.AddAdminAssistantClientServices();

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
            services.AddMocksOfExternalDependencies();
            services.AddAdminAssistantClientServices();

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
