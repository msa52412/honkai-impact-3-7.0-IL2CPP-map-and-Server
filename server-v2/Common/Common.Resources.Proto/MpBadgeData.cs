using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpBadgeData : IExtensible
{
	private IExtension __pbn__extensionData;

	private ulong? __pbn__EquipedBadgeUidU64;

	[ProtoMember(1, Name = "equiped_badge_uid_u64")]
	public ulong EquipedBadgeUidU64
	{
		get
		{
			return __pbn__EquipedBadgeUidU64.GetValueOrDefault();
		}
		set
		{
			__pbn__EquipedBadgeUidU64 = value;
		}
	}

	[ProtoMember(2, Name = "badge_list")]
	public List<MpBadge> BadgeLists { get; } = new List<MpBadge>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEquipedBadgeUidU64()
	{
		return __pbn__EquipedBadgeUidU64.HasValue;
	}

	public void ResetEquipedBadgeUidU64()
	{
		__pbn__EquipedBadgeUidU64 = null;
	}
}
