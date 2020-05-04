using System;

namespace AdminAssistant.UI.Shared
{
    public interface ILoadingSpinner
    {
        void OnHideLoadingSpinner();
        void OnShowLoadingSpinner();

        event Action? HideLoadingSpinner;
        event Action? ShowLoadingSpinner;
    }
}
