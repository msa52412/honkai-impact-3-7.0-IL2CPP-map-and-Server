using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers.Openworld;

/// <summary>
/// 6332 / 6333 —— 后崩坏书 2（DLC2）爬塔主数据。
///
/// 原来服务端没有 handler（日志 `GetScDLCTowerReq not handled!`），
/// 开放世界列表里点「进入」这张图时，客户端在 GetScDLCReq 之后等这个包，
/// 等不到就一直转圈。
///
/// 关键点：
///   1. retcode 必须是 SUCC —— DLC2TowerPageContext.OnShowPageGetScDLCTowerRsp（0x181247B00）
///      里 `retcode != 0` 会直接关页（`0x18303df20(ctx, 0)`）。
///   2. schedule_id 必须是 ScDLCTowerSchedule.json 里真实存在的 id，
///      否则客户端 `[+0x48]` 赛季元数据为 null，天气 / 加成 / 总层数全丢。
///   3. avatar_info_list 要给满，否则队伍界面全是残血。
/// </summary>
[PacketCmdId(CmdId.GetScDLCTowerReq)]
internal class GetScDLCTowerReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;

		GetScDLCTowerRsp rsp = ScDlcTowerService.BuildRsp(uid, out ScDlcTowerScheme state,
			out ScDLCTowerScheduleDataExcel schedule);

		Packet.c.Log($"[ScDlcTower] GetScDLCTowerReq uid={uid} -> Succ schedule={rsp.ScheduleId} "
			+ $"(config={schedule?.ConfigType} maxFloor={schedule?.MaxFloor} weather={schedule?.Weather} "
			+ $"time={schedule?.StartTime}~{schedule?.EndTime}) "
			+ $"cur={rsp.CurFloor}/{rsp.MaxFloor} maxScore={rsp.MaxScore} "
			+ $"sweep={rsp.MaxCanSweepFloor} avatars={rsp.AvatarInfoLists.Count}");

		session.Send(Packet.FromProto(rsp, CmdId.GetScDLCTowerRsp));
	}
}

/// <summary>6334 / 6335 —— 开始一层挑战，回要加载的层号（已通过层数 + 1）。</summary>
[PacketCmdId(CmdId.ScDLCTowerStageBeginReq)]
internal class ScDLCTowerStageBeginReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		uint floor = ScDlcTowerService.Begin(uid);

		session.Send(Packet.FromProto(new ScDLCTowerStageBeginRsp
		{
			retcode = ScDLCTowerStageBeginRsp.Retcode.Succ,
			CurFloor = floor
		}, CmdId.ScDLCTowerStageBeginRsp));
	}
}

/// <summary>6336 / 6337 —— 一层结束，推进进度并记最高分。</summary>
[PacketCmdId(CmdId.ScDLCTowerStageEndReq)]
internal class ScDLCTowerStageEndReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		ScDLCTowerStageEndReq req = packet.GetDecodedBody<ScDLCTowerStageEndReq>();

		ScDlcTowerService.End(uid, req, out uint floor, out uint settleFloor, out uint score);

		session.Send(Packet.FromProto(new ScDLCTowerStageEndRsp
		{
			retcode = ScDLCTowerStageEndRsp.Retcode.Succ,
			Floor = floor,
			SettleFloor = settleFloor,
			Score = score,
			RewardData = new Common.Resources.Proto.RewardData()
		}, CmdId.ScDLCTowerStageEndRsp));
	}
}

/// <summary>6338 / 6339 —— 塔排行榜。服务端没有别的玩家数据，回 Succ + 空名次。</summary>
[PacketCmdId(CmdId.GetScDLCTowerRankReq)]
internal class GetScDLCTowerRankReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		Packet.c.Log($"[ScDlcTower] GetScDLCTowerRankReq uid={uid} -> Succ (空榜)");
		session.Send(Packet.FromProto(new GetScDLCTowerRankRsp
		{
			retcode = GetScDLCTowerRankRsp.Retcode.Succ,
			Rank = 0u,
			RankPercent = 0u
		}, CmdId.GetScDLCTowerRankRsp));
	}
}

/// <summary>6347 / 6348 —— 扫荡已通过的层数。不真的发奖励，只回 Succ 避免卡界面。</summary>
[PacketCmdId(CmdId.ScDLCSweepTowerReq)]
internal class ScDLCSweepTowerReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		ScDlcTowerScheme state = ScDlcTowerService.State(uid, out _);
		Packet.c.Log($"[ScDlcTower] SweepTower uid={uid} -> Succ (sweep={state.MaxCanSweepFloor})");
		session.Send(Packet.FromProto(new ScDLCSweepTowerRsp
		{
			retcode = ScDLCSweepTowerRsp.Retcode.Succ
		}, CmdId.ScDLCSweepTowerRsp));
	}
}
