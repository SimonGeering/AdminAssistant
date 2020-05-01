using System;

namespace AdminAssistant.UI.Shared
{
    public class LoadingSpinner : ILoadingSpinner
    {
        public event Action? ShowLoadingSpinner;
        public event Action? HideLoadingSpinner;

        public void OnShowLoadingSpinner() => this.ShowLoadingSpinner?.Invoke();
        public void OnHideLoadingSpinner() => this.HideLoadingSpinner?.Invoke();
    }
}
