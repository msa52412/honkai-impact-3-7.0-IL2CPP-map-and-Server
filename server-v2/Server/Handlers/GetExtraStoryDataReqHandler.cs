using System;
using Common;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using AreaCfg = Common.Utils.ExcelReader.ExtraStoryAreaExcel;
using AreaReader = Common.Utils.ExcelReader.ExtraStoryArea;
using ActCfg = Common.Utils.ExcelReader.ExtraStoryActExcel;
using ActReader = Common.Utils.ExcelReader.ExtraStoryAct;
using ChapterCfg = Common.Utils.ExcelReader.ExtraStoryChapterExcel;
using ChapterReader = Common.Utils.ExcelReader.ExtraStoryChapter;

namespace PemukulPaku.GameServer.Handlers.Three;

/// <summary>
/// 231 / 232 —— 「编年史」（客户端 ExtraStory）主数据。
///
/// 原来服务端没有 handler（日志 `GetExtraStoryDataReq not handled!`），
/// 客户端拿不到 chapter_list，编年史界面整片显示「尚未开启」。
///
/// 每个章节的开启状态只看 `ExtraStoryChapter.begin_time / end_time`，
/// 而 ExtraStoryChapter.json 里没有时间字段，所以统一给 `2020-01-01 ~ 2099-12-31`。
/// 区域从 ExtraStoryArea.json 按 chapterId 挂上去（不带 act_list，客户端自己按表建）。
/// </summary>
[PacketCmdId(CmdId.GetExtraStoryDataReq)]
internal class GetExtraStoryDataReqHandler : IPacketHandler
{
	internal const uint BeginTime = 1577836800u; // 2020-01-01 00:00:00 UTC+8
	internal const uint EndTime = 4102358400u;   // 2099-12-31 00:00:00 UTC+8

	public void Handle(Session session, Packet packet)
	{
		GetExtraStoryDataRsp rsp = new GetExtraStoryDataRsp
		{
			retcode = GetExtraStoryDataRsp.Retcode.Succ,
			IsDynamicHardLv = false,
			IsOpenEnhance = true,
			ChapterKeyLists = Array.Empty<uint>(),
			TakenRewardLineIdLists = Array.Empty<uint>(),
			TakenEnhanceRewardActIdLists = Array.Empty<uint>()
		};

		AreaCfg[] areas = AreaReader.GetInstance().All;
		ActCfg[] actCfgs = ActReader.GetInstance().All;
		int areaCount = 0;
		int actCount = 0;

		foreach (ChapterCfg cfg in ChapterReader.GetInstance().All)
		{
			if (cfg.ChapterId == 0)
			{
				continue;
			}

			Common.Resources.Proto.ExtraStoryChapter chapter = new Common.Resources.Proto.ExtraStoryChapter
			{
				ChapterId = (uint)cfg.ChapterId,
				// 表里 minPlayerLevel 是 20/24 这类值，但客户端仍按它上锁
				// （提示「等级 24 解锁」而玩家已经 88 级），直接给 0 = 无等级门槛。
				MinPlayerLevel = 0u,
				BeginTime = BeginTime,
				EndTime = EndTime,
				IsNeedKey = false
			};

			if (areas != null)
			{
				foreach (AreaCfg area in areas)
				{
					if (area.ChapterId != cfg.ChapterId)
					{
						continue;
					}
					Common.Resources.Proto.ExtraStoryArea protoArea = new Common.Resources.Proto.ExtraStoryArea
					{
						AreaId = (uint)area.AreaId,
						BeginTime = BeginTime,
						EndTime = EndTime
					};

					// act_list 必须填！客户端 `OBDKDAJIHNO.MBLIIEOLACC(难度)`
					// （0x18172AE60）会遍历 zone 的 act 列表，找一条难度匹配的 act；
					// 列表为空 → IsActive() 恒 false → GetLockType() 返回 Time/PreZone
					// → 章节卡片点不进去（0x181D01D70 里 `lockType != 0` 直接走提示分支）。
					if (actCfgs != null)
					{
						foreach (ActCfg act in actCfgs)
						{
							if (act.AreaId != area.AreaId)
							{
								continue;
							}
							protoArea.ActLists.Add(new Common.Resources.Proto.ExtraStoryAct
							{
								ActId = (uint)act.ActId,
								BeginTime = BeginTime,
								EndTime = EndTime
							});
							actCount++;
						}
					}

					chapter.AreaLists.Add(protoArea);
					areaCount++;
				}
			}

			rsp.ChapterLists.Add(chapter);
		}

		Packet.c.Log($"[ExtraStory] GetExtraStoryData -> Succ chapters={rsp.ChapterLists.Count} "
			+ $"areas={areaCount} acts={actCount}");
		session.Send(Packet.FromProto(rsp, CmdId.GetExtraStoryDataRsp));
	}
}
