using System;

namespace AdminAssistant.UI.Shared
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class LoadingSpinner : ILoadingSpinner
    {
        public event Action? ShowLoadingSpinner;
        public event Action? HideLoadingSpinner;

        public void OnShowLoadingSpinner() => this.ShowLoadingSpinner?.Invoke();
        public void OnHideLoadingSpinner() => this.HideLoadingSpinner?.Invoke();
    }
}
