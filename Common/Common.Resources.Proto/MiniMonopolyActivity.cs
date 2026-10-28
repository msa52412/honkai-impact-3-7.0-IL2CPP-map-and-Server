using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MiniMonopolyActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private bool? __pbn__IsMapSwitchable;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "mini_monopoly_map_list")]
	public List<MiniMonopolyMap> MiniMonopolyMapLists { get; } = new List<MiniMonopolyMap>();


	[ProtoMember(3, Name = "is_map_switchable")]
	public bool IsMapSwitchable
	{
		get
		{
			return __pbn__IsMapSwitchable.GetValueOrDefault();
		}
		set
		{
			__pbn__IsMapSwitchable = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeIsMapSwitchable()
	{
		return __pbn__IsMapSwitchable.HasValue;
	}

	public void ResetIsMapSwitchable()
	{
		__pbn__IsMapSwitchable = null;
	}
}
