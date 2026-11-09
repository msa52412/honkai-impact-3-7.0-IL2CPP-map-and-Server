using System.Collections.Generic;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 图鉴/收藏页的评分奖励状态（WikiCollectionRank，收藏分 1 / 1500 / 3500 … 7 档）。
/// has_take_rating_reward_list 回已领过的档位，没领过的客户端画成「可领取」。
///
/// 注意：两个数组必须显式给，不能留 null —— 客户端会直接遍历它们。
/// </summary>
[PacketCmdId(CmdId.GetWikiDataReq)]
internal class GetWikiDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint[] taken = WikiService.TakenRanks(session.Player.User.Uid);

		session.Send(Packet.FromProto(new GetWikiDataRsp
		{
			retcode = GetWikiDataRsp.Retcode.Succ,
			HasTakeRatingRewardLists = taken,
			// 版本活动套装奖励（WikiActivity）7.0 没有生效的活动，固定空。
			HasTakeActivitySuitRewardLists = new uint[0]
		}, CmdId.GetWikiDataRsp));
	}
}
