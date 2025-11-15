using Microsoft.AspNetCore.Components;

namespace AdminAssistant.Blazor.Client.Shared;

public abstract class AdminAssistantComponentBase<TRuntimeViewModel, TDesignViewModel> : ComponentBase, IDisposable
    where TRuntimeViewModel : class, IViewModelBase
    where TDesignViewModel : class, TRuntimeViewModel
{
    private bool _disposed;
    private void vmOnPropertyChanged(object? sender, EventArgs e) => StateHasChanged();

    [Inject] protected NavigationManager Nav { get; set; } = null!;
    [Inject] protected IServiceProvider Services { get; set; } = null!;

    // ReSharper disable once InconsistentNaming
    protected TRuntimeViewModel vm { get; private set; } = null!;

    protected bool IsDesignerDemo { get; private set; } = false;

    protected override async Task OnInitializedAsync()
    {
        IsDesignerDemo = Nav.Uri.Contains("/demo/", StringComparison.OrdinalIgnoreCase);

        vm = IsDesignerDemo
            ? Services.GetRequiredService<TDesignViewModel>()
            : Services.GetRequiredService<TRuntimeViewModel>();

        vm.PropertyChanged += vmOnPropertyChanged;

        await vm.OnInitializedAsync();
        await base.OnInitializedAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing && vm != null)
        {
            vm.PropertyChanged -= vmOnPropertyChanged;
        }

        _disposed = true;
    }

    ~AdminAssistantComponentBase()
    {
        Dispose(false);
    }
}

