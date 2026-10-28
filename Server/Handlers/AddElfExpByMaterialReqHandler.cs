using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.AddElfExpByMaterialReq)]
internal class AddElfExpByMaterialReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
	}
}
