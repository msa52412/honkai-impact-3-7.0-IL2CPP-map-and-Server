using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.FinishPlotReq)]
internal class FinishPlotReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		FinishPlotReq req = packet.GetDecodedBody<FinishPlotReq>();
		FinishPlotRsp rsp = new FinishPlotRsp
		{
			retcode = FinishPlotRsp.Retcode.Succ
		};
		if (req.ShouldSerializePlotType()) rsp.PlotType = req.PlotType;
		if (req.ShouldSerializePlotId()) rsp.PlotId = req.PlotId;
		if (req.ShouldSerializeDialogId()) rsp.DialogId = req.DialogId;
		// Final dialogue of the completed 170199 intro. Save before creating the
		// lobby, so a UI exception cannot make the player repeat the battle.
		if (req.PlotType == 0 && req.PlotId == 54581 && req.DialogId == 545810056 && session.Player != null)
			GodWarCompatibility.CompleteIntro(session.Player.User.Uid);
		Packet.c.Log($"[Plot] Finish type={req.PlotType} plot={req.PlotId} dialog={req.DialogId}");
		session.Send(Packet.FromProto(rsp, CmdId.FinishPlotRsp));
	}
}
