using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GrandKeySetSkillReq)]
internal class GrandKeySetSkillReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
	}
}
