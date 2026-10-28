using System.Linq;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game.Chatrooms;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.EnterWorldChatroomReq)]
internal class EnterWorldChatroomReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		EnterWorldChatroomReq decodedBody = packet.GetDecodedBody<EnterWorldChatroomReq>();
		BaseChatroom<WorldChatroom>.GetInstance().Left(session);
		Chatroom chatroom = BaseChatroom<WorldChatroom>.GetInstance().Join(in session, (decodedBody.ChatroomId == 0) ? 1u : decodedBody.ChatroomId);
		EnterWorldChatroomRsp enterWorldChatroomRsp = new EnterWorldChatroomRsp
		{
			retcode = EnterWorldChatroomRsp.Retcode.Succ,
			ChatroomId = chatroom.Id,
			ActivityType = ActivityWorldChatroomType.ActivityWorldChatroomTypeNone,
			PlayerNum = (uint)chatroom.Members.Count
		};
		enterWorldChatroomRsp.HisChatMsgLists.AddRange(chatroom.Messages.Skip(chatroom.Messages.Count - 5).ToList());
		session.Send(Packet.FromProto(enterWorldChatroomRsp, CmdId.EnterWorldChatroomRsp));
	}
}
