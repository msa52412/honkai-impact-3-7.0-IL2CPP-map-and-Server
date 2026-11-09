using Common;
using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 神之键超限（GrandKeyBreachReq / 757）。
///
/// 超限档数按 GrandKey.json 的 breachIconList 长度决定（0..Length-1）：
/// 201 只有 1 个图标 → 不能超限；202/204 有 3 个 → 可超限 2 档。
/// 这个 handler 之前不存在，客户端发 757 服务端只会打 "not handled"。
/// </summary>
[PacketCmdId(CmdId.GrandKeyBreachReq)]
internal class GrandKeyBreachReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GrandKeyBreachReq req = packet.GetDecodedBody<GrandKeyBreachReq>();
		GrandKeys.GrandKeysScheme scheme = GrandKeyService.Scheme(session.Player);
		GrandKeyService.Sync(session.Player, scheme);

		GrandKey? key = scheme.Find(req.KeyId);
		if (key == null)
		{
			Packet.c.Warn($"[GrandKey] 超限失败：key {req.KeyId} 未解锁");
			session.Send(Packet.FromProto(new GrandKeyBreachRsp
			{
				retcode = GrandKeyBreachRsp.Retcode.Fail,
				KeyId = req.KeyId
			}, CmdId.GrandKeyBreachRsp));
			return;
		}

		uint before = key.BreachLevel;
		uint after = scheme.BreachGrandKey(req.KeyId);
		if (after == 0u)
		{
			// 已经到超限上限。**不要回 Fail** —— 客户端对任何 retcode != 0 都会弹一个
			// 通用错误框，而 GrandKeyBreachRsp.Retcode 没有本地化文本，界面上会直接显示
			// 「proto.GrandKeyBreachRsp+RetcodeFAIL」这种原始枚举名。
			// 状态本来就已经是满的，回 SUCC + 当前值即可（幂等，不改数据、不推列表）。
			Packet.c.Log($"[GrandKey] key {req.KeyId} 已到超限上限（{before}），回 SUCC 不报错");
			session.Send(Packet.FromProto(new GrandKeyBreachRsp
			{
				retcode = GrandKeyBreachRsp.Retcode.Succ,
				KeyId = req.KeyId,
				BreachLevel = before
			}, CmdId.GrandKeyBreachRsp));
			return;
		}

		scheme.Save();
		Packet.c.Log($"[GrandKey] key {req.KeyId} 超限 {before} -> {after}");
		session.Send(Packet.FromProto(new GrandKeyBreachRsp
		{
			retcode = GrandKeyBreachRsp.Retcode.Succ,
			KeyId = req.KeyId,
			BreachLevel = after
		}, CmdId.GrandKeyBreachRsp));

		GrandKeyService.PushMainInfo(session);
	}
}
