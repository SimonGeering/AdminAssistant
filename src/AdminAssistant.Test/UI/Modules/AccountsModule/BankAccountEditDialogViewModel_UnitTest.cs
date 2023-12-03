#pragma warning disable S125 // Sections of code should not be commented out
//#pragma warning disable CA1707 // Identifiers should not contain underscores
//using AdminAssistant.DomainModel.Modules.AccountsModule;
//using AdminAssistant.Framework.Providers;
//using AdminAssistant.WebAPI.Client.v1;

//namespace AdminAssistant.Test.UI.Modules.AccountsModule.BankAccountEditDialog
//{
//    public sealed class BankAccountEditDialogViewModel_Should
//    {
//        [Fact]
//        [Trait("Category", "Unit")]
//        public async Task Have_All_Lookups_Populated_Given_TheScreenHasLoaded()
//        {
//            // Arrange
//            var mockHttpClientProvider = new Mock<IAdminAssistantWebAPIClient>();

//            mockHttpClientProvider
//                .Setup(x => x.GetBankAccountTypeAsync())
//                .Returns(Task.FromResult(new BankAccountType[]
//                {
//                    Factory.BankAccountType.WithTestData().Build()
//                }));

//            mockHttpClientProvider
//                .Setup(x => x.GetFromJsonAsync<Currency[]>(Constants.AdminAssistantWebAPI, "api/v1/Currency", It.IsAny<CancellationToken>()))
//                .Returns(Task.FromResult(new Currency[]
//                {
//                    Factory.Currency.WithTestData().Build()
//                }));

//            var services = new ServiceCollection();
//            services.AddMocksOfExternalClientSideDependencies();
//            services.AddClientFrameworkServices();
//            services.AddAdminAssistantClientSideDomainModel();
//            services.AddAdminAssistantUI();
//            services.AddTransient((sp) => mockHttpClientProvider.Object);

//            // Act
//            var vm = services.BuildServiceProvider().GetRequiredService<IBankAccountEditDialogViewModel>();
//            await vm.OnInitializedAsync().ConfigureAwait(false);

//            // Assert
//            vm.BankAccountTypes.Should().NotBeNullOrEmpty();
//            vm.Currencies.Should().NotBeNullOrEmpty();
//        }

//        [Fact]
//        [Trait("Category", "Unit")]
//        public void Have_AnEditHeader_GivenAnExistingValidBankAccount()
//        {
//            // Arrange
//            var services = new ServiceCollection();
//            services.AddMocksOfExternalClientSideDependencies();
//            services.AddClientFrameworkServices();
//            services.AddAdminAssistantClientSideDomainModel();
//            services.AddAdminAssistantUI();

//            var container = services.BuildServiceProvider();

//            // Act
//            var vm = container.GetRequiredService<IBankAccountEditDialogViewModel>();
//            container.GetRequiredService<IAccountsStateStore>().OnEditAccount(Factory.BankAccount.WithTestData(bankAccountID: 20).Build());

//            // Assert
//            vm.HeaderText.Should().Be(IBankAccountEditDialogViewModel.EditBankAccountHeader);
//        }

//        [Fact]
//        [Trait("Category", "Unit")]
//        public void Have_ACreateHeader_GivenANewBankAccount()
//        {
//            // Arrange
//            var services = new ServiceCollection();
//            services.AddMocksOfExternalClientSideDependencies();
//            services.AddClientFrameworkServices();
//            services.AddAdminAssistantClientSideDomainModel();
//            services.AddAdminAssistantUI();

//            var container = services.BuildServiceProvider();

//            // Act
//            var vm = container.GetRequiredService<IBankAccountEditDialogViewModel>();
//            container.GetRequiredService<IAccountsStateStore>().OnEditAccount(Factory.BankAccount.WithTestData(bankAccountID: Constants.NewRecordID).Build());

//            // Assert
//            vm.HeaderText.Should().Be(IBankAccountEditDialogViewModel.NewBankAccountHeader);
//        }
//    }
//}
//#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore S125 // Sections of code should not be commented out
