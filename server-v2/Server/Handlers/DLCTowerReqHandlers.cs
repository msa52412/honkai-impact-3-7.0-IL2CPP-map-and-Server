using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers.Dlc;

/// <summary>
/// 3574 / 3575 —— 后崩坏书爬塔（无限塔）主数据。
///
/// 服务端原来没有 handler（`GetDLCTowerReq not handled!`），客户端拿不到 MyTowerData，
/// 界面显示 0 层、没有赛季。这里把当前赛季、进度、历史最高、每层最高分、队伍血量全给上。
///
/// 关键点：schedule_id 必须是 DLCTowerSchedule.json 里真实存在的 id，
/// 否则客户端 scheduleMetaData = null，MaxFloor 退化成 1，界面变成「已通关 1 层」。
/// </summary>
[PacketCmdId(CmdId.GetDLCTowerReq)]
internal class GetDLCTowerReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;

		GetDLCTowerRsp rsp = DlcTowerService.BuildRsp(uid, out DlcTowerScheme state, out DLCTowerScheduleDataExcel schedule);

		Packet.c.Log($"[DLCTower] GetDLCTowerReq uid={uid} -> Succ schedule={rsp.ScheduleId} "
			+ $"(config={schedule?.ConfigType} maxFloor={schedule?.MaxFloor} level={schedule?.LevelID} "
			+ $"time={schedule?.StartTime}~{schedule?.EndTime}) "
			+ $"progress={rsp.Progress} max={rsp.MaxProgress} cost={rsp.TowerCostTime} revive={rsp.AvatarReviveTimes} "
			+ $"scores={rsp.FloorMaxScoreLists.Count} avatars={rsp.AvatarInfoLists.Count}");

		session.Send(Packet.FromProto(rsp, CmdId.GetDLCTowerRsp));
	}
}

/// <summary>
/// 3563 / 3564 —— 开始一层挑战。回当前已通过层数，客户端用它加载 progress+1 层。
/// </summary>
[PacketCmdId(CmdId.DLCTowerStageBeginReq)]
internal class DLCTowerStageBeginReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		DLCTowerStageBeginReq req = packet.GetDecodedBody<DLCTowerStageBeginReq>();

		uint progress = DlcTowerService.Begin(uid);

		Packet.c.Log($"[DLCTower] StageBegin uid={uid} avatars=[{string.Join(",", req.AvatarIdLists ?? System.Array.Empty<uint>())}] -> progress={progress}");
		session.Send(Packet.FromProto(new DLCTowerStageBeginRsp
		{
			retcode = DLCTowerStageBeginRsp.Retcode.Succ,
			Progress = progress
		}, CmdId.DLCTowerStageBeginRsp));
	}
}

/// <summary>
/// 3565 / 3566 —— 一层挑战结束。StageWin 才推进进度，其余状态原样返回，避免「失败也算过」。
/// 回包里 end_progress = 结算后的已通过层数，score 用于界面上的本层得分。
/// </summary>
[PacketCmdId(CmdId.DLCTowerStageEndReq)]
internal class DLCTowerStageEndReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		DLCTowerStageEndReq req = packet.GetDecodedBody<DLCTowerStageEndReq>();

		DlcTowerService.End(uid, req, out uint progress, out uint score, out bool isNewRecord);

		DLCTowerStageEndRsp rsp = new DLCTowerStageEndRsp
		{
			retcode = DLCTowerStageEndRsp.Retcode.Succ,
			Progress = progress,
			EndProgress = progress,
			Score = score,
			IsNewRecord = isNewRecord
		};

		session.Send(Packet.FromProto(rsp, CmdId.DLCTowerStageEndRsp));
	}
}

/// <summary>
/// 3576 / 3577 —— 重置爬塔进度（界面上的「重置」按钮）。
/// </summary>
[PacketCmdId(CmdId.ResetDLCTowerProgressReq)]
internal class ResetDLCTowerProgressReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		DlcTowerService.Reset(uid);
		session.Send(Packet.FromProto(new ResetDLCTowerProgressRsp
		{
			retcode = ResetDLCTowerProgressRsp.Retcode.Succ
		}, CmdId.ResetDLCTowerProgressRsp));
	}
}

/// <summary>
/// 3571 / 3572 —— 爬塔排行榜。服务端没有别的玩家数据，回 Succ + 空 rank_data，
/// 至少不会让排行榜页一直转圈。
/// </summary>
[PacketCmdId(CmdId.GetDLCTowerRankReq)]
internal class GetDLCTowerRankReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		Packet.c.Log($"[DLCTower] GetDLCTowerRankReq uid={uid} -> Succ (空榜)");
		session.Send(Packet.FromProto(new GetDLCTowerRankRsp
		{
			retcode = GetDLCTowerRankRsp.Retcode.Succ
		}, CmdId.GetDLCTowerRankRsp));
	}
}
