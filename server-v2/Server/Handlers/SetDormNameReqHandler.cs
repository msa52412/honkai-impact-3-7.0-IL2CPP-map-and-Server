using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 宿舍 / 房间改名（SetDormNameReq 615）：room_id=0 改宿舍名，否则改房间名。
/// </summary>
[PacketCmdId(CmdId.SetDormNameReq)]
internal class SetDormNameReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetDormNameReq req = packet.GetDecodedBody<SetDormNameReq>();
		string name = req.Name ?? "";
		SetDormNameRsp rsp = new SetDormNameRsp
		{
			retcode = SetDormNameRsp.Retcode.Succ,
			CdEndTime = 0u
		};

		if (string.IsNullOrWhiteSpace(name) || name.Length > 16)
		{
			rsp.retcode = SetDormNameRsp.Retcode.FormatError;
		}
		else
		{
			switch (session.Player.Dorms.SetDormName(req.HouseId, req.RoomId, name))
			{
			case 0u:
				rsp.retcode = SetDormNameRsp.Retcode.HouseNotExist;
				break;
			case 1u:
				rsp.retcode = SetDormNameRsp.Retcode.RoomNotExist;
				break;
			default:
				session.Send(Packet.FromProto(rsp, CmdId.SetDormNameRsp));
				session.ProcessPacket(Packet.FromProto(new GetDormHouseReq
				{
					HouseIdLists = new uint[1] { req.HouseId }
				}, CmdId.GetDormHouseReq));
				return;
			}
		}

		session.Send(Packet.FromProto(rsp, CmdId.SetDormNameRsp));
	}
}
