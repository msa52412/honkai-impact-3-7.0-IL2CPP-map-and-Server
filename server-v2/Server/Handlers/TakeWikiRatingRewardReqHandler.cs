using System.Linq;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
using ProtoRewardData = Common.Resources.Proto.RewardData;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 收藏评分档位领奖（TakeWikiRatingRewardReq 1195）。
/// 校验：档位不存在 → FAIL；已领 → HAS_TKAE；收藏分不够 → SCORE_LACK；否则发奖并落库。
/// </summary>
[PacketCmdId(CmdId.TakeWikiRatingRewardReq)]
internal class TakeWikiRatingRewardReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		TakeWikiRatingRewardReq req = packet.GetDecodedBody<TakeWikiRatingRewardReq>();

		// 诊断：客户端会把「它自己认为已解锁的 CG 列表」一起报上来（cg_list），
		// 这是唯一能直接看到客户端 CG 解锁集合的地方，用来定位影像栏为什么是 0%。
		uint[] cgList = req.CgLists ?? new uint[0];
		string cgSample = string.Join(",", cgList.Take(10));
		Packet.c.Log(string.Format("[Collection] uid={0} 领收藏评分 rating={1} score={2} cg_list={3} 样例[{4}]",
			session.Player.User.Uid, req.RatingId, req.RatingScore, cgList.Length, cgSample));

		TakeWikiRatingRewardRsp.Retcode retcode = WikiService.Take(
			session.Player, req.RatingId, req.RatingScore, out ProtoRewardData reward);

		TakeWikiRatingRewardRsp rsp = new TakeWikiRatingRewardRsp
		{
			retcode = retcode,
			RatingId = req.RatingId
		};

		if (retcode == TakeWikiRatingRewardRsp.Retcode.Succ && reward != null)
		{
			rsp.RewardLists.Add(reward);
		}

		session.Send(Packet.FromProto(rsp, CmdId.TakeWikiRatingRewardRsp));
	}
}
