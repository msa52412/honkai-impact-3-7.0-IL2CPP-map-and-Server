using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Dlc;

/// <summary>
/// 3569 / 3570 —— 后崩坏书角色复活（爬塔里队伍全灭后用）。
///
/// 服务端不模拟战斗，也就没有真实扣费；这里只把复活次数累加进存档
/// （GetDLCTowerRsp.avatar_revive_times 会读它），回 Succ 让客户端继续战斗。
/// 原来没有 handler，点复活会 `DLCAvatarReviveReq not handled!` 并卡在结算界面。
/// </summary>
[PacketCmdId(CmdId.DLCAvatarReviveReq)]
internal class DLCAvatarReviveReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		DLCAvatarReviveReq req = packet.GetDecodedBody<DLCAvatarReviveReq>();

		Common.Database.DlcTowerScheme state = DlcTowerService.State(uid, out _);
		state.ReviveTimes++;
		state.Save();

		Packet.c.Log($"[DLCTower] DLCAvatarReviveReq uid={uid} avatar={req.AvatarId} map={req.MapId} "
			+ $"cost={req.CostType} -> Succ reviveTimes={state.ReviveTimes}");

		session.Send(Packet.FromProto(new DLCAvatarReviveRsp
		{
			retcode = DLCAvatarReviveRsp.Retcode.Succ,
			ReviveTimes = state.ReviveTimes,
			CostType = req.CostType,
			MapId = req.MapId,
			AvatarId = req.AvatarId
		}, CmdId.DLCAvatarReviveRsp));
	}
}
