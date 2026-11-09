using Common.Resources.Proto;

namespace PemukulPaku.GameServer.MPModule;

public class TeamMember
{
	public Session? Session;

	public uint Index;

	public LobbyClientStatus ClientStatus { get; set; } = LobbyClientStatus.LobbyClientNone;


	public LobbyMemberStatus Status { get; set; } = LobbyMemberStatus.LobbyMemberReady;


	public TeamMember(Session? session = null, uint index = 1u)
	{
		Session = session;
		Index = index;
	}
}
