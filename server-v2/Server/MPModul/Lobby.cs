using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.MPModule;

public class Lobby
{
	private static Lobby? Instance;

	public readonly Dictionary<uint, Team> Teams = new Dictionary<uint, Team>();

	public static Lobby GetInstance()
	{
		return Instance ?? (Instance = new Lobby());
	}

	public Team CreateTeam(Team team)
	{
		if (Teams.GetValueOrDefault(team.LeaderUid) == null)
		{
			Teams.Add(team.LeaderUid, team);
		}
		SyncTeam(team.LeaderUid);
		return team;
	}

	public void SyncTeam(uint teamId)
	{
		Teams.TryGetValue(teamId, out var value);
		if (value == null)
		{
			return;
		}
		MpTeamSyncNotify proto = new MpTeamSyncNotify
		{
			TeamData = value.GetMpTeamData()
		};
		foreach (Session item in from x in value.Members
			where x.Session != null
			select x.Session)
		{
			item?.Send(Packet.FromProto(proto, CmdId.MpTeamSyncNotify));
		}
	}
}
