using System.Collections.Generic;
using AdminAssistant.UI.Shared;

namespace AdminAssistant.UI.Services
{
    public interface IAppService
    {
        ModeSelectionItem GetDefaultMode();
        ModuleSelectionItem GetDefaultModule();
        List<ModeSelectionItem> GetModes();
        List<ModuleSelectionItem> GetModules();
    }
}
