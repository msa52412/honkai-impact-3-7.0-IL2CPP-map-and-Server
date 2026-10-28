using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.MPModule;

public class Team
{
	public uint MinLevel;

	public uint LeaderUid;

	public LobbyEnterType LobbyEnterType;

	public LobbyStatus LobbyStatus = LobbyStatus.LobbyPreparing;

	public List<TeamMember> Members;

	public string Name;

	public uint StageId { get; set; }

	public Team(uint stageId, uint minLevel, LobbyEnterType lobbyEnterType, in Session leader, string name)
	{
		StageId = stageId;
		MinLevel = minLevel;
		LobbyEnterType = lobbyEnterType;
		Members = new List<TeamMember>
		{
			new TeamMember(leader),
			new TeamMember(null, 2u),
			new TeamMember(null, 3u)
		};
		LeaderUid = leader.Player.User.Uid;
		Name = name;
	}

	public void Join(Session session)
	{
		if (Members[1].Session == null)
		{
			Members[1].Session = session;
		}
		else if (Members[2].Session == null)
		{
			Members[2].Session = session;
		}
		Lobby.GetInstance().SyncTeam(LeaderUid);
	}

	public MpTeamData GetMpTeamData()
	{
		MpTeamData mpTeamData = new MpTeamData
		{
			LeaderUid = LeaderUid,
			TeamId = LeaderUid,
			LobbyEnterType = LobbyEnterType,
			LobbyStatus = LobbyStatus,
			MinLevel = MinLevel,
			MaxLevel = 0u,
			StageId = StageId,
			TeamName = Name,
			Status = MpTeamStatus.TeamStatusInLobby
		};
		mpTeamData.MemberLists.AddRange(Members.Select((TeamMember member) => (member.Session == null) ? new MpTeamMember
		{
			Index = member.Index,
			DressId = 0u
		} : new MpTeamMember
		{
			Index = member.Index,
			Uid = member.Session!.Player.User.Uid,
			MpExp = 0u,
			Stamina = (uint)member.Session!.Player.User.Stamina,
			HeadAvatarId = member.Session!.Player.GetDetailData().LeaderAvatar.AvatarId,
			DressId = member.Session!.Player.GetDetailData().LeaderAvatar.DressId,
			PunishEndTime = 0u,
			MemberInfo = new MpTeamMemberInfo
			{
				Card = member.Session!.Player.GetCardData(),
				Detail = member.Session!.Player.GetDetailData()
			},
			Status = member.Status,
			ClientStatus = member.ClientStatus,
			AvatarTrialId = 0u,
			IsWild = true,
			RegionName = Global.config.Gameserver.RegionName,
			FrameId = member.Session!.Player.GetDetailData().FrameId,
			CustomHeadId = member.Session!.Player.GetDetailData().CustomHeadId,
			NewbieId = 0u
		}));
		return mpTeamData;
	}

	public override string ToString()
	{
		return Name;
	}
}
