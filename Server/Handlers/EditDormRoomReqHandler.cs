using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.EditDormRoomReq)]
internal class EditDormRoomReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		EditDormRoomReq decodedBody = packet.GetDecodedBody<EditDormRoomReq>();
		EditDormRoomRsp editDormRoomRsp = new EditDormRoomRsp
		{
			retcode = EditDormRoomRsp.Retcode.Succ
		};
		if (session.Player.Dorms.EditDorm(decodedBody.HouseId, decodedBody.RoomId, decodedBody.FurnitureLists) == 0)
		{
			editDormRoomRsp.retcode = EditDormRoomRsp.Retcode.RoomNotExist;
		}
		session.Send(Packet.FromProto(editDormRoomRsp, CmdId.EditDormRoomRsp));
	}
}
