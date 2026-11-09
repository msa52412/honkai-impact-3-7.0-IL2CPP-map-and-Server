using Common;
using Common.Database;
using Common.Utils.ExcelReader;

// 命名空间放在 GameServer 根（和 OwMapTuning 一致），
// 这样 Handlers.Openworld 下的 handler 不用额外 using 就能直接调。
namespace PemukulPaku.GameServer;

/// <summary>
/// 开放世界「周期完成奖励」免弹窗。
///
/// 客户端逻辑（日志实证）
/// ---------------------
/// 打开开放世界会先发 GetNewOpenworldReq，服务端回 GetNewOpenworldRsp，
/// 里面 MapLists[].Cycle / HasTakeFinishRewardCycle 决定要不要弹「奖励领取」。
/// 客户端认为「还有没领的周期」就自动发 TakeOpenworldCycleFinishRewardReq，
/// 而且**每领一次把 has_take 推一档**：
///
///     20:45:04  领取 mapId=1 cycle=3   has_take 2 -> 3
///     20:45:18  领取 mapId=1 cycle=4   has_take 3 -> 4
///     20:45:23  领取 mapId=2 cycle=12  has_take 11 -> 12
///
/// 也就是说客户端会一路领到该地图配置里的最大 cycle 才停。
/// 服务端 Cycle 用的是 OpenWorldCycleData.GetInitCycle（第一档 1/11/…），
/// 而 all_story_done=true 让客户端认为剧情全通、周期已到顶，两边口径不一致
/// → 每次打开都要补领一轮。
///
/// 修法：直接把 Cycle 和 HasTakeFinishRewardCycle 都拉到 GetMaxCycle(mapId)，
/// 两边一致，客户端就认为「已领取完毕」，不再弹窗也不再发领取请求。
/// </summary>
internal static class OwCycleReward
{
	internal static void Apply(OpenWorldScheme scheme)
	{
		if (scheme == null || !OwMapTuning.AutoTakeCycleReward)
		{
			return;
		}

		uint max = BaseExcelReader<OpenWorldCycleData, OpenWorldCycleDataExcel>.GetInstance().GetMaxCycle(scheme.MapId);
		if (max == 0u)
		{
			// map 301 之类在 OpenWorldCycleData 里没有行的地图，跳过
			return;
		}
		if (scheme.Cycle == max && scheme.HasTakeFinishRewardCycle == max)
		{
			return;
		}

		Packet.c.Log($"[OW] mapId={scheme.MapId} 周期 {scheme.Cycle}->{max}，"
			+ $"周期奖励 {scheme.HasTakeFinishRewardCycle}->{max}（视为已领取）");
		scheme.Cycle = max;
		scheme.HasTakeFinishRewardCycle = max;

		// 临时补出来的 scheme（Id<=0）不在库里，Save 会 InsertOne 插一条重复行，跳过。
		if (scheme.Id > 0)
		{
			scheme.Save();
		}
	}
}
