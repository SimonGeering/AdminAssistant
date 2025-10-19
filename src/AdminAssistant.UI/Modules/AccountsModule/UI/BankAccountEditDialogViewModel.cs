using System.Diagnostics.CodeAnalysis;
using AdminAssistant.Modules.CoreModule.UI;
using AdminAssistant.Modules.AccountsModule.Validation;
using AdminAssistant.Modules.CoreModule;

namespace AdminAssistant.Modules.AccountsModule.UI;

public interface IBankAccountEditDialogViewModel : IViewModelBase, IRecipient<EditBankAccountMessage>
{
    public const string NewBankAccountHeader = "New bank account";
    public const string EditBankAccountHeader = "Edit bank account";

    int BankAccountId { get; }
    int BankAccountTypeId { get; set; }
    int CurrencyId { get; set; }
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

    private BankAccount _bankAccount = new();

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
        get => _bankAccount.AccountName;
        set
        {
            if (_bankAccount.AccountName.Equals(value, StringComparison.InvariantCulture))
                return;

            // TODO: Switch to call base helper extension.
            // TODO: Hook Property changed and call refresh validation once for all properties.
            _bankAccount = _bankAccount with { AccountName = value };
            RefreshValidation();
            OnPropertyChanged();
        }
    }

    public int BankAccountId
    {
        get => _bankAccount.BankAccountID.Value;
        set
        {
            if (_bankAccount.BankAccountID.Value.Equals(value))
                return;

            _bankAccount = _bankAccount with { BankAccountID = new BankAccountId(value) };
            RefreshValidation();
            OnPropertyChanged();
        }
    }

    public int BankAccountTypeId
    {
        get => _bankAccount.BankAccountTypeID.Value;
        set
        {
            if (_bankAccount.BankAccountTypeID.Value.Equals(value))
                return;

            _bankAccount = _bankAccount with { BankAccountTypeID = new BankAccountTypeId(value) };
            RefreshValidation();
            OnPropertyChanged();
        }
    }

    public int CurrencyId
    {
        get => _bankAccount.CurrencyID.Value;
        set
        {
            if (_bankAccount.CurrencyID.Value.Equals(value))
                return;

            _bankAccount = _bankAccount with { CurrencyID = new CurrencyId(value) };
            RefreshValidation();
            OnPropertyChanged();
        }
    }

    public bool IsBudgeted
    {
        get => _bankAccount.IsBudgeted;
        set
        {
            if (_bankAccount.IsBudgeted.Equals(value))
                return;

            _bankAccount = _bankAccount with { IsBudgeted = value };
            RefreshValidation();
            OnPropertyChanged();
        }
    }

    public int OpeningBalance
    {
        get => _bankAccount.OpeningBalance;
        set
        {
            if (_bankAccount.OpeningBalance.Equals(value))
                return;

            _bankAccount = _bankAccount with { OpeningBalance = value };
            RefreshValidation();
            OnPropertyChanged();
        }
    }

    public int CurrentBalance => _bankAccount.CurrentBalance;

    public DateTime OpenedOn
    {
        get => _bankAccount.OpenedOn;
        set
        {
            if (_bankAccount.OpenedOn.Equals(value))
                return;

            _bankAccount = _bankAccount with { OpenedOn = value };
            RefreshValidation();
            OnPropertyChanged();
        }
    }

    public BindingList<BankAccountType> BankAccountTypes { get; } = [];

    public BindingList<Currency> Currencies { get; } = [];

    private string _headerText = string.Empty;
    public string HeaderText
    {
        get => _headerText;
        private set
        {
            if (_headerText.Equals(value, StringComparison.InvariantCulture))
                return;

            _headerText = value;
            OnPropertyChanged();
        }
    }

    private bool _showDialog;
    public bool ShowDialog
    {
        get => _showDialog;
        set
        {
            if (_showDialog.Equals(value))
                return;

            _showDialog = value;
            OnPropertyChanged();
        }
    }

    public void OnAccountNameChanged(string accountName)
    {
        Log.Start();

        _bankAccount = _bankAccount with { AccountName = accountName };
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

        _bankAccount = new BankAccount();
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
                if ((_bankAccount as IPersistable).IsNew)
                {
                    var savedBankAccountResult = await _accountsService.CreateBankAccountAsync(_bankAccount).ConfigureAwait(true);
                    // TODO: Notify OnBankAccountCreated
                    // this.accountsStateStore.OnBankAccountCreated(savedBankAccount);
                }
                else
                {
                    var savedBankAccountResult = await _accountsService.UpdateBankAccountAsync(_bankAccount).ConfigureAwait(true);
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
        _bankAccount = message.BankAccount;
        HeaderText = (_bankAccount as IPersistable).IsNew ? IBankAccountEditDialogViewModel.NewBankAccountHeader : IBankAccountEditDialogViewModel.EditBankAccountHeader;
        RefreshValidation();
        ShowDialog = true;
    }

    private bool RefreshValidation()
    {
        var result = _bankAccountValidator.Validate(_bankAccount);
        AccountNameValidationMessage = GetValidationMessageForField(nameof(BankAccount.AccountName), result);
        AccountNameValidationClass = GetValidationClassForField(nameof(BankAccount.AccountName), result);

        return result.IsValid;
    }

    private static string GetValidationMessageForField(string fieldName, ValidationResult result)
    {
        if (result.IsValid)
            return ValidationMessage.None;

        if (result.Errors.Exists(x => x.PropertyName == fieldName) == false)
            return ValidationMessage.None;

        return result.Errors.Single(x => x.PropertyName == fieldName).ErrorMessage;
    }

    private static string GetValidationClassForField(string fieldName, ValidationResult result)
    {
        if (result.IsValid)
            return ValidationCssClass.None;

        if (result.Errors.Exists(x => x.PropertyName == fieldName) == false)
            return ValidationMessage.None;

        return GetValidationClassForSeverity(result.Errors.Single(x => x.PropertyName == fieldName).Severity);
    }

    private static string GetValidationClassForSeverity(FluentValidation.Severity severity) => severity switch
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
