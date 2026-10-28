using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LobbyStat : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__WaitPlayerNum;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "in_team_player_num_list")]
	public List<LobbyTeamPlayerNum> InTeamPlayerNumLists { get; } = new List<LobbyTeamPlayerNum>();


	[ProtoMember(3, Name = "wait_player_num")]
	public uint WaitPlayerNum
	{
		get
		{
			return __pbn__WaitPlayerNum.GetValueOrDefault();
		}
		set
		{
			__pbn__WaitPlayerNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeWaitPlayerNum()
	{
		return __pbn__WaitPlayerNum.HasValue;
	}

	public void ResetWaitPlayerNum()
	{
		__pbn__WaitPlayerNum = null;
	}
}
