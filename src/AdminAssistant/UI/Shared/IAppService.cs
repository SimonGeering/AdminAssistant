using System.Collections.Generic;

namespace AdminAssistant.UI.Shared
{
    public interface IAppService
    {
        ModeSelectionItem GetDefaultMode();
        ModuleSelectionItem GetDefaultModule();
        List<ModeSelectionItem> GetModes();
        List<ModuleSelectionItem> GetModules();
    }
}
