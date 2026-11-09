using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.ElfTakeCompensationReq)]
internal class ElfTakeCompensationReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
	}
}
