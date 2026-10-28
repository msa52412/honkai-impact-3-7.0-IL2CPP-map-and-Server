using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GachaReq)]
internal class GachaReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GachaReq decodedBody = packet.GetDecodedBody<GachaReq>();
		GachaRsp gachaRsp = new GachaRsp
		{
			retcode = GachaRsp.Retcode.Succ,
			GachaRandom = decodedBody.GachaRandom,
			IsUseFreeGacha = decodedBody.IsUseFreeGacha
		};
		gachaRsp.ItemLists.Add(new GachaItem
		{
			ItemId = 10106u,
			Level = 0u,
			Num = 5u,
			GiftItemId = 80026u,
			GiftLevel = 0u,
			GiftNum = 50u
		});
		session.Send(Packet.FromProto(gachaRsp, CmdId.GachaRsp));
	}
}
