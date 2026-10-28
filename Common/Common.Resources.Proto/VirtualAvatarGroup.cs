using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VirtualAvatarGroup : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupId;

	private bool? __pbn__IsLocked;

	[ProtoMember(1, Name = "group_id")]
	public uint GroupId
	{
		get
		{
			return __pbn__GroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupId = value;
		}
	}

	[ProtoMember(2, Name = "virtual_avatar_list")]
	public List<VirtualAvatar> VirtualAvatarLists { get; } = new List<VirtualAvatar>();


	[ProtoMember(3, Name = "virtual_weapon_id_list")]
	public uint[] VirtualWeaponIdLists { get; set; }

	[ProtoMember(4, Name = "virtual_stigmata_id_list")]
	public uint[] VirtualStigmataIdLists { get; set; }

	[ProtoMember(5, Name = "virtual_avatar_team_list")]
	public uint[] VirtualAvatarTeamLists { get; set; }

	[ProtoMember(6, Name = "virtual_role_list")]
	public List<VirtualTrainRole> VirtualRoleLists { get; } = new List<VirtualTrainRole>();


	[ProtoMember(7, Name = "is_locked")]
	public bool IsLocked
	{
		get
		{
			return __pbn__IsLocked.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLocked = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGroupId()
	{
		return __pbn__GroupId.HasValue;
	}

	public void ResetGroupId()
	{
		__pbn__GroupId = null;
	}

	public bool ShouldSerializeIsLocked()
	{
		return __pbn__IsLocked.HasValue;
	}

	public void ResetIsLocked()
	{
		__pbn__IsLocked = null;
	}
}
