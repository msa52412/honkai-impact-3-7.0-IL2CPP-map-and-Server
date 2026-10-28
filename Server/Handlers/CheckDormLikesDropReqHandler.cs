using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.CheckDormLikesDropReq)]
internal class CheckDormLikesDropReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		CheckDormLikesDropReq decodedBody = packet.GetDecodedBody<CheckDormLikesDropReq>();
		CheckDormLikesDropRsp checkDormLikesDropRsp = new CheckDormLikesDropRsp
		{
			retcode = CheckDormLikesDropRsp.Retcode.Succ
		};
		DormsScheme.CheckDormLikesDrop checkDormLikes = session.Player.Dorms.CheckDormLikes;
		checkDormLikesDropRsp.HasLikesDrop = checkDormLikes.HasLikesDrop;
		checkDormLikesDropRsp.YesterdayBeLikedNum = checkDormLikes.YesterdayBeLikedNum;
		session.Send(Packet.FromProto(checkDormLikesDropRsp, CmdId.CheckDormLikesDropRsp));
	}
}
