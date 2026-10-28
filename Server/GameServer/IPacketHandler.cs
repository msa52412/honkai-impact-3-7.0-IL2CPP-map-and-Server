namespace PemukulPaku.GameServer;

public interface IPacketHandler
{
	void Handle(Session session, Packet packet);
}
