using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.One;

[PacketCmdId(CmdId.GetMpDataReq)]
internal class GetMpDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetMpDataRsp proto = new GetMpDataRsp
		{
			retcode = GetMpDataRsp.Retcode.Succ,
			DataType = MpDataType.MpDataAll,
			op_type = GetMpDataRsp.OpType.InitData,
			MpLevel = 0u,
			MpExp = 0u,
			TeamAvatarId = session.Player.GetDetailData().LeaderAvatar.AvatarId,
			PunishEndTime = 0u
		};
		session.Send(Packet.FromProto(proto, CmdId.GetMpDataRsp));
	}
}
