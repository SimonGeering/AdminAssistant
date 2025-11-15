using Microsoft.AspNetCore.Components;

namespace AdminAssistant.Blazor.Client.Shared;

public abstract class AdminAssistantComponentBase<TRuntimeViewModel, TDesignViewModel> : ComponentBase
    where TRuntimeViewModel : class, IViewModelBase
    where TDesignViewModel : class, TRuntimeViewModel
{
    [Inject] protected NavigationManager Nav { get; set; } = null!;
    [Inject] protected IServiceProvider Services { get; set; } = null!;

    // ReSharper disable once InconsistentNaming
    protected TRuntimeViewModel vm { get; private set; } = null!;

    protected bool IsDesignerDemo { get; private set; } = false;

    protected override void OnInitialized()
    {
        IsDesignerDemo = Nav.Uri.Contains("/demo/", StringComparison.OrdinalIgnoreCase);

        vm = IsDesignerDemo
            ? Services.GetRequiredService<TDesignViewModel>()
            : Services.GetRequiredService<TRuntimeViewModel>();

        vm.PropertyChanged += (_, _) => StateHasChanged();
        base.OnInitialized();
    }
    protected override Task OnInitializedAsync() => vm.OnInitializedAsync();
}

