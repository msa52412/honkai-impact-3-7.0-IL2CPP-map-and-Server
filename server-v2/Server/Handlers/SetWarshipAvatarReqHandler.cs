using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SetWarshipAvatarReq)]
internal class SetWarshipAvatarReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetWarshipAvatarReq decodedBody = packet.GetDecodedBody<SetWarshipAvatarReq>();
		if (decodedBody.FirstAvatarId == 0)
		{
			decodedBody.FirstAvatarId = 101u;
		}
		session.Player.User.WarshipAvatar = new WarshipAvatarData
		{
			WarshipFirstAvatarId = decodedBody.FirstAvatarId,
			WarshipSecondAvatarId = decodedBody.SecondAvatarId
		};
		GetMainDataRsp getMainDataRsp = new GetMainDataRsp();
		getMainDataRsp.retcode = GetMainDataRsp.Retcode.Succ;
		getMainDataRsp.WarshipAvatar = session.Player.User.WarshipAvatar;
		getMainDataRsp.TypeLists = new uint[1] { 35u };
		GetMainDataRsp proto = getMainDataRsp;
		session.Send(Packet.FromProto(proto, CmdId.GetMainDataRsp), Packet.FromProto(new SetWarshipAvatarRsp
		{
			retcode = SetWarshipAvatarRsp.Retcode.Succ
		}, CmdId.SetWarshipAvatarRsp));
	}
}
