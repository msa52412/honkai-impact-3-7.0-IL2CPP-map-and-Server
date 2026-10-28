using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.UltraEndlessEnterSiteReq)]
internal class UltraEndlessEnterSiteReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		UltraEndlessEnterSiteReq decodedBody = packet.GetDecodedBody<UltraEndlessEnterSiteReq>();
		Packet.c.Log($"[Ultra] 5204 EnterSite: site={decodedBody.SiteId}");
		session.Send(Packet.FromProto(new UltraEndlessEnterSiteRsp
		{
			retcode = UltraEndlessEnterSiteRsp.Retcode.Succ,
			SiteId = decodedBody.SiteId
		}, CmdId.UltraEndlessEnterSiteRsp));
	}
}
