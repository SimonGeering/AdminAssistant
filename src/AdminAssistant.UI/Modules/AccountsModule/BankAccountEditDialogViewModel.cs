using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Framework.Providers;
using AdminAssistant.Infra.DAL;
using AdminAssistant.UI.Modules.CoreModule;
using FluentValidation.Results;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountEditDialogViewModel : ViewModelBase, IBankAccountEditDialogViewModel
    {
        private readonly IBankAccountValidator bankAccountValidator;
        private readonly IAccountsService accountsService;
        private readonly ICoreService coreService;
        private readonly IMessenger messenger;

        private BankAccount bankAccount = new BankAccount();

        public BankAccountEditDialogViewModel(
            ILoggingProvider log,
            IBankAccountValidator bankAccountValidator,            
            IAccountsService accountsService,
            ICoreService coreService,
            IMessenger messenger)
            : base(log)
        {
            this.IsBusy = true;

            this.bankAccountValidator = bankAccountValidator;
            this.accountsService = accountsService;
            this.coreService = coreService;
            this.messenger = messenger;

            this.messenger.RegisterAll(this);

            this.Cancel = new AsyncRelayCommand(execute: this.OnCancelButtonClick);
            this.Save = new AsyncRelayCommand(execute: this.OnSaveButtonClick);
        }

        ~BankAccountEditDialogViewModel() => this.messenger.UnregisterAll(this);

        public IAsyncRelayCommand Cancel { get; }
        public IAsyncRelayCommand Save { get; }

        public string AccountName
        {
            get => this.bankAccount.AccountName;
            set
            {
                if (this.bankAccount.AccountName.Equals(value, StringComparison.InvariantCulture))
                    return;

                // TODO: Switch to call base helper extension.
                // TODO: Hook Property changed and call refresh validation once for all properties.
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

        public BindingList<BankAccountType> BankAccountTypes { get; } = new BindingList<BankAccountType>();

        public BindingList<Currency> Currencies { get; } = new BindingList<Currency>();

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

        private bool showDialog;
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

        private async Task OnCancelButtonClick()
        {
            this.Log.Start();

            this.bankAccount = new BankAccount();
            this.ShowDialog = false;

            await Task.CompletedTask.ConfigureAwait(true);

            this.Log.Finish();
        }

        private async Task OnSaveButtonClick()
        {
            this.Log.Start();

            try
            {
                this.IsBusy = true;

                var canSave = this.RefreshValidation();

                if (canSave)
                {
                    if ((this.bankAccount as IDatabasePersistable).IsNew)
                    {
                        var savedBankAccountResult = await this.accountsService.CreateBankAccountAsync(this.bankAccount).ConfigureAwait(true);
                        // TODO: Notify OnBankAccountCreated
                        // this.accountsStateStore.OnBankAccountCreated(savedBankAccount);
                    }
                    else
                    {
                        var savedBankAccountResult = await this.accountsService.UpdateBankAccountAsync(this.bankAccount).ConfigureAwait(true);
                        // TODO: Notify OnBankAccountUpdated
                        // this.accountsStateStore.OnBankAccountUpdated(savedBankAccount);
                    }
                    this.ShowDialog = false;
                }
            }
            finally
            {
                this.IsBusy = false;
            }
            this.Log.Finish();
        }

        public override async Task OnLoadedAsync()
        {
            this.Log.Start();

            try
            {
                this.IsBusy = true;

                var bankAccountTypes = await this.accountsService.LoadBankAccountTypesLookupDataAsync().ConfigureAwait(true);
                bankAccountTypes.ForEach(item => this.BankAccountTypes.Add(item));

                var currencies = await this.coreService.GetCurrencyListAsync().ConfigureAwait(true);
                currencies.ForEach(item => this.Currencies.Add(item));

                await base.OnLoadedAsync().ConfigureAwait(true);
            }
            finally
            {
                this.IsBusy = false;
            }

            this.Log.Finish();
        }

        public void Receive(EditBankAccountMessage message)
        {
            this.bankAccount = message.BankAccount;

            this.HeaderText = (this.bankAccount as IDatabasePersistable).IsNew ? IBankAccountEditDialogViewModel.NewBankAccountHeader : IBankAccountEditDialogViewModel.EditBankAccountHeader;
            this.RefreshValidation();
            this.ShowDialog = true;
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
