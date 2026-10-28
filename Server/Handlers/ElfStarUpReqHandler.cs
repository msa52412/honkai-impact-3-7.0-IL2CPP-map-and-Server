using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.ElfStarUpReq)]
internal class ElfStarUpReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		ElfStarUpReq decodedBody = packet.GetDecodedBody<ElfStarUpReq>();
		ElfStarUpRsp elfStarUpRsp = new ElfStarUpRsp
		{
			retcode = ElfStarUpRsp.Retcode.Succ
		};
		elfStarUpRsp.ElfId = decodedBody.ElfId;
		elfStarUpRsp.IsUnlock = true;
		session.Send(Packet.FromProto(elfStarUpRsp, CmdId.ElfStarUpRsp));
	}
}
