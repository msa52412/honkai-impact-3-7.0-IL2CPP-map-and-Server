using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Dlc;

/// <summary>
/// 3552 / 3553 —— 后崩坏书角色天赋升级。
///
/// 服务端把天赋按表满级下发（DlcService.TalentLevel / run\openworld.json 的 dlc_talent_level），
/// 正常点不到「升级」；但一键升满、红点引导等入口仍可能发这条请求，
/// 没有 handler 就会 `LevelUpDLCAvatarTalentReq not handled!` 并让按钮一直转。
///
/// 这里真正落库（DlcAvatars.Talents）：只升不降，且不超过该天赋在表里的上限。
/// Rsp 只有 retcode + talent_id，客户端按 talent_id 定位天赋项再刷新。
/// </summary>
[PacketCmdId(CmdId.LevelUpDLCAvatarTalentReq)]
internal class LevelUpDLCAvatarTalentReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		LevelUpDLCAvatarTalentReq req = packet.GetDecodedBody<LevelUpDLCAvatarTalentReq>();

		uint now = DlcService.LevelUp(uid, req.TalentId, req.TargetLevel, DlcService.TargetLevel);
		if (now == 0u)
		{
			Packet.c.Log($"[DLC] LevelUpDLCAvatarTalentReq uid={uid} talent={req.TalentId} target={req.TargetLevel} -> 表里没有该天赋，回 Succ 但不落库");
		}
		else
		{
			Packet.c.Log($"[DLC] LevelUpDLCAvatarTalentReq uid={uid} talent={req.TalentId} target={req.TargetLevel} -> lv={now}");
		}

		session.Send(Packet.FromProto(new LevelUpDLCAvatarTalentRsp
		{
			retcode = LevelUpDLCAvatarTalentRsp.Retcode.Succ,
			TalentId = req.TalentId
		}, CmdId.LevelUpDLCAvatarTalentRsp));
	}
}
