using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Activity;

[PacketCmdId(CmdId.ChapterActivityTakeDailyRewardReq)]
internal class ChapterActivityTakeDailyRewardReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		ChapterActivityTakeDailyRewardReq decodedBody = packet.GetDecodedBody<ChapterActivityTakeDailyRewardReq>();
		session.Send(Packet.FromProto(new ChapterActivityTakeDailyRewardRsp
		{
			retcode = ChapterActivityTakeDailyRewardRsp.Retcode.HasTake,
			ChapterId = decodedBody.ChapterId
		}, CmdId.ChapterActivityTakeDailyRewardRsp));
	}
}
