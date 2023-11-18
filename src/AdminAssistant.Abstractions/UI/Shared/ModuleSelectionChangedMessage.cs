using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace AdminAssistant.UI.Shared;

public sealed class ModuleSelectionChangedMessage : ValueChangedMessage<ModuleSelectionItem>
{
    public ModuleSelectionChangedMessage(ModuleSelectionItem moduleSelectionItem)
        : base(moduleSelectionItem)
    {
    }
}
