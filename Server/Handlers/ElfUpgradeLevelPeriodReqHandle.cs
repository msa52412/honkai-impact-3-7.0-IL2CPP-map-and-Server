using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.ElfUpgradeLevelPeriodReq)]
internal class ElfUpgradeLevelPeriodReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
	}
}
