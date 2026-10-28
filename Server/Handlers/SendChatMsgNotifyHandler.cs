using Common.Resources.Proto;
using PemukulPaku.GameServer.Game.Chatrooms;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SendChatMsgNotify)]
internal class SendChatMsgNotifyHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SendChatMsgNotify decodedBody = packet.GetDecodedBody<SendChatMsgNotify>();
		if (decodedBody.ChatMsg.Channel == ChatMsg.MsgChannel.World)
		{
			BaseChatroom<WorldChatroom>.GetInstance().GetChatroom(session).OnSendChat(in session, decodedBody.ChatMsg);
		}
		else if (decodedBody.ChatMsg.Channel == ChatMsg.MsgChannel.Private)
		{
			PrivateChatManager.OnSendChatMsg(session, decodedBody);
		}
	}
}
