using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 3430/3431 —— 击杀「入侵怪」。
///
/// 旧深渊的入侵玩法：别的玩家放怪进你的关卡，杀掉给分。
/// 服务端没有入侵系统，这里回 Succ + 当前总分（Score），SubScore = 0。
/// 客户端拿到 Score 会刷新总分，不会因为 SubScore=0 报错。
/// </summary>
[PacketCmdId(CmdId.FinishOpenworldEndlessInvasionMonsterReq)]
internal class FinishOpenworldEndlessInvasionMonsterReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		FinishOpenworldEndlessInvasionMonsterReq req = packet.GetDecodedBody<FinishOpenworldEndlessInvasionMonsterReq>();

		uint score = 0u;
		OwEndlessState? state = OwEndlessState.Find(session.Player.User.Uid, (uint)AbyssConfig.CurStatusType);
		if (state != null)
		{
			lock (state.Gate)
			{
				score = state.Score;
			}
		}

		Packet.c.Log($"[Abyss] FinishInvasion id={req.Id} battle={req.BattleId} tx={req.TransactionStr} score={score}");

		session.Send(Packet.FromProto(new FinishOpenworldEndlessInvasionMonsterRsp
		{
			retcode = FinishOpenworldEndlessInvasionMonsterRsp.Retcode.Succ,
			Id = req.Id,
			BattleId = req.BattleId,
			TransactionStr = req.TransactionStr,
			Score = score,
			SubScore = 0u
		}, CmdId.FinishOpenworldEndlessInvasionMonsterRsp));
	}
}
