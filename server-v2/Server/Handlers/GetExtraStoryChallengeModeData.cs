using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Two;

[PacketCmdId(CmdId.GetExtraStoryChallengeModeDataReq)]
internal class GetExtraStoryChallengeModeDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetExtraStoryChallengeModeDataReq decodedBody = packet.GetDecodedBody<GetExtraStoryChallengeModeDataReq>();
		session.Send(Packet.FromProto(new GetExtraStoryChallengeModeDataRsp
		{
			retcode = GetExtraStoryChallengeModeDataRsp.Retcode.Succ,
			ChooseDifficulty = 0u,
			IsCanReset = true,
			ChapterId = decodedBody.ChapterId
		}, CmdId.GetExtraStoryChallengeModeDataRsp));
	}
}
