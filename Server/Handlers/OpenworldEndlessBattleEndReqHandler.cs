using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 3440 —— 旧深渊单场战斗结束。
///
/// 客户端结算完立刻发这一包；服务端回 Succ 后它才会继续发 3424
/// （FinishOpenworldEndlessMonsterGroupReq）。不回包里会卡在结算界面。
/// </summary>
[PacketCmdId(CmdId.OpenworldEndlessBattleEndReq)]
internal class OpenworldEndlessBattleEndReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		OpenworldEndlessBattleEndReq req = packet.GetDecodedBody<OpenworldEndlessBattleEndReq>();
		Packet.c.Log($"[Abyss] BattleEnd type={(int)req.Type} battle={req.BattleId} "
			+ $"group={req.MonsterGroupId} layer={req.LayerNumber} status={(int)req.EndStatus} "
			+ $"cost={req.CostTime} tx={req.TransactionStr}");

		session.Send(Packet.FromProto(new OpenworldEndlessBattleEndRsp
		{
			retcode = OpenworldEndlessBattleEndRsp.Retcode.Succ
		}, CmdId.OpenworldEndlessBattleEndRsp));
	}
}
