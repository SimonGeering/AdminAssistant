using AdminAssistant.UI;
using Microsoft.AspNetCore.Components;
//using Syncfusion.Blazor.Spinner;

namespace AdminAssistant.Blazor.Shared;

public abstract class AdminAssistantLayoutComponentBase<TViewModel> : LayoutComponentBase
    where TViewModel : IViewModelBase
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    [Inject]
    protected TViewModel vm { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

    //protected SfSpinner SfSpinner { get; set; } = new SfSpinner();

    protected override void OnInitialized()
    {
        vm.PropertyChanged += (o, e) => StateHasChanged();
        vm.IsBusyChanged += (sender, isBusy) =>
        {
            //if (isBusy)
            //    SfSpinner.ShowAsync();
            //else
            //    SfSpinner.HideAsync();
        };
        base.OnInitialized();
    }

    protected override Task OnAfterRenderAsync(bool firstRender) => base.OnAfterRenderAsync(firstRender);

    protected override Task OnInitializedAsync() => vm.OnInitializedAsync();
}
