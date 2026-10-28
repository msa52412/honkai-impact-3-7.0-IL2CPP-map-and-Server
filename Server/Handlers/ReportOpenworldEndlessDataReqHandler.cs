using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 3434/3435 —— 客户端上报旧深渊战斗数据（作弊检测/统计）。
///
/// 服务端不做判定，回 Succ 即可；retcode != 0 会让客户端把这一局当异常丢弃。
/// 日志保留 type，方便确认客户端到底在上报哪一类数据。
/// </summary>
[PacketCmdId(CmdId.ReportOpenworldEndlessDataReq)]
internal class ReportOpenworldEndlessDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		ReportOpenworldEndlessDataReq req = packet.GetDecodedBody<ReportOpenworldEndlessDataReq>();
		Packet.c.Log($"[Abyss] ReportData type={(int)req.Type} "
			+ $"battle={(req.Battle != null)} invasion={(req.Invasion != null)}");

		session.Send(Packet.FromProto(new ReportOpenworldEndlessDataRsp
		{
			retcode = ReportOpenworldEndlessDataRsp.Retcode.Succ
		}, CmdId.ReportOpenworldEndlessDataRsp));
	}
}
