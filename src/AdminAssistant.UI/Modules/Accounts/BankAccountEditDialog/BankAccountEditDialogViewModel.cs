using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.DomainModel.Modules.Accounts.Validation;
using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Shared;
using Ardalis.GuardClauses;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountEditDialog
{
    public class BankAccountEditDialogViewModel : ViewModelBase, IBankAccountEditDialogViewModel
    {
        public const string NewBankAccountHeader = "New bank account";
        public const string EditBankAccountHeader = "Edit bank account";

        private readonly IAccountsStateStore accountsStateStore;
        private readonly IBankAccountValidator bankAccountValidator;
        private readonly IAccountsService accountsService;

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

                this.Model = bankAccount;
                this.HeaderText = this.Model.BankAccountID == Constants.NewRecordID ? NewBankAccountHeader : EditBankAccountHeader;
                this.RefreshValidation();
                this.ShowDialog = true;
            };
        }

        public BankAccount Model { get; private set; } = new BankAccount();

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

        public void OnAccountNameChanged(string accountName)
        {
            this.Log.Start();

            this.Model.AccountName = accountName;
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

            // TODO: Reset State.
            this.ShowDialog = false;

            this.Log.Finish();
        }

        public void OnSaveButtonClick()
        {
            this.Log.Start();

            this.LoadingSpinner.OnShowLoadingSpinner();

            var canSave = this.RefreshValidation();

            if (canSave)
            {
                // TODO: Save
                // TODO: Notify caller
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
            var result = this.bankAccountValidator.Validate(this.Model);

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
