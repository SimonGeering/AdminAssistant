using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace AdminAssistant.Shared.UI;

public sealed class ModuleSelectionChangedMessage : ValueChangedMessage<ModuleSelectionItem>
{
    public ModuleSelectionChangedMessage(ModuleSelectionItem moduleSelectionItem)
        : base(moduleSelectionItem)
    {
    }
}
