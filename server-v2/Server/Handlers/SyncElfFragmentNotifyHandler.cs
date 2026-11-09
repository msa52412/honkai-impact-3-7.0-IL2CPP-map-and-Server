using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SyncElfFragmentNotify)]
internal class SyncElfFragmentNotifyHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
	}
}
