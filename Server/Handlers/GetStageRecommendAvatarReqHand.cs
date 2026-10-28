using System.Linq;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetStageRecommendAvatarReq)]
internal class GetStageRecommendAvatarReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetStageRecommendAvatarReq decodedBody = packet.GetDecodedBody<GetStageRecommendAvatarReq>();
		GetStageRecommendAvatarRsp getStageRecommendAvatarRsp = new GetStageRecommendAvatarRsp
		{
			retcode = GetStageRecommendAvatarRsp.Retcode.Succ
		};
		if (decodedBody.IdLists != null)
		{
			getStageRecommendAvatarRsp.StageRecommendAvatarLists.AddRange(decodedBody.IdLists.Select((string x) => new StageRecommendAvatar
			{
				Id = x
			}));
		}
		session.Send(Packet.FromProto(getStageRecommendAvatarRsp, CmdId.GetStageRecommendAvatarRsp));
	}
}
