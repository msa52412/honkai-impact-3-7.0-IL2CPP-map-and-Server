using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.AddGoodfeelReq)]
internal class AddGoodfeelReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		AddGoodfeelReq Data = packet.GetDecodedBody<AddGoodfeelReq>();
		AvatarScheme avatarScheme = session.Player.AvatarList.Where((AvatarScheme avatar) => avatar.AvatarId == Data.AvatarId).FirstOrDefault();
		if (avatarScheme != null)
		{
			avatarScheme.TodayHasAddGoodfeel += (uint)Data.AddGoodfeel;
			avatarScheme.TouchGoodfeel += (uint)Data.AddGoodfeel;
			session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq
			{
				AvatarIdLists = new uint[1] { avatarScheme.AvatarId }
			}, CmdId.GetAvatarDataReq));
		}
		session.Send(Packet.FromProto(new AddGoodfeelRsp
		{
			retcode = AddGoodfeelRsp.Retcode.Succ
		}, CmdId.AddGoodfeelRsp));
	}
}
