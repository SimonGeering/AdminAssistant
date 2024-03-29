using System.Diagnostics.CodeAnalysis;
using AdminAssistant.Modules.CoreModule.UI;
using AdminAssistant.Modules.AccountsModule.Validation;
using AdminAssistant.Modules.CoreModule;

namespace AdminAssistant.Modules.AccountsModule.UI;

public interface IBankAccountEditDialogViewModel : IViewModelBase, IRecipient<EditBankAccountMessage>
{
    public const string NewBankAccountHeader = "New bank account";
    public const string EditBankAccountHeader = "Edit bank account";

    int BankAccountID { get; }
    int BankAccountTypeID { get; set; }
    int CurrencyID { get; set; }
    string AccountName { get; set; }
    public bool IsBudgeted { get; set; }
    public int OpeningBalance { get; set; }
    public int CurrentBalance { get; }
    public DateTime OpenedOn { get; set; }

    string HeaderText { get; }
    bool ShowDialog { get; set; }

    BindingList<BankAccountType> BankAccountTypes { get; }
    BindingList<Currency> Currencies { get; }

    IAsyncRelayCommand Save { get; }
    IAsyncRelayCommand Cancel { get; }

    void OnAccountNameChanged(string accountName);
    string AccountNameValidationMessage { get; }
    string AccountNameValidationClass { get; }

    void OnBankAccountTypeChanged();
    void OnCurrencyChanged();
}
internal sealed class BankAccountEditDialogViewModel : ViewModelBase, IBankAccountEditDialogViewModel
{
    private readonly IBankAccountValidator _bankAccountValidator;
    private readonly IAccountsService _accountsService;
    private readonly ICoreService _coreService;
    private readonly IMessenger _messenger;

    private BankAccount bankAccount = new();

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
        get => bankAccount.BankAccountID.Value;
        set
        {
            if (bankAccount.BankAccountID.Equals(value))
                return;

            bankAccount = bankAccount with { BankAccountID = new(value) };
            RefreshValidation();
            OnPropertyChanged();
        }
    }

    public int BankAccountTypeID
    {
        get => bankAccount.BankAccountTypeID.Value;
        set
        {
            if (bankAccount.BankAccountTypeID.Equals(value))
                return;

            bankAccount = bankAccount with { BankAccountTypeID = new(value) };
            RefreshValidation();
            OnPropertyChanged();
        }
    }

    public int CurrencyID
    {
        get => bankAccount.CurrencyID.Value;
        set
        {
            if (bankAccount.CurrencyID.Equals(value))
                return;

            bankAccount = bankAccount with { CurrencyID = new(value) };
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

    public BindingList<BankAccountType> BankAccountTypes { get; } = [];

    public BindingList<Currency> Currencies { get; } = [];

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
    [SuppressMessage("Minor Code Smell", "S1481:Unused local variables should be removed", Justification = "WIP")]
    private async Task OnSaveButtonClick()
    {
        Log.Start();

        try
        {
            IsBusy = true;

            var canSave = RefreshValidation();

            if (canSave)
            {
#pragma warning disable S125 // Sections of code should not be commented out
                if ((bankAccount as IPersistable).IsNew)
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
#pragma warning restore S125 // Sections of code should not be commented out
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
        HeaderText = (bankAccount as IPersistable).IsNew ? IBankAccountEditDialogViewModel.NewBankAccountHeader : IBankAccountEditDialogViewModel.EditBankAccountHeader;
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

        if (result.Errors.Exists(x => x.PropertyName == fieldName) == false)
            return ValidationMessage.None;

        return result.Errors.Single(x => x.PropertyName == fieldName).ErrorMessage;
    }

    private string GetValidationClassForField(string fieldName, ValidationResult result)
    {
        if (result.IsValid)
            return ValidationCssClass.None;

        if (result.Errors.Exists(x => x.PropertyName == fieldName) == false)
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
