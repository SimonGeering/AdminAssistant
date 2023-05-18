using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.AccountsModule.Admin;

internal sealed class BankListViewModel : ViewModelBase, IBankListViewModel
{
    private readonly List<Bank> _banks;

    public IEnumerable<Bank> Banks => _banks.AsEnumerable();

    public BankListViewModel(ILoggingProvider log) : base(log)
        => _banks = new List<Bank>()
        {
            // TODO: Test data for mocking UI design bound to VM
            new Bank() { BankID = 1, BankName = "Barclays Bank plc" },
            new Bank() { BankID = 2, BankName = "HSBC Bank (UK) Limited" },
            new Bank() { BankID = 3, BankName = "Santander UK Plc" }
        };
}
