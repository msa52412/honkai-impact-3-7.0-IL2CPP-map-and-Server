using System.Collections.Generic;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetAvatarRollDataReq)]
internal class GetAvatarRollDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetAvatarRollDataReq decodedBody = packet.GetDecodedBody<GetAvatarRollDataReq>();
		GetAvatarRollDataRsp getAvatarRollDataRsp = new GetAvatarRollDataRsp
		{
			retcode = GetAvatarRollDataRsp.Retcode.Succ
		};
		uint[] unlockAvatarLists = session.Player.Dorms.UnlockAvatarLists;
		List<AvatarRoll> list = new List<AvatarRoll>();
		for (int i = 0; i < unlockAvatarLists.Length; i++)
		{
			AvatarRoll item = new AvatarRoll
			{
				AvatarId = unlockAvatarLists[i],
				Progress = 1u,
				IsUnlock = true
			};
			list.Add(item);
		}
		getAvatarRollDataRsp.RollLists.AddRange(list);
		getAvatarRollDataRsp.IsAll = true;
		session.Send(Packet.FromProto(getAvatarRollDataRsp, CmdId.GetAvatarRollDataRsp));
	}
}
