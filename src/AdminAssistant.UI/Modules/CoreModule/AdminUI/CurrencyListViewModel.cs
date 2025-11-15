using AdminAssistant.Domain;

namespace AdminAssistant.Modules.CoreModule.AdminUI;

public interface ICurrencyListViewModel : IViewModelBase
{
    IEnumerable<Currency> Currencies { get; }
}
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
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class CurrencyListDesignerViewModel
    : DesignerViewModelBase, ICurrencyListViewModel
{
    private readonly List<Currency> _currencies =
    [
        Factory.Currency.WithTestData(1).WithSymbol("GBP").Build(),
        Factory.Currency.WithTestData(2).WithSymbol("EUR").Build(),
        Factory.Currency.WithTestData(3).WithSymbol("USD").Build()
    ];

    public IEnumerable<Currency> Currencies => _currencies.AsEnumerable();
}
