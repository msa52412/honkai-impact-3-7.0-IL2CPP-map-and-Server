using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers.Dlc;

/// <summary>
/// 3559 / 3560 —— 后崩坏书可选角色列表（含角色天赋/技能/词条）。
///
/// 客户端 OnGetDLCAvatarRsp（0x1827B3AE0）拿到 avatar_list 后，按 avatar_id 建
/// DLCAvatarDataItem 存进字典；IsAvatarUnlock(id)（0x1827B40B0）只判断 id 在不在字典里。
///
/// 天赋列表（talent_list）是**唯一来源**：客户端
///   0x1806276A0 GetTalentListByType  → 遍历 proto 的 talent_list，按 DLCTalentData.TalentType 过滤
///   0x1806273A0 GetTalentDataItem    → 在 talent_list 里按 talent_id 找，没有就传 null
///   0x181EA5E80 OHHANBBFFIA 构造      → level = proto.level，上限取 DLCTalentLevelData
///   0x181EA3B10（词条套装 id）        → proto.affix_set_id
/// 表里 99 行 = 3 角色 × 33 个天赋，不下发就等于没有这个天赋。
///
/// 等级/词条按存档下发（DlcService.EnsureAvatar 会在首次访问时按调参灌满级）。
/// </summary>
[PacketCmdId(CmdId.GetDLCAvatarReq)]
internal class GetDLCAvatarReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		uint dlcLevel = DlcService.TargetLevel;

		GetDLCAvatarRsp rsp = new GetDLCAvatarRsp
		{
			retcode = GetDLCAvatarRsp.Retcode.Succ
		};

		List<string> logs = new List<string>();

		foreach (DLCAvatarDataExcel row in DLCAvatarData.GetInstance().All.OrderBy((DLCAvatarDataExcel x) => x.Index))
		{
			if (row.DLCAvatarID == 0)
			{
				continue;
			}

			DLCAvatar avatar = new DLCAvatar
			{
				AvatarId = (uint)row.DLCAvatarID,
				EquipTalentLists = DlcService.EquippedOf(uid, row.DLCAvatarID, dlcLevel)
			};

			List<DLCAvatarTalent> talents = DlcService.BuildTalentList(uid, row.DLCAvatarID, dlcLevel);
			avatar.TalentLists.AddRange(talents);

			rsp.AvatarLists.Add(avatar);

			int affixCount = talents.Count(t => t.AffixSetId != 0u);
			logs.Add($"{row.DLCAvatarID}(talent={talents.Count},affix={affixCount},equip={avatar.EquipTalentLists.Length})");
		}

		Packet.c.Log($"[DLC] GetDLCAvatarReq uid={uid} -> Succ avatars=[{string.Join(",", logs)}] dlcLevel={dlcLevel}");
		session.Send(Packet.FromProto(rsp, CmdId.GetDLCAvatarRsp));
	}
}
