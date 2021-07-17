using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;

namespace MogLog
{
    public class FFXIVMessage
    {
        public XivChatType ChatType { get; private set; }
        public uint SenderId { get; private set; }
        public SeString Sender { get; private set; }
        public SeString Message { get; private set; }

        public FFXIVMessage(XivChatType xivChatType, uint senderId, SeString sender, SeString message)
        {
            ChatType = xivChatType;
            SenderId = senderId;
            Sender = sender;
            Message = message;
        }
    }
}
