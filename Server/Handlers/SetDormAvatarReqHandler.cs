using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SetDormAvatarReq)]
internal class SetDormAvatarReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetDormAvatarReq decodedBody = packet.GetDecodedBody<SetDormAvatarReq>();
		SetDormAvatarRsp setDormAvatarRsp = new SetDormAvatarRsp
		{
			retcode = SetDormAvatarRsp.Retcode.Succ
		};
		switch (session.Player.Dorms.SetDormAvatar(decodedBody.HouseId, decodedBody.RoomAvatarLists))
		{
		case 1u:
			setDormAvatarRsp.retcode = SetDormAvatarRsp.Retcode.RoomNotExist;
			break;
		default:
			session.ProcessPacket(Packet.FromProto(new GetDormDataReq
			{
				DataType = DormDataType.DormDataAll
			}, CmdId.GetDormDataReq));
			break;
		case 0u:
			setDormAvatarRsp.retcode = SetDormAvatarRsp.Retcode.HouseNotExist;
			break;
		}
		session.Send(Packet.FromProto(setDormAvatarRsp, CmdId.SetDormAvatarRsp));
	}
}
