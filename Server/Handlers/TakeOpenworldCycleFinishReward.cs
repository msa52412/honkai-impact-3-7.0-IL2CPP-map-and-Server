using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

[PacketCmdId(CmdId.TakeOpenworldCycleFinishRewardReq)]
internal class TakeOpenworldCycleFinishRewardReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		TakeOpenworldCycleFinishRewardReq decodedBody = packet.GetDecodedBody<TakeOpenworldCycleFinishRewardReq>();
		session.Send(Packet.FromProto(new TakeOpenworldCycleFinishRewardRsp
		{
			retcode = TakeOpenworldCycleFinishRewardRsp.Retcode.Succ,
			MapId = decodedBody.MapId,
			Cycle = decodedBody.Cycle
		}, CmdId.TakeOpenworldCycleFinishRewardRsp));
	}
}
