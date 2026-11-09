using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ProtectEntry : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TotalTimes;

	private uint? __pbn__DropTimes;

	[ProtoMember(1, Name = "possible_item_list")]
	public List<DropItem> PossibleItemLists { get; } = new List<DropItem>();


	[ProtoMember(2, Name = "total_times")]
	public uint TotalTimes
	{
		get
		{
			return __pbn__TotalTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalTimes = value;
		}
	}

	[ProtoMember(3, Name = "drop_times")]
	public uint DropTimes
	{
		get
		{
			return __pbn__DropTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__DropTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTotalTimes()
	{
		return __pbn__TotalTimes.HasValue;
	}

	public void ResetTotalTimes()
	{
		__pbn__TotalTimes = null;
	}

	public bool ShouldSerializeDropTimes()
	{
		return __pbn__DropTimes.HasValue;
	}

	public void ResetDropTimes()
	{
		__pbn__DropTimes = null;
	}
}
