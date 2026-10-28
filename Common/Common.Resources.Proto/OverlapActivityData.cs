using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OverlapActivityData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__OverlapId;

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

	[ProtoMember(2, Name = "grid_list")]
	public List<OverlapGrid> GridLists { get; } = new List<OverlapGrid>();


	[ProtoMember(3, Name = "has_take_reward_id_list")]
	public uint[] HasTakeRewardIdLists { get; set; }

	[ProtoMember(4, Name = "overlap_id")]
	public uint OverlapId
	{
		get
		{
			return __pbn__OverlapId.GetValueOrDefault();
		}
		set
		{
			__pbn__OverlapId = value;
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

	public bool ShouldSerializeOverlapId()
	{
		return __pbn__OverlapId.HasValue;
	}

	public void ResetOverlapId()
	{
		__pbn__OverlapId = null;
	}
}
