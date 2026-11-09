using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 给别人的宿舍点赞（LikeDormReq 639）：
/// 每天最多点 10 个（SEND_LIKE_LIMIT）、最多收 10 个（RECEIVE_LIKE_LIMIT），同一个人一天只能点一次（HAS_LIKED）。
/// </summary>
[PacketCmdId(CmdId.LikeDormReq)]
internal class LikeDormReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		LikeDormReq req = packet.GetDecodedBody<LikeDormReq>();
		DormsScheme own = session.Player.Dorms;
		DormService.Prepare(own);

		LikeDormRsp rsp = new LikeDormRsp
		{
			TargetUid = req.TargetUid
		};

		DormsScheme target = req.TargetUid == 0u || req.TargetUid == session.Player.User.Uid
			? null
			: Dorms.TryFromUid(req.TargetUid);
		if (target == null)
		{
			rsp.retcode = LikeDormRsp.Retcode.Fail;
		}
		else if (DormService.HasLiked(own, req.TargetUid))
		{
			rsp.retcode = LikeDormRsp.Retcode.HasLiked;
		}
		else if (DormService.IsSendLikeLimit(own))
		{
			rsp.retcode = LikeDormRsp.Retcode.SendLikeLimit;
		}
		else
		{
			DormService.Prepare(target);
			if (DormService.IsReceiveLikeLimit(target))
			{
				rsp.retcode = LikeDormRsp.Retcode.ReceiveLikeLimit;
			}
			else
			{
				DormService.AddLike(session.Player, target, req.TargetUid);
				rsp.retcode = LikeDormRsp.Retcode.Succ;
				rsp.FriendsPoint = DormService.LikeFriendsPointValue;
			}
		}

		session.Send(Packet.FromProto(rsp, CmdId.LikeDormRsp));
	}
}
