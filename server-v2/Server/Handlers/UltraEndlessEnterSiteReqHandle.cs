using Common.Resources.Proto;
using System.Linq;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.UltraEndlessEnterSiteReq)]
internal class UltraEndlessEnterSiteReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
        session.Send(Packet.FromProto(Build(session.Player,packet.GetDecodedBody<UltraEndlessEnterSiteReq>()),CmdId.UltraEndlessEnterSiteRsp));
    }
    internal static UltraEndlessEnterSiteRsp Build(Player player,UltraEndlessEnterSiteReq decodedBody)
    {
		Packet.c.Log($"[Ultra] 5204 EnterSite: site={decodedBody.SiteId}");
		var state = UltraEndlessCycle.Enabled ? UltraEndlessState.Get(player.User.Uid) : null;
		return new UltraEndlessEnterSiteRsp
		{
			retcode = (UltraEndlessCycle.Enabled && !UltraEndlessCycle.Current.Open) || (state != null ? !state.PeriodSites.Contains((int)decodedBody.SiteId) : !UltraEndlessValidation.ContainsSite(decodedBody.SiteId))
				? UltraEndlessEnterSiteRsp.Retcode.NotInSchedule : UltraEndlessEnterSiteRsp.Retcode.Succ,
			SiteId = decodedBody.SiteId
		};
	}
}
