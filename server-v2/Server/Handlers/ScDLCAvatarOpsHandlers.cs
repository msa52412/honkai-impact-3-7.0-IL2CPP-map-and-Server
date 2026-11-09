using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers.Openworld;

/// <summary>
/// 6314 / 6315 —— 后崩坏书 2「装备技能槽」。
///
/// 原来没有 handler（日志 `EquipScDLCAvatarTalentReq not handled!`），
/// 客户端点了没回包，界面就一直等。装备结果要落库，
/// 否则下次 GetScDLCReq 会按默认值重建，用户选的技能又被冲掉。
/// </summary>
[PacketCmdId(CmdId.EquipScDLCAvatarTalentReq)]
internal class EquipScDLCAvatarTalentReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		EquipScDLCAvatarTalentReq req = packet.GetDecodedBody<EquipScDLCAvatarTalentReq>();

		ScDlcAvatarScheme scheme = ScDlcAvatar.GetOrCreate(uid, req.AvatarId);
		scheme.Equipped = req.EquipTalentLists ?? System.Array.Empty<uint>();
		scheme.Save();

		Packet.c.Log($"[ScDLC] EquipTalent avatar={req.AvatarId} "
			+ $"equip=[{string.Join(",", scheme.Equipped)}] -> Succ");

		session.Send(Packet.FromProto(new EquipScDLCAvatarTalentRsp
		{
			retcode = EquipScDLCAvatarTalentRsp.Retcode.Succ
		}, CmdId.EquipScDLCAvatarTalentRsp));
	}
}

/// <summary>
/// 6310 / 6311 —— 后崩坏书 2「角色升级」。
///
/// 服务端默认已经把角色拉到表内最高等级，所以正常情况下这里只是把 retcode 回 SUCC，
/// 避免客户端点一下就没回包。低于上限时才会真的 +1 并落库。
/// </summary>
[PacketCmdId(CmdId.LevelUpScDLCAvatarReq)]
internal class LevelUpScDLCAvatarReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		LevelUpScDLCAvatarReq req = packet.GetDecodedBody<LevelUpScDLCAvatarReq>();

		ScDlcAvatarScheme scheme = ScDlcAvatar.GetOrCreate(uid, req.AvatarId);
		uint max = GetScDLCReqHandler.ResolveAvatarLevel();
		bool leveled = false;
		if (scheme.Level < max)
		{
			scheme.Level++;
			scheme.Save();
			leveled = true;
		}

		Packet.c.Log($"[ScDLC] LevelUpAvatar avatar={req.AvatarId} level={scheme.Level}/{max} "
			+ $"{(leveled ? "已升级" : "已满级")} -> Succ");

		session.Send(Packet.FromProto(new LevelUpScDLCAvatarRsp
		{
			retcode = LevelUpScDLCAvatarRsp.Retcode.Succ
		}, CmdId.LevelUpScDLCAvatarRsp));
	}
}

/// <summary>6312 / 6313 —— 后崩坏书 2「天赋升级」。同样默认已满级，只保证有回包。</summary>
[PacketCmdId(CmdId.LevelUpScDLCTalentReq)]
internal class LevelUpScDLCTalentReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		LevelUpScDLCTalentReq req = packet.GetDecodedBody<LevelUpScDLCTalentReq>();

		ScDlcAvatarScheme scheme = ScDlcAvatar.GetOrCreate(uid, req.AvatarId);
		ScDlcTalentState state = scheme.Talent(req.TalentId);
		int maxLv = ScDLCTalentLevelData.GetInstance().MaxLevel(req.TalentId);

		bool leveled = false;
		if (state == null && maxLv > 0)
		{
			state = new ScDlcTalentState { TalentId = req.TalentId, Level = 1u };
			scheme.Talents.Add(state);
			leveled = true;
		}
		else if (state != null && state.Level < maxLv)
		{
			state.Level++;
			leveled = true;
		}

		if (leveled)
		{
			scheme.Save();
		}

		Packet.c.Log($"[ScDLC] LevelUpTalent avatar={req.AvatarId} talent={req.TalentId} "
			+ $"level={state?.Level ?? 0u}/{maxLv} {(leveled ? "已升级" : "已满级")} -> Succ");

		session.Send(Packet.FromProto(new LevelUpScDLCTalentRsp
		{
			retcode = LevelUpScDLCTalentRsp.Retcode.Succ
		}, CmdId.LevelUpScDLCTalentRsp));
	}
}
