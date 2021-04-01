using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Infra.Providers;
using AdminAssistant.Infra.DAL;
using AdminAssistant.UI.Modules.CoreModule;
using FluentValidation.Results;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using AdminAssistant.DomainModel.Modules.CoreModule;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    [SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountEditDialogViewModel : ViewModelBase, IBankAccountEditDialogViewModel
    {
        private readonly IBankAccountValidator _bankAccountValidator;
        private readonly IAccountsService _accountsService;
        private readonly ICoreService _coreService;
        private readonly IMessenger _messenger;

        private BankAccount bankAccount = new BankAccount();

        public BankAccountEditDialogViewModel(
            ILoggingProvider log,
            IBankAccountValidator bankAccountValidator,            
            IAccountsService accountsService,
            ICoreService coreService,
            IMessenger messenger)
            : base(log)
        {
            IsBusy = true;

            _bankAccountValidator = bankAccountValidator;
            _accountsService = accountsService;
            _coreService = coreService;
            _messenger = messenger;

            _messenger.RegisterAll(this);

            Cancel = new AsyncRelayCommand(execute: OnCancelButtonClick);
            Save = new AsyncRelayCommand(execute: OnSaveButtonClick);
        }

        ~BankAccountEditDialogViewModel() => _messenger.UnregisterAll(this);

        public IAsyncRelayCommand Cancel { get; }
        public IAsyncRelayCommand Save { get; }

        public string AccountName
        {
            get => bankAccount.AccountName;
            set
            {
                if (bankAccount.AccountName.Equals(value, StringComparison.InvariantCulture))
                    return;

                // TODO: Switch to call base helper extension.
                // TODO: Hook Property changed and call refresh validation once for all properties.
                bankAccount = bankAccount with { AccountName = value };
                RefreshValidation();
                OnPropertyChanged();
            }
        }

        public int BankAccountID
        {
            get => bankAccount.BankAccountID;
            set
            {
                if (bankAccount.BankAccountID.Equals(value))
                    return;

                bankAccount = bankAccount with { BankAccountID = value };
                RefreshValidation();
                OnPropertyChanged();
            }
        }

        public int BankAccountTypeID
        {
            get => bankAccount.BankAccountTypeID;
            set
            {
                if (bankAccount.BankAccountTypeID.Equals(value))
                    return;

                bankAccount = bankAccount with { BankAccountTypeID = value };
                RefreshValidation();
                OnPropertyChanged();
            }
        }

        public int CurrencyID
        {
            get => bankAccount.CurrencyID;
            set
            {
                if (bankAccount.CurrencyID.Equals(value))
                    return;

                bankAccount = bankAccount with { CurrencyID = value };
                RefreshValidation();
                OnPropertyChanged();
            }
        }

        public bool IsBudgeted
        {
            get => bankAccount.IsBudgeted;
            set
            {
                if (bankAccount.IsBudgeted.Equals(value))
                    return;

                bankAccount = bankAccount with { IsBudgeted = value };
                RefreshValidation();
                OnPropertyChanged();
            }
        }

        public int OpeningBalance 
        {
            get => bankAccount.OpeningBalance;
            set
            {
                if (bankAccount.OpeningBalance.Equals(value))
                    return;

                bankAccount = bankAccount with { OpeningBalance = value };
                RefreshValidation();
                OnPropertyChanged();
            }
        }

        public int CurrentBalance => bankAccount.CurrentBalance;

        public DateTime OpenedOn
        {
            get => bankAccount.OpenedOn;
            set
            {
                if (bankAccount.OpenedOn.Equals(value))
                    return;

                bankAccount = bankAccount with { OpenedOn = value };
                RefreshValidation();
                OnPropertyChanged();
            }
        }

        public BindingList<BankAccountType> BankAccountTypes { get; } = new BindingList<BankAccountType>();

        public BindingList<Currency> Currencies { get; } = new BindingList<Currency>();

        private string headerText = string.Empty;
        public string HeaderText
        {
            get => headerText;
            private set
            {
                if (headerText.Equals(value, StringComparison.InvariantCulture))
                    return;

                headerText = value;
                OnPropertyChanged();
            }
        }

        private bool showDialog;
        public bool ShowDialog
        {
            get => showDialog;
            set
            {
                if (showDialog.Equals(value))
                    return;

                showDialog = value;
                OnPropertyChanged();
            }
        }

        public void OnAccountNameChanged(string accountName)
        {
            Log.Start();

            bankAccount = bankAccount with { AccountName = accountName };
            RefreshValidation();
            OnPropertyChanged();

            Log.Finish();
        }

        public string AccountNameValidationMessage { get; private set; } = string.Empty;
        public string AccountNameValidationClass { get; private set; } = string.Empty;

        public void OnBankAccountTypeChanged() => RefreshValidation();

        public void OnCurrencyChanged() => RefreshValidation();

        private async Task OnCancelButtonClick()
        {
            Log.Start();

            bankAccount = new BankAccount();
            ShowDialog = false;

            await Task.CompletedTask.ConfigureAwait(true);

            Log.Finish();
        }

        [SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "WIP")]
        private async Task OnSaveButtonClick()
        {
            Log.Start();

            try
            {
                IsBusy = true;

                var canSave = RefreshValidation();

                if (canSave)
                {
                    if ((bankAccount as IDatabasePersistable).IsNew)
                    {
                        var savedBankAccountResult = await _accountsService.CreateBankAccountAsync(bankAccount).ConfigureAwait(true);
                        // TODO: Notify OnBankAccountCreated
                        // this.accountsStateStore.OnBankAccountCreated(savedBankAccount);
                    }
                    else
                    {
                        var savedBankAccountResult = await _accountsService.UpdateBankAccountAsync(bankAccount).ConfigureAwait(true);
                        // TODO: Notify OnBankAccountUpdated
                        // this.accountsStateStore.OnBankAccountUpdated(savedBankAccount);
                    }
                    ShowDialog = false;
                }
            }
            finally
            {
                IsBusy = false;
            }
            Log.Finish();
        }

        public override async Task OnLoadedAsync()
        {
            Log.Start();

            try
            {
                IsBusy = true;

                var bankAccountTypes = await _accountsService.LoadBankAccountTypesLookupDataAsync().ConfigureAwait(true);
                bankAccountTypes.ForEach(item => BankAccountTypes.Add(item));

                var currencies = await _coreService.GetCurrencyListAsync().ConfigureAwait(true);
                currencies.ForEach(item => Currencies.Add(item));

                await base.OnLoadedAsync().ConfigureAwait(true);
            }
            finally
            {
                IsBusy = false;
            }

            Log.Finish();
        }

        public void Receive(EditBankAccountMessage message)
        {
            bankAccount = message.BankAccount;
            HeaderText = (bankAccount as IDatabasePersistable).IsNew ? IBankAccountEditDialogViewModel.NewBankAccountHeader : IBankAccountEditDialogViewModel.EditBankAccountHeader;
            RefreshValidation();
            ShowDialog = true;
        }

        private bool RefreshValidation()
        {
            var result = _bankAccountValidator.Validate(bankAccount);
            AccountNameValidationMessage = GetValidationMessageForField(nameof(BankAccount.AccountName), result);
            AccountNameValidationClass = GetValidationClassForField(nameof(BankAccount.AccountName), result);

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

            return GetValidationClassForSeverity(result.Errors.Single(x => x.PropertyName == fieldName).Severity);
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
