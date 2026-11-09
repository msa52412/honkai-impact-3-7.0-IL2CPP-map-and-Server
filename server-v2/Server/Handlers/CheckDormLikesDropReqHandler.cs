using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 查询昨日获赞掉落状态（CheckDormLikesDropReq 1010）。先做一次跨天结算再看结果。
/// </summary>
[PacketCmdId(CmdId.CheckDormLikesDropReq)]
internal class CheckDormLikesDropReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		CheckDormLikesDropRsp rsp = new CheckDormLikesDropRsp
		{
			retcode = CheckDormLikesDropRsp.Retcode.Succ,
			HasLikesDrop = dorms.CheckDormLikes.HasLikesDrop,
			YesterdayBeLikedNum = dorms.CheckDormLikes.YesterdayBeLikedNum
		};

		session.Send(Packet.FromProto(rsp, CmdId.CheckDormLikesDropRsp));
	}
}
