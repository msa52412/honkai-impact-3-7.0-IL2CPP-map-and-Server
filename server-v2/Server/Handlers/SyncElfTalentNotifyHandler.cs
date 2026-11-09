using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SyncElfTalentNotify)]
internal class SyncElfTalentNotifyHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
	}
}
