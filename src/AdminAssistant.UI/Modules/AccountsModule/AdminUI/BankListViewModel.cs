namespace AdminAssistant.Modules.AccountsModule.AdminUI;

public interface IBankListViewModel : IViewModelBase
{
    IEnumerable<Bank> Banks { get; }
}

internal sealed class BankListViewModel : ViewModelBase, IBankListViewModel
{
    private readonly List<Bank> _banks;

    public IEnumerable<Bank> Banks => _banks.AsEnumerable();

    public BankListViewModel(ILoggingProvider log) : base(log)
        => _banks = [
            // TODO: Test data for mocking UI design bound to VM
            new Bank() { BankID = new(1), BankName = new("Barclays Bank plc") },
            new Bank() { BankID = new(2), BankName = new("HSBC Bank (UK) Limited") },
            new Bank() { BankID = new(3), BankName = new("Santander UK Plc") }
        ];
}
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class BankListDesignerViewModel
    : DesignerViewModelBase, IBankListViewModel
{
    public IEnumerable<Bank> Banks => [
            new Bank() { BankID = new(1), BankName = new("Barclays Bank plc") },
            new Bank() { BankID = new(2), BankName = new("HSBC Bank (UK) Limited") },
            new Bank() { BankID = new(3), BankName = new("Santander UK Plc") }
        ];
}
