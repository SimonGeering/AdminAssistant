using AdminAssistant.DomainModel.Modules.CoreModule;

namespace AdminAssistant.UI.Modules.CoreModule.Admin;

public interface ICurrencyListViewModel : IViewModelBase
{
    IEnumerable<Currency> Currencies { get; }
}
