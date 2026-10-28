using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GrandKeyActivateSkillReq)]
internal class GrandKeyActivateSkillReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
	}
}
