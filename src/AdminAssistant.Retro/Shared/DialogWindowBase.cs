using AdminAssistant.UI;

namespace AdminAssistant.Retro.Shared;

internal abstract class DialogWindowBase<TViewModel>
    : Dialog
    where TViewModel : IViewModelBase
{
    protected TViewModel VM {get; init;}

	public DialogWindowBase(TViewModel vm, string title)
	{
        VM = vm;
		Title = title;
	}
}
