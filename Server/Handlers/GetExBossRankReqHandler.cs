using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetExBossRankReq)]
internal class GetExBossRankReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetExBossRankReq decodedBody = packet.GetDecodedBody<GetExBossRankReq>();
		GetExBossRankRsp proto = new GetExBossRankRsp
		{
			retcode = GetExBossRankRsp.Retcode.Succ,
			BossId = decodedBody.BossId,
			RankId = decodedBody.RankId,
			RankData = new RankShowData
			{
				MyScore = 0u,
				MyRank = 1u,
				MyRankType = 1u
			}
		};
		session.Send(Packet.FromProto(proto, CmdId.GetExBossRankRsp));
	}
}
