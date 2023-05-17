using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule.Admin;

public interface IBankListViewModel : IViewModelBase
{
    IEnumerable<Bank> Banks { get; }
}
