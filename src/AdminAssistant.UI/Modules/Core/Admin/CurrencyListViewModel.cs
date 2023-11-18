using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.Infra.Providers;
using AdminAssistant.UI.Modules.CoreModule.Admin;

namespace AdminAssistant.UI.Modules.Core.Admin;

internal sealed class CurrencyListViewModel(ILoggingProvider log)
    : ViewModelBase(log), ICurrencyListViewModel
{
    private readonly List<Currency> _currencies =
    [
        Factory.Currency.WithTestData(1).WithSymbol("GBP").Build(),
        Factory.Currency.WithTestData(2).WithSymbol("EUR").Build(),
        Factory.Currency.WithTestData(3).WithSymbol("USD").Build()
    ];

    public IEnumerable<Currency> Currencies => _currencies.AsEnumerable();
}
