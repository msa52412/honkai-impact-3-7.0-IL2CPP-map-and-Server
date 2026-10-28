using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetPlayerCardReq)]
internal class GetPlayerCardReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetPlayerCardRsp getPlayerCardRsp = new GetPlayerCardRsp();
		getPlayerCardRsp.retcode = GetPlayerCardRsp.Retcode.Succ;
		getPlayerCardRsp.Type = PlayerCardType.CardAll;
		getPlayerCardRsp.ElfIdLists = new uint[1];
		getPlayerCardRsp.AvatarIdLists = new uint[3];
		GetPlayerCardRsp getPlayerCardRsp2 = getPlayerCardRsp;
		getPlayerCardRsp2.MedalLists.AddRange(new Medal[2]
		{
			new Medal
			{
				Id = 0u,
				EndTime = 0u,
				ExtraParam = 0
			},
			new Medal
			{
				Id = 0u,
				EndTime = 0u,
				ExtraParam = 0
			}
		});
		session.Send(Packet.FromProto(getPlayerCardRsp2, CmdId.GetPlayerCardRsp));
	}
}
