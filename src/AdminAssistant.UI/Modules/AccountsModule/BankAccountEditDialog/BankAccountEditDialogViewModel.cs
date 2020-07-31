using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Modules.CoreModule;
using AdminAssistant.UI.Shared;
using Ardalis.GuardClauses;
using FluentValidation.Results;

namespace AdminAssistant.UI.Modules.AccountsModule.BankAccountEditDialog
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountEditDialogViewModel : ViewModelBase, IBankAccountEditDialogViewModel
    {
        private readonly IAccountsStateStore accountsStateStore;
        private readonly IBankAccountValidator bankAccountValidator;
        private readonly IAccountsService accountsService;
        private readonly ICoreService coreService;

        private BankAccount bankAccount = new BankAccount();

        public BankAccountEditDialogViewModel(
            ILoggingProvider log,
            ILoadingSpinner loadingSpinner,
            IAccountsStateStore accountsStateStore,
            IBankAccountValidator bankAccountValidator,            
            IAccountsService accountsService,
            ICoreService coreService)
            : base(log, loadingSpinner)
        {
            this.accountsStateStore = accountsStateStore;
            this.bankAccountValidator = bankAccountValidator;
            this.accountsService = accountsService;
            this.coreService = coreService;

            this.accountsStateStore.EditAccount += (BankAccount bankAccount) =>
            {
                Guard.Against.Null(bankAccount, nameof(bankAccount));

                this.bankAccount = bankAccount;

                this.HeaderText = (this.bankAccount as IDatabasePersistable).IsNew ? IBankAccountEditDialogViewModel.NewBankAccountHeader : IBankAccountEditDialogViewModel.EditBankAccountHeader;
                this.RefreshValidation();
                this.ShowDialog = true;
            };
        }

        public string AccountName
        {
            get => this.bankAccount.AccountName;
            set
            {
                if (this.bankAccount.AccountName.Equals(value, StringComparison.InvariantCulture))
                    return;

                this.bankAccount.AccountName = value;
                this.RefreshValidation();
                this.OnPropertyChanged();
            }
        }

        public int BankAccountID
        {
            get => this.bankAccount.BankAccountID;
            set
            {
                if (this.bankAccount.BankAccountID.Equals(value))
                    return;

                this.bankAccount.BankAccountID = value;
                this.RefreshValidation();
                this.OnPropertyChanged();
            }
        }

        public int BankAccountTypeID
        {
            get => this.bankAccount.BankAccountTypeID;
            set
            {
                if (this.bankAccount.BankAccountTypeID.Equals(value))
                    return;

                this.bankAccount.BankAccountTypeID = value;
                this.RefreshValidation();
                this.OnPropertyChanged();
            }
        }

        public int CurrencyID
        {
            get => this.bankAccount.CurrencyID;
            set
            {
                if (this.bankAccount.CurrencyID.Equals(value))
                    return;

                this.bankAccount.CurrencyID = value;
                this.RefreshValidation();
                this.OnPropertyChanged();
            }
        }

        public bool IsBudgeted
        {
            get => this.bankAccount.IsBudgeted;
            set
            {
                if (this.bankAccount.IsBudgeted.Equals(value))
                    return;

                this.bankAccount.IsBudgeted = value;
                this.RefreshValidation();
                this.OnPropertyChanged();
            }
        }

        public int OpeningBalance 
        {
            get => this.bankAccount.OpeningBalance;
            set
            {
                if (this.bankAccount.OpeningBalance.Equals(value))
                    return;

                this.bankAccount.OpeningBalance = value;
                this.RefreshValidation();
                this.OnPropertyChanged();
            }
        }

        public int CurrentBalance => this.bankAccount.CurrentBalance;

        public DateTime OpenedOn
        {
            get => this.bankAccount.OpenedOn;
            set
            {
                if (this.bankAccount.OpenedOn.Equals(value))
                    return;

                this.bankAccount.OpenedOn = value;
                this.RefreshValidation();
                this.OnPropertyChanged();
            }
        }

        private IEnumerable<BankAccountType> bankAccountTypes = new List<BankAccountType>();
        public IEnumerable<BankAccountType> BankAccountTypes
        {
            get => this.bankAccountTypes;
            private set
            {
                this.bankAccountTypes = value;
                this.OnPropertyChanged();
            }
        }

        private IEnumerable<Currency> currencies = new List<Currency>();
        public IEnumerable<Currency> Currencies
        {
            get => this.currencies;
            private set
            {
                this.currencies = value;
                this.OnPropertyChanged();
            }
        }

        private string headerText = string.Empty;
        public string HeaderText
        {
            get => this.headerText;
            private set
            {
                if (this.headerText.Equals(value, StringComparison.InvariantCulture))
                    return;

                this.headerText = value;
                this.OnPropertyChanged();
            }
        }

        private bool showDialog = false;
        public bool ShowDialog
        {
            get => this.showDialog;
            set
            {
                if (this.showDialog.Equals(value))
                    return;

                this.showDialog = value;
                this.OnPropertyChanged();
            }
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
            this.Currencies = await this.coreService.GetCurrencyListAsync().ConfigureAwait(false);

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
#if DEBUG
    public class BankAccountEditDialogDesignTimeViewModel : DesignTimeViewModelBase, IBankAccountEditDialogViewModel
    {
        public string AccountName { get; set; } = "Acme Bank Current Account";

        public int BankAccountID { get; set; } = Constants.UnknownRecordID;
        public int BankAccountTypeID { get; set; } = Constants.UnknownRecordID;
        public int CurrencyID { get; set; } = Constants.UnknownRecordID;
        public bool IsBudgeted { get; set; } = false;
        public int OpeningBalance { get; set; } = 0;
        public int CurrentBalance { get; private set; } = 0;
        public DateTime OpenedOn { get; set; }

        public BankAccountEditDialogDesignTimeViewModel()
        {
            this.OpenedOn = DateTime.Today;
        }

        public string HeaderText { get; private set; } = "Create Bank Account";

        public bool ShowDialog { get; set; } = false;

        public IEnumerable<BankAccountType> BankAccountTypes => new List<BankAccountType>();

        public IEnumerable<Currency> Currencies => new List<Currency>();

        public string AccountNameValidationMessage => string.Empty;

        public string AccountNameValidationClass => string.Empty;

        public void OnAccountNameChanged(string accountName) => throw new System.NotImplementedException();
        public void OnBankAccountTypeChanged() => throw new System.NotImplementedException();
        public void OnCancelButtonClick() => throw new System.NotImplementedException();
        public void OnCurrencyChanged() => throw new System.NotImplementedException();
        public Task OnSaveButtonClick() => throw new System.NotImplementedException();
    }
#endif // DEBUG
}
