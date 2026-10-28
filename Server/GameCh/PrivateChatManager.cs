using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Game.Chatrooms;

public static class PrivateChatManager
{
	public static void OnSendChatMsg(Session session, SendChatMsgNotify chatMsgNotify)
	{
		ChatMsg chatMsg = chatMsgNotify.ChatMsg;
		string text = chatMsg.Content.Items.Where((ChatMsgItem item) => item.MsgStr != null).FirstOrDefault()?.MsgStr;
		if (text != null)
		{
			chatMsg.CheckResult = new ChatMsgSensitiveCheckResult
			{
				ShieldType = 0,
				NumberCheck = 0,
				RewriteText = text
			};
			chatMsg.Msg = text;
		}
		UserScheme User = session.Player.User;
		chatMsg.Uid = User.Uid;
		chatMsg.Nickname = User.Nick;
		chatMsg.Time = (uint)Global.GetUnixInSeconds();
		chatMsg.AvatarId = (uint)User.AssistantAvatarId;
		chatMsg.DressId = session.Player.AvatarList.Where((AvatarScheme avatar) => avatar.AvatarId == User.AssistantAvatarId).First().DressId;
		chatMsg.FrameId = ((User.FrameId < 200001) ? 200001u : ((uint)User.FrameId));
		chatMsg.CustomHeadId = (uint)User.CustomHeadId;
		uint[] targetUidLists = chatMsgNotify.TargetUidLists;
		foreach (uint targetUid in targetUidLists)
		{
			SendPrivateMessage(session, targetUid, chatMsg);
		}
	}

	public static void SendPrivateMessage(Session session, uint targetUid, ChatMsg chatMsg)
	{
		PrivateMessageScheme privateMessageScheme = PrivateMessage.Create(session.Player.User.Uid, targetUid, chatMsg);
		Session session2 = Session.FromUid(targetUid);
		RecvChatMsgNotify recvChatMsgNotify = new RecvChatMsgNotify();
		recvChatMsgNotify.ChatMsgLists.Add(privateMessageScheme.Msg);
		session.Send(Packet.FromProto(recvChatMsgNotify, CmdId.RecvChatMsgNotify));
		session2?.Send(Packet.FromProto(recvChatMsgNotify, CmdId.RecvChatMsgNotify));
	}
}
