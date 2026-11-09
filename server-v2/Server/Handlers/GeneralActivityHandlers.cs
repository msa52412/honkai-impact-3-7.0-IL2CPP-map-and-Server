using System;
using System.Collections.Generic;
using Common;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers.Activity;

/// <summary>
/// 4003 / 4004 —— 通用活动排期。
///
/// 客户端 `GeneralActivityModule` 用这份排期建 `activityId -> 时间窗口` 的字典，
/// **活动入口开不开、按钮显不显示「未开启」，全看这里有没有该活动的排期**。
/// 原来服务端没这个 handler（日志 `GeneralActivityGetScheduleReq not handled!`），
/// 所以主界面的「小队联机」（MP PVE，本质是一个通用活动）永远是「未开启」。
///
/// 默认把 GeneralActivity.json 里全部活动都下成
/// `now-1 天 ~ now+30 天`，用 run\generalactivity.json 可以收窄。
/// </summary>
[PacketCmdId(CmdId.GeneralActivityGetScheduleReq)]
internal class GeneralActivityGetScheduleReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GeneralActivityGetScheduleRsp rsp = new GeneralActivityGetScheduleRsp
		{
			retcode = GeneralActivityGetScheduleRsp.Retcode.Succ
		};

		// 按玩家等级过滤：满级号不该看到 MaxLevel=55/69/70 的旧活动卡（会显示「未开启」）。
		uint level = GeneralActivityService.LevelOfExp(session.Player.User.Exp);
		List<uint> ids = GeneralActivityService.TargetActivityIds(level);
		uint begin = GeneralActivityTuning.BeginTime;
		uint end = GeneralActivityTuning.EndTime;

		uint scheduleId = 1u;
		foreach (uint id in ids)
		{
			rsp.ScheduleLists.Add(new GeneralActivityScheduleInfo
			{
				ActivityId = id,
				ScheduleId = scheduleId,
				ShowTime = begin,
				BeginTime = begin,
				EndTime = end,
				SettleTime = end,
				BeginDayTime = 0u,
				// 客户端按「周循环」算开放窗口：begin_day_time ~ end_day_time 是周期内的秒偏移。
				// 两个都填 0 等于零长度窗口 → 永远「准备中 / 下周一开启」。
				// 填满一周（604800 秒）才是全周开放。
				EndDayTime = GeneralActivityTuning.DayWindowSeconds,
				LeftTimes = 1u
			});
			scheduleId++;
		}

		Packet.c.Log($"[GeneralActivity] GetSchedule -> Succ {rsp.ScheduleLists.Count} 条 "
			+ $"（{begin} ~ {end}）open_all={GeneralActivityTuning.OpenAll} lv={level}");
		session.Send(Packet.FromProto(rsp, CmdId.GeneralActivityGetScheduleRsp));
	}
}

/// <summary>
/// 4005 / 4006 —— 通用活动主数据。客户端把想查的 activity_id 一起发过来，
/// 这里按请求回，保证 id 一一对应（客户端按 id 查字典）。
/// </summary>
[PacketCmdId(CmdId.GeneralActivityGetMainInfoReq)]
internal class GeneralActivityGetMainInfoReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GeneralActivityGetMainInfoReq req = packet.GetDecodedBody<GeneralActivityGetMainInfoReq>();

		GeneralActivityGetMainInfoRsp rsp = new GeneralActivityGetMainInfoRsp
		{
			retcode = GeneralActivityGetMainInfoRsp.Retcode.Succ
		};

		uint[] want = req.ActivityIdLists ?? Array.Empty<uint>();
		uint begin = GeneralActivityTuning.BeginTime;
		uint end = GeneralActivityTuning.EndTime;
		uint level = GeneralActivityService.LevelOfExp(session.Player.User.Exp);
		GeneralActivityStageGroupData groupData = GeneralActivityStageGroupData.GetInstance();

		foreach (uint id in want)
		{
			Common.Resources.Proto.GeneralActivity activity = new Common.Resources.Proto.GeneralActivity
			{
				GeneralBasicInfo = new GeneralActivityBasicInfo
				{
					ActivityId = id,
					ScheduleId = GeneralActivityService.ScheduleIdOf(id, level),
					CurScore = 0u,
					// 注意：这里必须保持空数组。
					// 试过填「同 Series 的活动 id」想让二级 tab 出来，结果把别的模式搞坏了
					// （2026-10-30 实测，已回退）。
					SeriesActivityIds = Array.Empty<uint>(),
					ScoreRewardTakenLists = Array.Empty<uint>()
				}
			};

			// 关卡组排期：活动页（比如「小队联机」）按它决定「准备中 / 进行中」，
			// 不给的话界面永远显示「准备中 下周一开启」。
			System.Collections.Generic.List<Common.Utils.ExcelReader.GeneralActivityStageGroupDataExcel> groups =
				groupData.OfActivity(id);
			if (groups.Count > 0)
			{
				GeneralActivityStage stage = new GeneralActivityStage();
				foreach (Common.Utils.ExcelReader.GeneralActivityStageGroupDataExcel g in groups)
				{
					stage.StageGroupScheduleLists.Add(new GeneralActivityStageGroupScheduleInfo
					{
						StageGroupId = g.StageGroupId,
						BeginTime = begin,
						EndTime = end
					});
				}
				activity.ActivityStage = stage;

				// 「小队联机」的页面（MpPveDailyModeCmpt）还读 scheduled_map_site 的站点排期
				// 和站点倒计时（_siteCmpt / _siteRemainTimer），一起给上。
				activity.ScheduledMapSite = new ScheduledMapSiteActivity();
				foreach (GeneralActivityStageGroupScheduleInfo info in stage.StageGroupScheduleLists)
				{
					activity.ScheduledMapSite.StageGroupScheduleLists.Add(new GeneralActivityStageGroupScheduleInfo
					{
						StageGroupId = info.StageGroupId,
						BeginTime = info.BeginTime,
						EndTime = info.EndTime
					});
				}
			}

			rsp.ActivityLists.Add(activity);
		}

		Packet.c.Log($"[GeneralActivity] GetMainInfo -> Succ 请求 [{string.Join(",", want)}] 回 {rsp.ActivityLists.Count} 条");
		session.Send(Packet.FromProto(rsp, CmdId.GeneralActivityGetMainInfoRsp));
	}
}
