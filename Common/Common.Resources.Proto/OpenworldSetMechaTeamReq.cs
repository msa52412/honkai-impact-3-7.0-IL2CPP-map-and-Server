using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldSetMechaTeamReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4506
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TeamIndex;

	private bool? __pbn__SetAsBattleTeam;

	[ProtoMember(1, Name = "team_index")]
	public uint TeamIndex
	{
		get
		{
			return __pbn__TeamIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__TeamIndex = value;
		}
	}

	[ProtoMember(2, Name = "mecha_list")]
	public List<OpenworldMechaTeamMember> MechaLists { get; } = new List<OpenworldMechaTeamMember>();


	[ProtoMember(3, Name = "set_as_battle_team")]
	public bool SetAsBattleTeam
	{
		get
		{
			return __pbn__SetAsBattleTeam.GetValueOrDefault();
		}
		set
		{
			__pbn__SetAsBattleTeam = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTeamIndex()
	{
		return __pbn__TeamIndex.HasValue;
	}

	public void ResetTeamIndex()
	{
		__pbn__TeamIndex = null;
	}

	public bool ShouldSerializeSetAsBattleTeam()
	{
		return __pbn__SetAsBattleTeam.HasValue;
	}

	public void ResetSetAsBattleTeam()
	{
		__pbn__SetAsBattleTeam = null;
	}
}
