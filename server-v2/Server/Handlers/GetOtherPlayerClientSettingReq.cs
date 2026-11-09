using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Three;

[PacketCmdId(CmdId.GetOtherPlayerClientSettingReq)]
internal class GetOtherPlayerClientSettingReqHadler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetOtherPlayerClientSettingReq decodedBody = packet.GetDecodedBody<GetOtherPlayerClientSettingReq>();
		GetOtherPlayerClientSettingRsp proto = new GetOtherPlayerClientSettingRsp
		{
			retcode = GetOtherPlayerClientSettingRsp.Retcode.Succ,
			ClientSettingType = decodedBody.ClientSettingType,
			TargetUid = decodedBody.TargetUid,
			IsWeeklyGuideSwitchOn = false
		};
		session.Send(Packet.FromProto(proto, CmdId.GetOtherPlayerClientSettingRsp));
	}
}
