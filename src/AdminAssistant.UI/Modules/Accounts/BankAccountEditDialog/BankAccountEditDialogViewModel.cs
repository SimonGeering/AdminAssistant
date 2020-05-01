using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.DomainModel.Modules.Accounts.Validation;
using Ardalis.GuardClauses;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountEditDialog
{
    public class BankAccountEditDialogViewModel : ViewModelBase, IBankAccountEditDialogViewModel
    {
        public const string NewBankAccountHeader = "New bank account";
        public const string EditBankAccountHeader = "Edit bank account";

        private readonly HttpClient httpClient;
        private readonly IAccountsStateStore accountsStateStore;
        private readonly IBankAccountValidator bankAccountValidator;

        public BankAccountEditDialogViewModel(
            HttpClient httpClient,
            IAccountsStateStore accountsStateStore,
            IBankAccountValidator bankAccountValidator)
        {
            // TODO: Wrap HttpClient and HttpClientJsonExtensions with an interface we own so it can be safely mocked.
            this.httpClient = httpClient;
            this.accountsStateStore = accountsStateStore;
            this.bankAccountValidator = bankAccountValidator;

            this.accountsStateStore.EditAccount += (BankAccount bankAccount) =>
            {
                Guard.Against.Null(bankAccount, nameof(bankAccount));

                this.BankAccount = bankAccount;

                this.HeaderText = this.BankAccount.BankAccountID == Constants.NewRecordID ? NewBankAccountHeader : EditBankAccountHeader;
                this.ShowDialog = true;
            };
        }

        public BankAccount BankAccount { get; private set; } = new BankAccount();

        public IEnumerable<BankAccountType> BankAccountTypes { get; private set; } = new List<BankAccountType>();
        public IEnumerable<Currency> Currencies { get; private set; } = new List<Currency>();

        public string HeaderText { get; private set; } = string.Empty;

        private bool showDialog = false;
        public bool ShowDialog
        {
            get { return this.showDialog; }
            set
            {
                if (this.showDialog == value)
                    return;

                this.showDialog = value;
                this.OnPropertyChanged();
            }
        }

        public void OnCancelButtonClick()
        {
            this.ShowDialog = false;
        }

        public void OnSaveButtonClick()
        {
            this.ShowDialog = false;
        }

        public async Task InitializeAsync()
        {
            // TODO: Add unit test for BankAccountEditDialogViewModel.InitializeAsync.
            await this.LoadBankAccountTypesLookupDataAsync().ConfigureAwait(false);
            await this.LoadCurrencyLookupDataAsync().ConfigureAwait(false);
        }

        private async Task LoadBankAccountTypesLookupDataAsync()
        {
            var response = await httpClient.GetFromJsonAsync<BankAccountType[]>("api/v1/BankAccountType").ConfigureAwait(false);
            this.BankAccountTypes = new List<BankAccountType>(response);
#if DEBUG
            foreach (var item in this.BankAccountTypes)
            {
                Console.WriteLine($"[BankAccountType - BankAccountTypeID: {item.BankAccountTypeID} Description: {item.Description}]");
            }
#endif
        }
        private async Task LoadCurrencyLookupDataAsync()
        {
            var response = await httpClient.GetFromJsonAsync<Currency[]>("api/v1/Currency").ConfigureAwait(false);
            this.Currencies = new List<Currency>(response);
#if DEBUG
            foreach (var item in this.Currencies)
            {
                Console.WriteLine($"[Currency - CurrencyID: {item.CurrencyID} Symbol: {item.Symbol} DecimalFormat: {item.DecimalFormat}]");
            }
#endif
        }
    }
}
