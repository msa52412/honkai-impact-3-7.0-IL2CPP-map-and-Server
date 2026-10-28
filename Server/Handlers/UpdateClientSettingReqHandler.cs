using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.UpdateClientSettingReq)]
internal class UpdateClientSettingReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		UpdateClientSettingReq Data = packet.GetDecodedBody<UpdateClientSettingReq>();
		session.Player.AvatarList.Where((AvatarScheme Avatar) => Avatar.AvatarId == Data.AvatarArtifactSwitch.AvatarId).FirstOrDefault()?.ArtifactSwitch(Data.AvatarArtifactSwitch);
		GetClientSettingRsp getClientSettingRsp = new GetClientSettingRsp
		{
			retcode = GetClientSettingRsp.Retcode.Succ,
			ClientSettingType = 4u,
			IsWeeklyGuideSwitchOn = false
		};
		List<AvatarArtifactSwitch> list = new List<AvatarArtifactSwitch>();
		AvatarArtifactSwitch avatarArtifactSwitch = Data.AvatarArtifactSwitch;
		list.Add(avatarArtifactSwitch);
		getClientSettingRsp.AvatarArtifactSwitchLists.AddRange(list);
		session.Send(Packet.FromProto(getClientSettingRsp, CmdId.GetClientSettingRsp));
	}
}
