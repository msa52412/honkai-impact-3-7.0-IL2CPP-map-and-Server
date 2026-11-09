using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Dlc;

/// <summary>
/// 3554 / 3555 —— 装备/切换角色天赋（界面里的「装配」「专精二选一」）。
///
/// 装备列表在 GetDLCAvatarRsp.avatar_list[].equip_talent_list，服务端落库到
/// DlcAvatars.Equipped。客户端点切换时发这条请求；原来没有 handler
/// → `EquipDLCAvatarTalentReq not handled!`，OnEquipTalentRsp（0x182E32D20）
/// 收不到，界面停在旧状态（它只判断 retcode == Succ 然后 RefreshActivationView）。
/// </summary>
[PacketCmdId(CmdId.EquipDLCAvatarTalentReq)]
internal class EquipDLCAvatarTalentReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		EquipDLCAvatarTalentReq req = packet.GetDecodedBody<EquipDLCAvatarTalentReq>();

		DlcService.Equip(uid, req.AvatarId, req.EquipTalentLists, DlcService.TargetLevel);

		Packet.c.Log($"[DLC] EquipDLCAvatarTalentReq uid={uid} avatar={req.AvatarId} "
			+ $"equip=[{string.Join(",", req.EquipTalentLists ?? System.Array.Empty<uint>())}] -> Succ");

		session.Send(Packet.FromProto(new EquipDLCAvatarTalentRsp
		{
			retcode = EquipDLCAvatarTalentRsp.Retcode.Succ
		}, CmdId.EquipDLCAvatarTalentRsp));
	}
}

/// <summary>
/// 3588 / 3589 —— 切换「二选一」类型的角色天赋（IsNeedSwitch 的那些）。
/// 语义等同装备：把该天赋加进 / 移出 equip 列表。
/// </summary>
[PacketCmdId(CmdId.SwitchDLCAvatarTalentReq)]
internal class SwitchDLCAvatarTalentReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		SwitchDLCAvatarTalentReq req = packet.GetDecodedBody<SwitchDLCAvatarTalentReq>();

		uint dlcLevel = DlcService.TargetLevel;
		uint[] equipped = DlcService.EquippedOf(uid, (int)req.AvatarId, dlcLevel);

		bool exists = false;
		foreach (uint id in equipped)
		{
			if (id == req.TalentId)
			{
				exists = true;
				break;
			}
		}

		uint[] next;
		if (exists)
		{
			next = System.Array.FindAll(equipped, (uint x) => x != req.TalentId);
		}
		else
		{
			next = new uint[equipped.Length + 1];
			System.Array.Copy(equipped, next, equipped.Length);
			next[equipped.Length] = req.TalentId;
		}

		DlcService.Equip(uid, req.AvatarId, next, dlcLevel);

		Packet.c.Log($"[DLC] SwitchDLCAvatarTalentReq uid={uid} avatar={req.AvatarId} talent={req.TalentId} "
			+ $"{(exists ? "卸下" : "装备")} -> [{string.Join(",", next)}]");

		session.Send(Packet.FromProto(new SwitchDLCAvatarTalentRsp
		{
			retcode = SwitchDLCAvatarTalentRsp.Retcode.Succ
		}, CmdId.SwitchDLCAvatarTalentRsp));
	}
}

/// <summary>
/// 3584 / 3585 —— 天赋「词条（affix）」选择。
///
/// 刷新出来的是 wait_select_affix_set_id，玩家点「替换」才发 is_select = true 转正，
/// 点「取消」发 false 丢弃。客户端实际逻辑是取 proto 的 affix_set_id 统计套装件数
/// （0x180626CC0 GetApplicableAffixSetList），所以这里必须真的落库。
/// </summary>
[PacketCmdId(CmdId.SelectDLCTalentAffixReq)]
internal class SelectDLCTalentAffixReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		SelectDLCTalentAffixReq req = packet.GetDecodedBody<SelectDLCTalentAffixReq>();

		DlcService.SelectAffix(uid, req.AvatarId, req.TalentId, req.IsSelect, DlcService.TargetLevel);

		Packet.c.Log($"[DLC] SelectDLCTalentAffixReq uid={uid} avatar={req.AvatarId} talent={req.TalentId} "
			+ $"select={req.IsSelect} -> Succ");

		session.Send(Packet.FromProto(new SelectDLCTalentAffixRsp
		{
			retcode = SelectDLCTalentAffixRsp.Retcode.Succ
		}, CmdId.SelectDLCTalentAffixRsp));
	}
}

/// <summary>
/// 3580 / 3581 —— 刷新天赋词条。
/// 服务端从 DLCTalentAffixSet（1..5）里随机一个塞进 wait，回给客户端；
/// 客户端拿到 new_affix_set_id 后显示「新词条」，等玩家确认。
/// 只有类型 4（武器/特技）有词条，其他类型回 0。
/// </summary>
[PacketCmdId(CmdId.RefreshDLCTalentAffixReq)]
internal class RefreshDLCTalentAffixReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		RefreshDLCTalentAffixReq req = packet.GetDecodedBody<RefreshDLCTalentAffixReq>();

		uint newId = DlcService.RefreshAffix(uid, req.AvatarId, req.TalentId, DlcService.TargetLevel);

		Packet.c.Log($"[DLC] RefreshDLCTalentAffixReq uid={uid} avatar={req.AvatarId} talent={req.TalentId} "
			+ $"-> newAffixSet={newId}");

		session.Send(Packet.FromProto(new RefreshDLCTalentAffixRsp
		{
			retcode = RefreshDLCTalentAffixRsp.Retcode.Succ,
			NewAffixSetId = newId
		}, CmdId.RefreshDLCTalentAffixRsp));
	}
}
