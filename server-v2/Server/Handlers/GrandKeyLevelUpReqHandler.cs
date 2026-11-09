using Common;
using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 神之键升级（GrandKeyLevelUpReq / 753）。
///
/// 老实现是空函数（不回包），客户端会一直等。
/// 现在按 GrandKeyLevel.json / maxGrandKeyLv 限上限，升完再推一份完整列表，
/// 让客户端触发 Notify(656 GrandKeyUpdate) 刷新界面。
/// </summary>
[PacketCmdId(CmdId.GrandKeyLevelUpReq)]
internal class GrandKeyLevelUpReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GrandKeyLevelUpReq req = packet.GetDecodedBody<GrandKeyLevelUpReq>();
		GrandKeys.GrandKeysScheme scheme = GrandKeyService.Scheme(session.Player);
		GrandKeyService.Sync(session.Player, scheme);

		GrandKey? key = scheme.Find(req.KeyId);
		if (key == null)
		{
			Packet.c.Warn($"[GrandKey] 升级失败：key {req.KeyId} 未解锁");
			session.Send(Packet.FromProto(new GrandKeyLevelUpRsp
			{
				retcode = GrandKeyLevelUpRsp.Retcode.Fail,
				KeyId = req.KeyId
			}, CmdId.GrandKeyLevelUpRsp));
			return;
		}

		uint before = key.Level;
		uint after = scheme.LevelUpGrandKey(req.KeyId);
		if (after == 0u)
		{
			// 已经满级。同 GrandKeyBreachReqHandler 的理由：客户端对任何 retcode != 0
			// 都会弹通用错误框并把枚举名原样打出来（proto.GrandKeyLevelUpRsp+RetcodeMAX_LEVEL），
			// 所以这里回 SUCC + 当前等级，不报错、不改数据、不推列表。
			uint max = GrandKeyTuning.MaxLevelOverride != 0u
				? GrandKeyTuning.MaxLevelOverride
				: GrandKeys.GrandKeysScheme.MaxLevel((int)req.KeyId);
			Packet.c.Log($"[GrandKey] key {req.KeyId} 已满级（{before}/{max}），回 SUCC 不报错");
			session.Send(Packet.FromProto(new GrandKeyLevelUpRsp
			{
				retcode = GrandKeyLevelUpRsp.Retcode.Succ,
				KeyId = req.KeyId,
				Level = before
			}, CmdId.GrandKeyLevelUpRsp));
			return;
		}

		scheme.Save();
		Packet.c.Log($"[GrandKey] key {req.KeyId} 升级 {before} -> {after}");
		session.Send(Packet.FromProto(new GrandKeyLevelUpRsp
		{
			retcode = GrandKeyLevelUpRsp.Retcode.Succ,
			KeyId = req.KeyId,
			Level = after
		}, CmdId.GrandKeyLevelUpRsp));

		// 客户端对这个 Rsp 的处理是空函数，必须再推一份完整列表才会刷新界面。
		GrandKeyService.PushMainInfo(session);
	}
}
