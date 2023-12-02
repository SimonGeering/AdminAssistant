using AdminAssistant.Primitives.UI;

namespace AdminAssistant.Retro.Shared;

internal abstract class DialogWindowBase<TViewModel>
    : Dialog
    where TViewModel : IViewModelBase
{
    protected TViewModel VM {get; init;}

	protected DialogWindowBase(TViewModel vm, string title)
	{
        VM = vm;
		Title = title;
	}
}
