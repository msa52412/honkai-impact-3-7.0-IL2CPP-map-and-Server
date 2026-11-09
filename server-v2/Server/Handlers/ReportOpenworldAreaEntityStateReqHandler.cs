using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

/// <summary>
/// 4542 / 4543 —— 客户端上报开放世界区域实体的状态（传送点解锁、宝箱打开、门开启）。
///
/// 原来服务端没接这个包（日志 `ReportOpenworldAreaEntityStateReq not handled!`），
/// 客户端报完就丢，下次 `GetOpenworldMapRsp` 里 `area_entity_state_list` 还是空的，
/// 界面表现就是「地图上只有一个传送点」——除了出生点，其它锚点全回到未解锁。
///
/// 这里把上报的状态落到 OpenWorldScheme.EntityStates，并在应答里把该区域的全部状态回给客户端；
/// GetOpenworldMapReqHandler 也会把它们放进 `area_entity_state_list`。
/// </summary>
[PacketCmdId(CmdId.ReportOpenworldAreaEntityStateReq)]
internal class ReportOpenworldAreaEntityStateReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		ReportOpenworldAreaEntityStateReq req = packet.GetDecodedBody<ReportOpenworldAreaEntityStateReq>();
		uint uid = session.Player.User.Uid;

		OpenWorldScheme scheme = session.Player.OpenWorlds.FirstOrDefault(x => x.MapId == req.MapId);
		if (scheme == null)
		{
			scheme = new OpenWorldScheme
			{
				OwnerUid = uid,
				MapId = req.MapId,
				Cycle = 0u,
				QuestLevel = 1u,
				HasTakeFinishRewardCycle = 0u,
				SpawnPoint = "",
				EntityStates = new List<OpenWorldEntityStateRow>()
			};
			OpenWorld.collection.InsertOne(scheme);
		}

		// force_entity_state > 0 时统一改写（后崩坏书 2 的传送点要 2 = Unlocked 才能点）
		uint state = OwMapTuning.ForceEntityState != 0u ? OwMapTuning.ForceEntityState : req.EntityState;
		bool dirty = scheme.SetEntityState(req.AreaId, req.EntityId, state);
		if (dirty)
		{
			scheme.Save();
		}

		ReportOpenworldAreaEntityStateRsp rsp = new ReportOpenworldAreaEntityStateRsp
		{
			retcode = ReportOpenworldAreaEntityStateRsp.Retcode.Succ,
			MapId = req.MapId,
			AreaId = req.AreaId,
			EntityId = req.EntityId
		};

		OpenworldAreaEntityState area = new OpenworldAreaEntityState
		{
			AreaId = req.AreaId
		};
		foreach (OpenWorldEntityStateRow row in scheme.States())
		{
			if (row.AreaId == req.AreaId)
			{
				area.EntityStateLists.Add(new OpenworldEntityState
				{
					EntityId = row.EntityId,
					EntityState = OwMapTuning.ForceEntityState != 0u ? OwMapTuning.ForceEntityState : row.EntityState
				});
			}
		}
		rsp.AreaEntityStateLists.Add(area);

		Packet.c.Log($"[OW] 实体状态 map={req.MapId} area={req.AreaId} entity={req.EntityId} "
			+ $"state={req.EntityState} 存档{(dirty ? "已更新" : "无变化")} 该区域共 {area.EntityStateLists.Count} 条");

		session.Send(Packet.FromProto(rsp, CmdId.ReportOpenworldAreaEntityStateRsp));
	}
}
