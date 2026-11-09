using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class NinjaActivityData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__NinjaId;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	[ProtoMember(1, Name = "ninja_id")]
	public uint NinjaId
	{
		get
		{
			return __pbn__NinjaId.GetValueOrDefault();
		}
		set
		{
			__pbn__NinjaId = value;
		}
	}

	[ProtoMember(2, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(3, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(4, Name = "slot_list")]
	public List<NinjaSlot> SlotLists { get; } = new List<NinjaSlot>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNinjaId()
	{
		return __pbn__NinjaId.HasValue;
	}

	public void ResetNinjaId()
	{
		__pbn__NinjaId = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}
}
