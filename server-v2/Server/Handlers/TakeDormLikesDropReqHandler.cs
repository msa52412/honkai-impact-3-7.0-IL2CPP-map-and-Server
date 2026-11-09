using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
using ProtoRewardData = Common.Resources.Proto.RewardData;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 领取昨日获赞掉落（TakeDormLikesDropReq 1008）：按 3 / 12 / 30 / 50 赞四档发奖，
/// 每天只能领一次（HAVE_TAKEN）；不到第一档时没有掉落（NO_DROP）。
/// </summary>
[PacketCmdId(CmdId.TakeDormLikesDropReq)]
internal class TakeDormLikesDropReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		TakeDormLikesDropRsp rsp = new TakeDormLikesDropRsp
		{
			retcode = TakeDormLikesDropRsp.Retcode.Succ
		};

		DormsScheme.CheckDormLikesDrop drop = dorms.CheckDormLikes;
		rsp.YesterdayBeLikedNum = drop.YesterdayBeLikedNum;

		if (drop.YesterdayBeLikedNum == 0u)
		{
			rsp.retcode = TakeDormLikesDropRsp.Retcode.NoDrop;
		}
		else if (!drop.HasLikesDrop)
		{
			rsp.retcode = TakeDormLikesDropRsp.Retcode.HaveTaken;
		}
		else
		{
			drop.HasLikesDrop = false;
			dorms.Save();

			int rewardId = DormService.LikesDropRewardId(drop.YesterdayBeLikedNum);
			if (rewardId <= 0)
			{
				rsp.retcode = TakeDormLikesDropRsp.Retcode.NoDrop;
			}
			else
			{
				ProtoRewardData reward = DormService.GrantReward(session.Player, rewardId);
				if (reward == null)
				{
					rsp.retcode = TakeDormLikesDropRsp.Retcode.Fail;
				}
				else
				{
					DormService.AppendDropItems(rsp.DropItemLists, reward);
				}
			}
		}

		session.Send(Packet.FromProto(rsp, CmdId.TakeDormLikesDropRsp));
	}
}
