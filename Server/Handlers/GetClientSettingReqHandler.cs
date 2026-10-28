using System.Collections.Generic;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetClientSettingReq)]
internal class GetClientSettingReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetClientSettingReq decodedBody = packet.GetDecodedBody<GetClientSettingReq>();
		GetClientSettingRsp getClientSettingRsp = new GetClientSettingRsp
		{
			retcode = GetClientSettingRsp.Retcode.Succ,
			ClientSettingType = decodedBody.ClientSettingType,
			IsWeeklyGuideSwitchOn = true
		};
		List<AvatarArtifactSwitch> list = new List<AvatarArtifactSwitch>();
		AvatarScheme[] avatarList = session.Player.AvatarList;
		foreach (AvatarScheme avatarScheme in avatarList)
		{
			if (avatarScheme.AvatarArtifact != null)
			{
				list.Add(new AvatarArtifactSwitch
				{
					AvatarId = avatarScheme.AvatarArtifact.ArtifactId,
					IsArtifactSwitchOn = avatarScheme.AvatarArtifact.IsArtifactSwitchOn
				});
			}
		}
		getClientSettingRsp.AvatarArtifactSwitchLists.AddRange(list);
		session.Send(Packet.FromProto(getClientSettingRsp, CmdId.GetClientSettingRsp));
	}
}
