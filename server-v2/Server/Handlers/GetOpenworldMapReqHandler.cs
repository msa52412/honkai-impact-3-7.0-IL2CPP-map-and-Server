using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers.Openworld;

[PacketCmdId(CmdId.GetOpenworldMapReq)]
internal class GetOpenworldMapReqHandler : IPacketHandler
{
	/// <summary>已打过日志的 mapId，避免刷屏。</summary>
	private static readonly HashSet<uint> Logged = new HashSet<uint>();

	public void Handle(Session session, Packet packet)
	{
		GetOpenworldMapReq Data = packet.GetDecodedBody<GetOpenworldMapReq>();
		OpenWorldScheme openWorldScheme = session.Player.OpenWorlds.FirstOrDefault((OpenWorldScheme x) => x.MapId == Data.MapId);

		bool first;
		lock (Logged)
		{
			first = Logged.Add(Data.MapId);
		}

		if (openWorldScheme == null)
		{
			// 原来这里直接 return —— 客户端一个字节都收不到，只能干等到超时。
			// 开放世界页面就是这么被判成「空」然后 2 秒退出的。
			// 现在改成：现场补一条临时 scheme 并正常应答，同时把 mapId 打出来。
			openWorldScheme = new OpenWorldScheme
			{
				OwnerUid = session.Player.User.Uid,
				MapId = Data.MapId,
				Cycle = 0u,
				QuestLevel = 1u,
				HasTakeFinishRewardCycle = 0u,
				SpawnPoint = ""
			};
			Packet.c.Log($"[Abyss] GetOpenworldMapReq mapId={Data.MapId} 存档里没有 → 临时补一条");
		}
		else if (first)
		{
			Packet.c.Log($"[OW] GetOpenworldMapReq mapId={Data.MapId} cycle={openWorldScheme.Cycle} spawn='{openWorldScheme.SpawnPoint}'");
		}

		// 周期完成奖励：客户端用 has_take_finish_reward_cycle 和 Cycle 比较，
		// 不相等就弹「奖励领取」。拉到该地图配置的最大周期（见 OwCycleReward 注释）。
		OwCycleReward.Apply(openWorldScheme);

		// 传送点补齐：按 OpenWorldArea.switchPoint 把本图所有切换点并进 spawn_point，
		// 否则没去过的区域永远进不去（地图页上「部分区域没开放」）。
		OwSpawnPoints.Fill(openWorldScheme);

		// Status 原来是写死的 3u = OpenworldMapStatusLock（永久锁定），
		// 「进入」按钮永远灰着。改成由 run\openworld.json 的 map_status 控制，默认 1 = ACTIVATE。
		uint status = OwMapTuning.Status;

		// 委托面板数据（quest_data）：原来写死成 is_open=false + 空 quest_list，
		// 客户端 MonoOpenWorldQuestMapUIManager 拿不到委托，进委托界面就一直转圈。
		// 现在从委托存档里取真实槽位。
		OwQuestScheme questState = OwQuest.GetOrCreate(session.Player.User.Uid, Data.MapId);
		OwQuestService.EnsureSlots(questState, session.Player.User);

		GetOpenworldMapRsp proto = new GetOpenworldMapRsp
		{
			retcode = GetOpenworldMapRsp.Retcode.Succ,
			MapId = Data.MapId,
			Cycle = openWorldScheme.Cycle,
			EventRandomSeed = Global.GetRandomSeed(),
			SpawnPoint = openWorldScheme.SpawnPoint,
			Status = status,
			QuestData = OwQuestService.BuildQuestData(questState),
			TechData = SakuraServant.Snapshot(session.Player.User, Data.MapId, openWorldScheme),
			FinishOnceEventLists = openWorldScheme.SakuraGatheredOnce?.ToArray(),
			HasTakeFinishRewardCycle = openWorldScheme.HasTakeFinishRewardCycle
		};
		var partnerQuest = SakuraServant.CurrentQuest(openWorldScheme);
		if (partnerQuest != null)
			proto.QuestData.KeyQuest = OwQuestService.ToProto(questState, partnerQuest);
		if (openWorldScheme.CompletedPartnerQuests != null)
			proto.QuestData.FinishKeyQuestLists = openWorldScheme.CompletedPartnerQuests.ToArray();

		// 区域实体状态（传送点 / 宝箱 / 门）。客户端拿它决定地图上哪些锚点已经解锁；
		// 不填就是「只有一个传送点」。数据来自客户端自己的 ReportOpenworldAreaEntityStateReq。
		foreach (IGrouping<uint, OpenWorldEntityStateRow> grp in openWorldScheme.States().GroupBy(x => x.AreaId))
		{
			OpenworldAreaEntityState area = new OpenworldAreaEntityState
			{
				AreaId = grp.Key
			};
			foreach (OpenWorldEntityStateRow row in grp)
			{
				area.EntityStateLists.Add(new OpenworldEntityState
				{
					EntityId = row.EntityId,
					// force_entity_state > 0 时统一改写（后崩坏书 2 传送点要 2 = Unlocked）
					EntityState = OwMapTuning.ForceEntityState != 0u ? OwMapTuning.ForceEntityState : row.EntityState
				});
			}
			proto.AreaEntityStateLists.Add(area);
		}

		// 把下发出去的每个槽的表格明细也打出来，方便和界面上的名字/星级/奖励对照，
		// 判断是「服务端下发错」还是「客户端查不到这张表」。
		if (proto.QuestData != null && proto.QuestData.QuestLists.Count > 0)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			foreach (OpenworldQuest q in proto.QuestData.QuestLists)
			{
				Common.Utils.ExcelReader.OpenWorldQuestDataExcel def = OwQuestService.Def(Data.MapId, q.Id);
				sb.Append(string.Format(" [pos={0} id={1} st={2} rw={3} rar={4} area={5} rr={6}]",
					q.Position, q.Id, (uint)q.Status, q.RarityRewardId,
					(def?.Rarity ?? -1), (def?.QuestArea ?? -1), (def?.RewardId ?? -1)));
			}

			Packet.c.Log($"[OWQuest] quild_quest_data map={Data.MapId} status={status} lv={proto.QuestData.Level}/{proto.QuestData.MaxQuestLevel} open={proto.QuestData.IsOpen} n={proto.QuestData.QuestLists.Count}{sb}");
		}

		session.Send(Packet.FromProto(proto, CmdId.GetOpenworldMapRsp));
	}
}
