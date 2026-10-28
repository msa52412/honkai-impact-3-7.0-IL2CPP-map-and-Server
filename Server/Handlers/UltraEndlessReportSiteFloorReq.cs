using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.UltraEndlessReportSiteFloorReq)]
internal class UltraEndlessReportSiteFloorReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		UltraEndlessReportSiteFloorReq decodedBody = packet.GetDecodedBody<UltraEndlessReportSiteFloorReq>();

		// 记分：每层只保留历史最高分。
		// 客户端 GetSiteScore = Σ floor_list[].max_score，所以 5203 必须把这些值发回去，
		// 否则地图上的进度条永远是 0，排名里自己也没有真实分数。
		UltraEndlessState state = UltraEndlessState.Get(session.Player.User.Uid);
		bool isNewBest;
		uint siteTotal;
		uint maxStage;
		lock (state.Gate)
		{
			isNewBest = state.Report(decodedBody.SiteId, decodedBody.Floor, decodedBody.Score);
			siteTotal = state.SiteScore(decodedBody.SiteId);
			maxStage = state.MaxStageScore;
		}
		// Persist 自己拿 FileGate，必须放在 lock(state.Gate) 外面。
		if (isNewBest)
		{
			UltraEndlessState.Persist();
		}

		Packet.c.Log($"[Ultra] 5206 ReportSiteFloor: site={decodedBody.SiteId} floor={decodedBody.Floor} "
			+ $"score={decodedBody.Score} cost={decodedBody.CostTime}/{decodedBody.TotalCostTime} "
			+ $"up={decodedBody.IsUpFloor} newBest={isNewBest} "
			+ $"siteTotal={siteTotal} maxStage={maxStage}");

		session.Send(Packet.FromProto(new UltraEndlessReportSiteFloorRsp
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
		}, CmdId.UltraEndlessReportSiteFloorRsp));
	}
}
