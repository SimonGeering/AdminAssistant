using System.Collections.Generic;

namespace AdminAssistant.Core.UI.Shared.Sidebar
{
    public interface ISidebarViewModel : IViewModelBase
    {
        SidebarStateSettings Sidebar { get; }

        ModeSelectionStateSettings ModeSelectionDropDown { get; }

        List<ModeSelectionItem> Modes { get; }
        ModeSelectionItem ActiveMode { get; }

        List<ModuleSelectionItem> Modules { get; }
        ModuleSelectionItem ActiveModule { get; }

        void OnModeSelectionDropDownClick();
        void OnSideBarControlButtonClick();

        void OnSelectedModeChanged(ModeSelectionItem mode);
        void OnSelectedModuleChanged(ModuleSelectionItem selectedModule);
    }
}
