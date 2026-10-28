using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.ElfFragmentTransformReq)]
internal class ElfFragmentTransformReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
	}
}
