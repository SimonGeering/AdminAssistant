using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace AdminAssistant.Shared.UI;

public class ModeSelectionChangedMessage : ValueChangedMessage<ModeSelectionItem>
{
    public ModeSelectionChangedMessage(ModeSelectionItem modeSelectionItem)
        : base(modeSelectionItem)
    {
    }
}
