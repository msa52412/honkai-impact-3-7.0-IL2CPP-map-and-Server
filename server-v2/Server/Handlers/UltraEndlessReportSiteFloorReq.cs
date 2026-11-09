using Common.Resources.Proto;
using System.Linq;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.UltraEndlessReportSiteFloorReq)]
internal class UltraEndlessReportSiteFloorReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
        session.Send(Packet.FromProto(Apply(session.Player,packet.GetDecodedBody<UltraEndlessReportSiteFloorReq>()),CmdId.UltraEndlessReportSiteFloorRsp));
    }
    internal static UltraEndlessReportSiteFloorRsp Apply(Player player,UltraEndlessReportSiteFloorReq decodedBody)
    {

		// 记分：每层只保留历史最高分。
		// 客户端 GetSiteScore = Σ floor_list[].max_score，所以 5203 必须把这些值发回去，
		// 否则地图上的进度条永远是 0，排名里自己也没有真实分数。
		UltraEndlessState state = UltraEndlessState.Get(player.User.Uid);
		bool isNewBest;
		uint siteTotal;
		uint maxStage;
		bool participationChanged = false;
		bool lineupChanged;
		var lineup = UltraEndlessLineup.Capture(player, decodedBody.AvatarIdLists, decodedBody.ElfIdLists);
		lock (state.Gate)
		{
			{
				var period = UltraEndlessCycle.Enabled ? UltraEndlessCycle.Current : null;
				var site = BaseExcelReader<Common.Utils.ExcelReader.UltraEndlessSite, UltraEndlessSiteExcel>
					.GetInstance().FromId((int)decodedBody.SiteId);
				var floor = site == null ? null : BaseExcelReader<Common.Utils.ExcelReader.UltraEndlessFloor, UltraEndlessFloorExcel>
					.GetInstance().GetFloorDatasFromStageId(site.StageId).FirstOrDefault(f => f.FloorId == decodedBody.Floor);
				if ((period != null ? !period.Open || state.PeriodKey != period.Key || !state.PeriodSites.Contains((int)decodedBody.SiteId) : !UltraEndlessValidation.ContainsSite(decodedBody.SiteId)) || floor == null)
				{
					return new UltraEndlessReportSiteFloorRsp
					{
						retcode = UltraEndlessReportSiteFloorRsp.Retcode.NotInSchedule,
						SiteId = decodedBody.SiteId, Floor = decodedBody.Floor, IsUpFloor = false
					};
				}
				decodedBody.Score = System.Math.Min(decodedBody.Score, (uint)floor.MaxScore);
				participationChanged = UltraEndlessCycle.Enabled && !state.Participated;
				if(UltraEndlessCycle.Enabled)state.Participated = true;
			}
			isNewBest = state.Report(decodedBody.SiteId, decodedBody.Floor, decodedBody.Score);
			lineupChanged = state.RecordLineup(decodedBody.SiteId, decodedBody.Floor, decodedBody.Score, lineup, isNewBest);
			if (UltraEndlessCycle.Enabled && (isNewBest || participationChanged))
				state.ScoreUpdatedAt = (uint)UltraEndlessCycle.Clock().ToUnixTimeSeconds();
			siteTotal = state.SiteScore(decodedBody.SiteId);
			maxStage = state.MaxStageScore;
		}
		// Persist 自己拿 FileGate，必须放在 lock(state.Gate) 外面。
		if (isNewBest || participationChanged || lineupChanged)
		{
			UltraEndlessState.Persist();
		}

		Packet.c.Log($"[Ultra] 5206 ReportSiteFloor: site={decodedBody.SiteId} floor={decodedBody.Floor} "
			+ $"score={decodedBody.Score} cost={decodedBody.CostTime}/{decodedBody.TotalCostTime} "
			+ $"up={decodedBody.IsUpFloor} newBest={isNewBest} "
			+ $"siteTotal={siteTotal} maxStage={maxStage}");

		return new UltraEndlessReportSiteFloorRsp
		{
			retcode = UltraEndlessReportSiteFloorRsp.Retcode.Succ,
			Floor = decodedBody.Floor,
			SiteId = decodedBody.SiteId,
			// ★ is_up_floor 必须原样回客户端自己报的值，不能拿「是否刷新最高分」顶替。
			// 客户端收包处理 CNGIBFBCMLL @ 0x181F9B530：
			//   if (rsp.retcode != 0) return;
			//   if (rsp.is_up_floor == 0) return;      // false 直接返回，什么都不做
			//   ... call 0x181F9BEC0                   // true 才走升层流程（开面板/加载下一层）
			// 原来这里回 isNewBest —— 只要分数破纪录就是 true，
			// 于是「打完第 1 波、根本没升层」也被客户端当成升层，卡在等待里转圈
			// （战斗场景是本地跑的，所以画面还能动、还能继续打怪）。
			IsUpFloor = decodedBody.IsUpFloor
		};
	}
}
