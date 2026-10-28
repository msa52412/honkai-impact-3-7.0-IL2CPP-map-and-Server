using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChallengeModeArea : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AreaId;

	[ProtoMember(1, Name = "area_id")]
	public uint AreaId
	{
		get
		{
			return __pbn__AreaId.GetValueOrDefault();
		}
		set
		{
			__pbn__AreaId = value;
		}
	}

	[ProtoMember(2, Name = "avatar_list")]
	public List<ChallengeModeAvatar> AvatarLists { get; } = new List<ChallengeModeAvatar>();


	[ProtoMember(3, Name = "weapon_uid_list")]
	public uint[] WeaponUidLists { get; set; }

	[ProtoMember(4, Name = "stigmata_uid_list")]
	public uint[] StigmataUidLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAreaId()
	{
		return __pbn__AreaId.HasValue;
	}

	public void ResetAreaId()
	{
		__pbn__AreaId = null;
	}
}
