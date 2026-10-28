using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Chat;

[PacketCmdId(CmdId.GetPrivateHistoryChatMsgReq)]
internal class GetPrivateHistoryChatMsgReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetPrivateHistoryChatMsgReq decodedBody = packet.GetDecodedBody<GetPrivateHistoryChatMsgReq>();
		GetPrivateHistoryChatMsgRsp getPrivateHistoryChatMsgRsp = new GetPrivateHistoryChatMsgRsp
		{
			retcode = GetPrivateHistoryChatMsgRsp.Retcode.Succ
		};
		if (decodedBody.UidLists.Length != 0)
		{
			uint[] uidLists = decodedBody.UidLists;
			foreach (uint targetUid in uidLists)
			{
				getPrivateHistoryChatMsgRsp.ChatLists.AddRange(PrivateMessage.GetMessages(session.Player.User.Uid, targetUid));
			}
		}
		else
		{
			getPrivateHistoryChatMsgRsp.ChatLists.AddRange(PrivateMessage.GetMessages(session.Player.User.Uid));
		}
		session.Send(Packet.FromProto(getPrivateHistoryChatMsgRsp, CmdId.GetPrivateHistoryChatMsgRsp));
	}
}
