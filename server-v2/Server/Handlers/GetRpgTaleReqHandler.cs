using System;
using System.Collections.Generic;
using Common;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 2300 / 2301 —— 「RPG 故事」主数据。
///
/// 「角色挑战」那套页面（`GeneralAvatarActivityMainPageContext` /
/// `MonoGeneralAvatarActLevelPage`）走的是 RpgTale：章节 = tale，章节里的卡片 = site。
///
/// 原来这里只回 `retcode + tale_id`，`rpg_tale_list` 是**空的**，
/// 客户端拿不到任何 site，卡片能画出来（靠配置表）但**点了没反应**。
/// 日志里 `GetRpgTaleReq` 每 6 分钟轮询一次，就是因为一直拿不到数据。
///
/// 这里按 `RpgTale.AreaList` → `RpgArea.SiteList` 把站点全量下发，
/// 站点状态统一给「可进入」（cur_progress 全 0 = 未开始，pass_times 0）。
/// </summary>
[PacketCmdId(CmdId.GetRpgTaleReq)]
internal class GetRpgTaleReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetRpgTaleReq req = packet.GetDecodedBody<GetRpgTaleReq>();

		GetRpgTaleRsp rsp = new GetRpgTaleRsp
		{
			retcode = GetRpgTaleRsp.Retcode.Succ,
			IsAll = req.IsAll,
			TaleId = req.TaleId
		};

		RpgTaleCfg taleReader = RpgTaleCfg.GetInstance();
		RpgAreaCfg areaReader = RpgAreaCfg.GetInstance();

		int siteCount = 0;
		int taleCount = 0;

		if (req.TaleId != 0u)
		{
			RpgTaleCfgExcel cfg = taleReader.FromTaleId(req.TaleId);
			if (cfg != null)
			{
				rsp.RpgTaleLists.Add(BuildTale(cfg, areaReader, out int sites));
				siteCount += sites;
				taleCount++;
			}
		}
		else if (req.IsAll && taleReader.All != null)
		{
			foreach (RpgTaleCfgExcel cfg in taleReader.All)
			{
				rsp.RpgTaleLists.Add(BuildTale(cfg, areaReader, out int sites));
				siteCount += sites;
				taleCount++;
			}
		}

		Packet.c.Log($"[RpgTale] GetRpgTale tale={req.TaleId} isAll={req.IsAll} "
			+ $"-> Succ tales={taleCount} sites={siteCount}");
		session.Send(Packet.FromProto(rsp, CmdId.GetRpgTaleRsp));
	}

	private static RpgTaleData BuildTale(RpgTaleCfgExcel cfg, RpgAreaCfg areaReader, out int siteCount)
	{
		RpgTaleData data = new RpgTaleData
		{
			TaleId = cfg.TaleId,
			CurSiteId = 0u,
			EndTime = 0u,
			ScheduleId = 0u,
			SiteRefreshNum = 0u,
			FinishedEventIdLists = Array.Empty<uint>(),
			RewardTakenCollectionCountLists = Array.Empty<uint>()
		};

		siteCount = 0;
		if (cfg.AreaList == null)
		{
			return data;
		}

		foreach (int areaId in cfg.AreaList)
		{
			data.AreaLists.Add(new RpgTaleArea { AreaId = (uint)areaId });

			RpgAreaCfgExcel area = areaReader.FromAreaId((uint)areaId);
			if (area?.SiteList == null)
			{
				continue;
			}

			foreach (int siteId in area.SiteList)
			{
				// cur_progress.id 必须是 RpgSite.SiteProgressConfig 里的 id，
				// 填 0 客户端会当成无效状态，卡片就点不动。
				uint progressId = 0u;
				RpgSiteCfgExcel siteCfg = RpgSiteCfg.GetInstance().FromSiteId((uint)siteId);
				if (siteCfg?.SiteProgressConfig != null && siteCfg.SiteProgressConfig.Length > 0)
				{
					progressId = (uint)siteCfg.SiteProgressConfig[0];
				}

				data.SiteLists.Add(new RpgTaleSite
				{
					SiteId = (uint)siteId,
					AreaId = (uint)areaId,
					CurProgress = new RpgTaleProgress
					{
						Id = progressId,
						Progress = 0u
					},
					PassTimes = 0u,
					StatusEndTime = 0u,
					CloseTime = 0u,
					EventType = 0u,
					EventParam = 0u,
					FinishedMatrixEventIdLists = Array.Empty<uint>()
				});
				siteCount++;
				if (data.CurSiteId == 0u)
				{
					data.CurSiteId = (uint)siteId;
				}
			}
		}

		return data;
	}
}
