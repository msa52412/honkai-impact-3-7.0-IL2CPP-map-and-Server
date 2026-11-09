using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;
using ProtoRewardData = Common.Resources.Proto.RewardData;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 收藏评分（图鉴收藏页右上角那条进度 / 档位奖励）的服务端实现。
///
/// 协议（SeaRelWin8.4.0.proto）：
///   GetWikiDataReq 1193 / GetWikiDataRsp 1194
///     → has_take_rating_reward_list / has_take_activity_suit_reward_list
///   TakeWikiRatingRewardReq 1195 / TakeWikiRatingRewardRsp 1196
///     → 请求带 rating_id + 客户端自己算的 rating_score，回 reward_list
///
/// 档位表是 WikiCollectionRank.json（1/1500/3500/7000/9500/12000/16000 分，7 档）。
/// </summary>
internal static class WikiService
{
	private static WikiCollectionRank Reader => BaseExcelReader<WikiCollectionRank, WikiCollectionRankExcel>.GetInstance();

	/// <summary>已领取的评分档位（回给客户端的 has_take_rating_reward_list）。</summary>
	internal static uint[] TakenRanks(uint uid)
	{
		return WikiRatingTake.TakenRanks(uid);
	}

	/// <summary>
	/// 领收藏评分奖励。客户端会把当前收藏分一起带上来，服务端按配置再校一遍，
	/// 防止「收藏不满却能领最高档」。
	/// </summary>
	internal static TakeWikiRatingRewardRsp.Retcode Take(Player player, uint ratingId, uint ratingScore, out ProtoRewardData reward)
	{
		reward = null!;

		WikiCollectionRankExcel? cfg = Reader.FromId(ratingId);
		if (cfg == null || cfg.Reward <= 0)
		{
			Packet.c.Warn($"[Wiki] 未知收藏评分档位 rating_id={ratingId}");
			return TakeWikiRatingRewardRsp.Retcode.Fail;
		}

		if (WikiRatingTake.Has(player.User.Uid, ratingId))
		{
			return TakeWikiRatingRewardRsp.Retcode.HasTkae;
		}

		if (ratingScore < (uint)cfg.RankScore)
		{
			Packet.c.Warn($"[Wiki] 收藏分不足：rating_id={ratingId} 需要 {cfg.RankScore}，客户端报 {ratingScore}");
			return TakeWikiRatingRewardRsp.Retcode.ScoreLack;
		}

		RewardDataExcel? rewardCfg = BaseExcelReader<Common.Utils.ExcelReader.RewardData, RewardDataExcel>.GetInstance().FromId(cfg.Reward);
		if (rewardCfg == null)
		{
			Packet.c.Warn($"[Wiki] RewardData 里找不到 rewardId={cfg.Reward}（档位 {ratingId}）");
			return TakeWikiRatingRewardRsp.Retcode.Fail;
		}

		reward = RewardService.Grant(player, rewardCfg);
		WikiRatingTake.Mark(player.User.Uid, ratingId);
		player.User.Save();
		player.Equipment.Save();

		Packet.c.Log($"[Wiki] uid={player.User.Uid} 领收藏评分第 {ratingId} 档（score={ratingScore}, rewardId={cfg.Reward}）");
		return TakeWikiRatingRewardRsp.Retcode.Succ;
	}
}
