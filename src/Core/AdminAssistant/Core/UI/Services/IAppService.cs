using System;
using System.Collections.Generic;
using AdminAssistant.Core.UI.Shared;

namespace AdminAssistant.Core.UI.Services
{
    public interface IAppService
    {
        ModeSelectionItem GetDefaultMode();
        ModuleSelectionItem GetDefaultModule();
        List<ModeSelectionItem> GetModes();
        List<ModuleSelectionItem> GetModules();
    }
}
