namespace AdminAssistant.Shared.UI;

public sealed class ModuleSelectionChangedMessage(ModuleSelectionItem moduleSelectionItem)
    : ValueChangedMessage<ModuleSelectionItem>(moduleSelectionItem)
{
}
