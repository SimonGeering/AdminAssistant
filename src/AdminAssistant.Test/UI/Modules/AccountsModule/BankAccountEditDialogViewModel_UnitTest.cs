#pragma warning disable CA1707 // Identifiers should not contain underscores
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
using Moq;
using System.Threading;

namespace AdminAssistant.UI.Modules.AccountsModule.BankAccountEditDialog
{
    public class BankAccountEditDialogViewModel_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Have_All_Lookups_Populated_Given_TheScreenHasLoaded()
        {
            // Arrange
            var mockHttpClientProvider = new Mock<IHttpClientProvider>();

            mockHttpClientProvider
                .Setup(x => x.GetFromJsonAsync<BankAccountType[]>("api/v1/BankAccountType", It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new BankAccountType[]
                {
                    Factory.BankAccountType.WithTestData().Build()
                }));

            mockHttpClientProvider
                .Setup(x => x.GetFromJsonAsync<Currency[]>("api/v1/Currency", It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Currency[]
                {
                    Factory.Currency.WithTestData().Build()
                }));

            var services = new ServiceCollection();
            services.AddMocksOfExternalDependencies();
            services.AddClientFrameworkServices();
            services.AddAdminAssistantClientSideDomainModel();
            services.AddAdminAssistantUI();
            services.AddTransient((sp) => mockHttpClientProvider.Object);

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
            services.AddClientFrameworkServices();
            services.AddAdminAssistantClientSideDomainModel();
            services.AddAdminAssistantUI();

            var container = services.BuildServiceProvider();

            // Act
            var vm = container.GetRequiredService<IBankAccountEditDialogViewModel>();
            container.GetRequiredService<IAccountsStateStore>().OnEditAccount(Factory.BankAccount.WithTestData(bankAccountID: 20).Build());

            // Assert
            vm.HeaderText.Should().Be(IBankAccountEditDialogViewModel.EditBankAccountHeader);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Have_ACreateHeader_GivenANewBankAccount()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMocksOfExternalDependencies();
            services.AddClientFrameworkServices();
            services.AddAdminAssistantClientSideDomainModel();
            services.AddAdminAssistantUI();

            var container = services.BuildServiceProvider();

            // Act
            var vm = container.GetRequiredService<IBankAccountEditDialogViewModel>();
            container.GetRequiredService<IAccountsStateStore>().OnEditAccount(Factory.BankAccount.WithTestData(bankAccountID: Constants.NewRecordID).Build());

            // Assert
            vm.HeaderText.Should().Be(IBankAccountEditDialogViewModel.NewBankAccountHeader);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
