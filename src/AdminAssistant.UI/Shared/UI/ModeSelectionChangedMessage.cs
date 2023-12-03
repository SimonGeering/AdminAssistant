namespace AdminAssistant.Shared.UI;

public class ModeSelectionChangedMessage(ModeSelectionItem modeSelectionItem)
    : ValueChangedMessage<ModeSelectionItem>(modeSelectionItem)
{
}
