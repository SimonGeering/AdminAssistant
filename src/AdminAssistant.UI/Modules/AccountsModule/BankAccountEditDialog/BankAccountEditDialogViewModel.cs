using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Shared;
using Ardalis.GuardClauses;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AdminAssistant.UI.Modules.AccountsModule.BankAccountEditDialog
{
    public class BankAccountEditDialogViewModel : ViewModelBase, IBankAccountEditDialogViewModel
    {
        public const string NewBankAccountHeader = "New bank account";
        public const string EditBankAccountHeader = "Edit bank account";

        private readonly IAccountsStateStore accountsStateStore;
        private readonly IBankAccountValidator bankAccountValidator;
        private readonly IAccountsService accountsService;

        private BankAccount bankAccount = new BankAccount();

        [System.Obsolete("Replace with delegate properties that implement INotifyPropertyChanged")]
        public BankAccount Model { get => this.bankAccount; }

        public BankAccountEditDialogViewModel(
            ILoggingProvider log,
            ILoadingSpinner loadingSpinner,
            IAccountsStateStore accountsStateStore,
            IBankAccountValidator bankAccountValidator,            
            IAccountsService accountsService)
            : base(log, loadingSpinner)
        {
            this.accountsStateStore = accountsStateStore;
            this.bankAccountValidator = bankAccountValidator;
            this.accountsService = accountsService;

            this.accountsStateStore.EditAccount += (BankAccount bankAccount) =>
            {
                Guard.Against.Null(bankAccount, nameof(bankAccount));

                this.bankAccount = bankAccount;

                this.HeaderText = (this.bankAccount as IDatabasePersistable).IsNew ? NewBankAccountHeader : EditBankAccountHeader;
                this.RefreshValidation();
                this.ShowDialog = true;
            };
        }

        private IEnumerable<BankAccountType> bankAccountTypes = new List<BankAccountType>();
        public IEnumerable<BankAccountType> BankAccountTypes
        {
            get => this.bankAccountTypes;
            private set => this.SetValue(ref this.bankAccountTypes, value);
        }

        private IEnumerable<Currency> currencies = new List<Currency>();
        public IEnumerable<Currency> Currencies
        {
            get => this.currencies;
            private set => this.SetValue(ref this.currencies, value);
        }

        private string headerText = string.Empty;
        public string HeaderText
        {
            get => this.headerText;
            private set => this.SetValue(ref this.headerText, value);
        }

        private bool showDialog = false;
        public bool ShowDialog
        {
            get => this.showDialog;
            set => this.SetValue(ref this.showDialog, value);
        }

        public void OnAccountNameChanged(string accountName)
        {
            this.Log.Start();

            this.bankAccount.AccountName = accountName;
            this.RefreshValidation();
            this.OnPropertyChanged();

            this.Log.Finish();
        }

        public string AccountNameValidationMessage { get; private set; } = string.Empty;
        public string AccountNameValidationClass { get; private set; } = string.Empty;

        public void OnBankAccountTypeChanged() => this.RefreshValidation();

        public void OnCurrencyChanged() => this.RefreshValidation();


        public void OnCancelButtonClick()
        {
            this.Log.Start();

            this.bankAccount = new BankAccount();
            this.ShowDialog = false;

            this.Log.Finish();
        }

        public async Task OnSaveButtonClick()
        {
            this.Log.Start();

            this.LoadingSpinner.OnShowLoadingSpinner();

            var canSave = this.RefreshValidation();

            if (canSave)
            {
                if ((this.bankAccount as IDatabasePersistable).IsNew)
                {
                    var savedBankAccountResult = await this.accountsService.CreateBankAccountAsync(this.bankAccount).ConfigureAwait(false);
                    // TODO: Notify OnBankAccountCreated
                    // this.accountsStateStore.OnBankAccountCreated(savedBankAccount);
                }
                else
                {
                    var savedBankAccountResult = await this.accountsService.UpdateBankAccountAsync(this.bankAccount).ConfigureAwait(false);
                    // TODO: Notify OnBankAccountUpdated
                    // this.accountsStateStore.OnBankAccountUpdated(savedBankAccount);
                }
                this.ShowDialog = false;
            }
            this.LoadingSpinner.OnHideLoadingSpinner();
            this.Log.Finish();
        }

        public override async Task OnInitializedAsync()
        {
            this.Log.Start();
            this.LoadingSpinner.OnShowLoadingSpinner();

            this.BankAccountTypes = await this.accountsService.LoadBankAccountTypesLookupDataAsync().ConfigureAwait(false);
#if DEBUG
            foreach (var item in this.BankAccountTypes)
            {
                this.Log.LogDebug($"[BankAccountType - BankAccountTypeID: {item.BankAccountTypeID} Description: {item.Description}]");
            }
#endif
            this.Currencies = await this.accountsService.LoadCurrencyLookupDataAsync().ConfigureAwait(false);
#if DEBUG
            foreach (var item in this.Currencies)
            {
                this.Log.LogDebug($"[Currency - CurrencyID: {item.CurrencyID} Symbol: {item.Symbol} DecimalFormat: {item.DecimalFormat}]");
            }
#endif
            this.LoadingSpinner.OnHideLoadingSpinner();

            await base.OnInitializedAsync().ConfigureAwait(false);
            this.Log.Finish();
        }

        private bool RefreshValidation()
        {
            var result = this.bankAccountValidator.Validate(this.bankAccount);

            this.AccountNameValidationMessage = this.GetValidationMessageForField(nameof(BankAccount.AccountName), result);
            this.AccountNameValidationClass = this.GetValidationClassForField(nameof(BankAccount.AccountName), result);

            return result.IsValid;
        }

        private string GetValidationMessageForField(string fieldName, ValidationResult result)
        {
            if (result.IsValid)
                return ValidationMessage.None;

            if (result.Errors.Any(x => x.PropertyName == fieldName) == false)
                return ValidationMessage.None;

            return result.Errors.Single(x => x.PropertyName == fieldName).ErrorMessage;
        }

        private string GetValidationClassForField(string fieldName, ValidationResult result)
        {
            if (result.IsValid)
                return ValidationCssClass.None;

            if (result.Errors.Any(x => x.PropertyName == fieldName) == false)
                return ValidationMessage.None;

            return this.GetValidationClassForSeverity(result.Errors.Single(x => x.PropertyName == fieldName).Severity);
        }

        private string GetValidationClassForSeverity(FluentValidation.Severity severity) => severity switch
        {
            FluentValidation.Severity.Error => ValidationCssClass.Error,
            FluentValidation.Severity.Warning => ValidationCssClass.Warning,
            FluentValidation.Severity.Info => ValidationCssClass.Info,
            _ => ValidationCssClass.None
        };

        private static class ValidationMessage
        {
            public const string None = "";
        }
        private static class ValidationCssClass
        {
            public const string Error = "e-error";
            public const string Warning = "e-error";
            public const string Info = "e-error";
            public const string None = "e-error";
        }
    }
}
