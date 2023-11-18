using AdminAssistant.UI;
using Microsoft.AspNetCore.Components;

namespace AdminAssistant.Blazor.Client.Shared;

public abstract class AdminAssistantComponentBase<TViewModel> : ComponentBase
    where TViewModel : IViewModelBase
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    [Inject]
    protected TViewModel vm { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

    protected override void OnInitialized()
    {
        vm.PropertyChanged += (o, e) => StateHasChanged();
        base.OnInitialized();
    }

    protected override Task OnInitializedAsync() => vm.OnInitializedAsync();
}
