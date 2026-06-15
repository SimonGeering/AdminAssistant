using AdminAssistant.Primitives.UI;
using Microsoft.AspNetCore.Components;

namespace AdminAssistant.Blazor.Client.Shared;

public abstract class AdminAssistantLayoutComponentBase<TViewModel> : LayoutComponentBase, IDisposable
    where TViewModel : class, IViewModelBase
{
    private bool _disposed;
    private void vmOnPropertyChanged(object? sender, EventArgs e) => StateHasChanged();

    [Inject] protected NavigationManager Nav { get; set; } = null!;
    [Inject] protected TViewModel vm { get; set; } = null!;
    protected bool IsDesignerDemo { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        IsDesignerDemo = Nav.Uri.Contains("/demo/", StringComparison.OrdinalIgnoreCase);

        // Note: we are not setting a dynamic VM based on IsDesignerDemo as we don't want to
        // go to the trouble of implementing a designer VM. this will mean that vm.IsDesignerDemo
        // will always be false and may not match the Nav.Uri.

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

    ~AdminAssistantLayoutComponentBase() => Dispose(false);
}
