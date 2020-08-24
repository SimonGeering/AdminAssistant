using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace AdminAssistant.UI.Shared
{
    public class ModeSelectionChangedMessage : ValueChangedMessage<ModeSelectionItem>
    {
        public ModeSelectionChangedMessage(ModeSelectionItem modeSelectionItem)
            : base(modeSelectionItem)
        {
        }
    }
}
