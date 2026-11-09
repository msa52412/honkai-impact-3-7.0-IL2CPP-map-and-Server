using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.ChapterBwWorldGetDataReq)]
internal class ChapterBwWorldGetDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		ChapterBwWorldGetDataReq decodedBody = packet.GetDecodedBody<ChapterBwWorldGetDataReq>();
		session.Send(Packet.FromProto(new ChapterBwWorldGetDataRsp
		{
			retcode = ChapterBwWorldGetDataRsp.Retcode.Succ,
			ChapterBwWorld = new ChapterBwWorld
			{
				ChapterId = decodedBody.ChapterId,
				EquipRuneUniqueIdLists = new uint[2]
			}
		}, CmdId.ChapterBwWorldGetDataRsp));
	}
}
