using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetStageChapterReq)]
internal class GetStageChapterReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetStageChapterRsp getStageChapterRsp = new GetStageChapterRsp
		{
			retcode = GetStageChapterRsp.Retcode.Succ
		};
		for (uint num = 1u; num < 38; num++)
		{
			getStageChapterRsp.ChapterLists.Add(new StageChapterInfo
			{
				ChapterId = num,
				HasTakeChallenge = 0uL
			});
		}
		session.Send(Packet.FromProto(getStageChapterRsp, CmdId.GetStageChapterRsp));
	}
}
