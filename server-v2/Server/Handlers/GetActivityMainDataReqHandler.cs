using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetActivityMainDataReq)]
internal class GetActivityMainDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet _packet)
	{
		session.Send(Packet.FromProto(new GetActivityMainDataRsp
		{
			retcode = GetActivityMainDataRsp.Retcode.Succ,
			ActivityModuleTypeLists = new uint[70]
			{
				1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u, 9u, 10u,
				11u, 12u, 13u, 14u, 15u, 16u, 17u, 18u, 19u, 20u,
				21u, 22u, 23u, 24u, 25u, 26u, 27u, 28u, 29u, 30u,
				31u, 32u, 33u, 34u, 35u, 36u, 37u, 38u, 39u, 40u,
				41u, 42u, 43u, 44u, 45u, 46u, 47u, 48u, 49u, 50u,
				51u, 52u, 53u, 54u, 55u, 56u, 57u, 58u, 59u, 60u,
				61u, 62u, 63u, 64u, 65u, 66u, 67u, 68u, 70u, 72u
			}
		}, CmdId.GetActivityMainDataRsp));
	}
}
