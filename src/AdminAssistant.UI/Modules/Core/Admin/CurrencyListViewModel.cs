using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.Infra.Providers;
using AdminAssistant.UI.Modules.CoreModule.Admin;

namespace AdminAssistant.UI.Modules.Core.Admin;

internal sealed class CurrencyListViewModel : ViewModelBase, ICurrencyListViewModel
{
    private readonly List<Currency> _currencies;

    public IEnumerable<Currency> Currencies => _currencies.AsEnumerable();

    public CurrencyListViewModel(ILoggingProvider log) : base(log)
        => _currencies = new List<Currency>()
        {
            Factory.Currency.WithTestData(1).WithSymbol("GBP").Build(),
            Factory.Currency.WithTestData(2).WithSymbol("EUR").Build(),
            Factory.Currency.WithTestData(3).WithSymbol("USD").Build()
        };
}
