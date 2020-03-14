using System;
using System.ComponentModel;
using AdminAssistant.Core.UI.Shared;

namespace AdminAssistant.Core.UI.Services
{
    public interface IAppStateStore : INotifyPropertyChanged
    {
        event Action? ActiveModeChanged;
        event Action? ActiveModuleChanged;

        ModeSelectionItem ActiveMode { get; }
        ModuleSelectionItem ActiveModule { get; }

        void OnActiveModeChanged(ModeSelectionItem mode);
        void OnActiveModuleChanged(ModuleSelectionItem module);
    }
}
