using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 3428/3429 —— 触发场景陷阱。
///
/// 客户端在关卡里踩到陷阱就上报 (unique_id, item_id)。
/// 反汇编（OWEndlessModule.CreateOpenWorldEndlessTrap @0x181897AA0 / GetTrapInfo @0x181899620）
/// 说明 unique_id 是陷阱的全局唯一号，服务端在 3427 的 trap_list 里给过，客户端原样带回来。
///
/// 这里做两件事：
///   1. 从状态里把这个陷阱移除（不然它会一直躺在 3427 的 trap_list 里，玩家每次进图都踩同一个）
///   2. 回 Succ；找不到也回 Succ（重连重复上报不该弹网络错误）
/// </summary>
[PacketCmdId(CmdId.TriggerOpenworldEndlessTrapReq)]
internal class TriggerOpenworldEndlessTrapReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		TriggerOpenworldEndlessTrapReq req = packet.GetDecodedBody<TriggerOpenworldEndlessTrapReq>();
		uint uid = session.Player.User.Uid;
		uint type = AbyssConfig.SelectedEndlessType == 3u ? 3u : 2u;

		Packet.c.Log($"[Abyss] TriggerTrap unique={req.UniqueId} item={req.ItemId}");

		bool ok = OwEndlessItems.TriggerTrap(uid, type, req.UniqueId, req.ItemId);
		OwEndlessItemState.Persist();

		session.Send(Packet.FromProto(new TriggerOpenworldEndlessTrapRsp
		{
			retcode = ok ? TriggerOpenworldEndlessTrapRsp.Retcode.Succ : TriggerOpenworldEndlessTrapRsp.Retcode.Fail
		}, CmdId.TriggerOpenworldEndlessTrapRsp));
	}
}
