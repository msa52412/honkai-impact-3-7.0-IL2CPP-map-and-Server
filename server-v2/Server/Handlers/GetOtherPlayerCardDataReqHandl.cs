using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers.Two;

[PacketCmdId(CmdId.GetOtherPlayerCardDataReq)]
internal class GetOtherPlayerCardDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetOtherPlayerCardDataReq decodedBody = packet.GetDecodedBody<GetOtherPlayerCardDataReq>();
		UserScheme userScheme = User.FromUid(decodedBody.TargetUid);
		GetOtherPlayerCardDataRsp getOtherPlayerCardDataRsp = new GetOtherPlayerCardDataRsp
		{
			retcode = GetOtherPlayerCardDataRsp.Retcode.Succ,
			TargetUid = decodedBody.TargetUid
		};
		if (userScheme != null)
		{
			Player player = Session.FromUid(userScheme.Uid)?.Player;
			if (player == null)
			{
				player = new Player(userScheme);
			}
			getOtherPlayerCardDataRsp.CardData = player.GetCardData();
			getOtherPlayerCardDataRsp.PlayerData = player.GetDetailData();
		}
		else
		{
			getOtherPlayerCardDataRsp.retcode = GetOtherPlayerCardDataRsp.Retcode.Fail;
		}
		session.Send(Packet.FromProto(getOtherPlayerCardDataRsp, CmdId.GetOtherPlayerCardDataRsp));
	}
}
