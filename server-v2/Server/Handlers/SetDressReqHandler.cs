using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SetDressReq)]
internal class SetDressReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetDressReq Data = packet.GetDecodedBody<SetDressReq>();
		SetDressRsp setDressRsp = new SetDressRsp
		{
			retcode = SetDressRsp.Retcode.Succ
		};
		AvatarScheme avatarScheme = session.Player.AvatarList.Where((AvatarScheme x) => x.AvatarId == Data.AvatarId).FirstOrDefault();
		if (avatarScheme != null)
		{
			if (avatarScheme.DressLists.Contains(Data.DressId))
			{
				avatarScheme.SetDress(Data.DressId);
				session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq
				{
					AvatarIdLists = new uint[1] { avatarScheme.AvatarId }
				}, CmdId.GetAvatarDataReq));
			}
			else
			{
				setDressRsp.retcode = SetDressRsp.Retcode.DressNotExist;
			}
		}
		else
		{
			setDressRsp.retcode = SetDressRsp.Retcode.AvatarNotExist;
		}
		session.Send(Packet.FromProto(setDressRsp, CmdId.SetDressRsp));
	}
}
