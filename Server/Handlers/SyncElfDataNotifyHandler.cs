using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SyncElfDataNotify)]
internal class SyncElfDataNotifyHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
	}
}
