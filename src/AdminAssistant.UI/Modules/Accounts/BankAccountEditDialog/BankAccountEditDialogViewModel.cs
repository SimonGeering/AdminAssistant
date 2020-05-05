using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.DomainModel.Modules.Accounts.Validation;
using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Shared;
using Ardalis.GuardClauses;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountEditDialog
{
    public class BankAccountEditDialogViewModel : ViewModelBase, IBankAccountEditDialogViewModel
    {
        public const string NewBankAccountHeader = "New bank account";
        public const string EditBankAccountHeader = "Edit bank account";

        private readonly IHttpClientJsonProvider httpClient;
        private readonly IAccountsStateStore accountsStateStore;
        private readonly IBankAccountValidator bankAccountValidator;

        public BankAccountEditDialogViewModel(
            ILoadingSpinner loadingSpinner,
            IHttpClientJsonProvider httpClient,
            IAccountsStateStore accountsStateStore,
            IBankAccountValidator bankAccountValidator,
            ILoggingProvider log)
            : base(log)
        {
            this.LoadingSpinner = loadingSpinner;
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

        public event Action? Validate;
        public void OnValidate() => this.Validate?.Invoke();
        
        public ILoadingSpinner LoadingSpinner { get; }

        public BankAccount BankAccount { get; private set; } = new BankAccount();

        private IEnumerable<BankAccountType> bankAccountTypes = new List<BankAccountType>();
        public IEnumerable<BankAccountType> BankAccountTypes
        {
            get { return this.bankAccountTypes; }
            private set
            {
                if (this.bankAccountTypes == value)
                    return;

                this.bankAccountTypes = value;
                this.OnPropertyChanged();
            }
        }

        private IEnumerable<Currency> currencies = new List<Currency>();
        public IEnumerable<Currency> Currencies
        {
            get { return this.currencies; }
            private set
            {
                if (this.currencies == value)
                    return;

                this.currencies = value;
                this.OnPropertyChanged();
            }
        }

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
            this.Log.Start();

            this.ShowDialog = false;

            this.Log.Finish();
        }
        public void OnSaveButtonClick()
        {
            this.Log.Start();

            this.OnValidate();
            //this.ShowDialog = false;

            this.Log.Finish();
        }

        public async Task InitializeAsync()
        {
            this.LoadingSpinner.OnShowLoadingSpinner();

            await this.LoadBankAccountTypesLookupDataAsync().ConfigureAwait(false);
            await this.LoadCurrencyLookupDataAsync().ConfigureAwait(false);

            this.LoadingSpinner.OnHideLoadingSpinner();
        }

        private async Task LoadBankAccountTypesLookupDataAsync()
        {
            var response = await httpClient.GetFromJsonAsync<BankAccountType[]>("api/v1/BankAccountType").ConfigureAwait(false);

            var values = new List<BankAccountType>(response);
            values.Insert(0, new BankAccountType() { BankAccountTypeID = 0, Description = string.Empty });

            this.BankAccountTypes = values;
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

            var values = new List<Currency>(response);
            values.Insert(0, new Currency() { CurrencyID = 0, Symbol = string.Empty, DecimalFormat = string.Empty });

            this.Currencies = values;
#if DEBUG
            foreach (var item in this.Currencies)
            {
                Console.WriteLine($"[Currency - CurrencyID: {item.CurrencyID} Symbol: {item.Symbol} DecimalFormat: {item.DecimalFormat}]");
            }
#endif
        }
    }
}
