using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GrandKeyLevelUpReq)]
internal class GrandKeyLevelUpReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
	}
}
