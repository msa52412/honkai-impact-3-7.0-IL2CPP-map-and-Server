using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RefineStigmataRuneReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 193
	}

	private IExtension __pbn__extensionData;

	private StigmataRefineType? __pbn__Type;

	private uint? __pbn__UniqueId;

	private uint? __pbn__SpecialId;

	private uint? __pbn__LockRuneIndex;

	private bool? __pbn__IsRetry;

	private StigmataRefineTimesType? __pbn__TimesType;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(StigmataRefineType.StigmataRefineNormal)]
	public StigmataRefineType Type
	{
		get
		{
			return __pbn__Type ?? StigmataRefineType.StigmataRefineNormal;
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(3, Name = "consume_item_list")]
	public EquipmentItemList ConsumeItemList { get; set; }

	[ProtoMember(4, Name = "special_id")]
	public uint SpecialId
	{
		get
		{
			return __pbn__SpecialId.GetValueOrDefault();
		}
		set
		{
			__pbn__SpecialId = value;
		}
	}

	[ProtoMember(5, Name = "lock_rune_index")]
	public uint LockRuneIndex
	{
		get
		{
			return __pbn__LockRuneIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__LockRuneIndex = value;
		}
	}

	[ProtoMember(6, Name = "is_retry")]
	public bool IsRetry
	{
		get
		{
			return __pbn__IsRetry.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRetry = value;
		}
	}

	[ProtoMember(7, Name = "times_type")]
	[DefaultValue(StigmataRefineTimesType.StigmataRefineTimesOne)]
	public StigmataRefineTimesType TimesType
	{
		get
		{
			return __pbn__TimesType ?? StigmataRefineTimesType.StigmataRefineTimesOne;
		}
		set
		{
			__pbn__TimesType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeSpecialId()
	{
		return __pbn__SpecialId.HasValue;
	}

	public void ResetSpecialId()
	{
		__pbn__SpecialId = null;
	}

	public bool ShouldSerializeLockRuneIndex()
	{
		return __pbn__LockRuneIndex.HasValue;
	}

	public void ResetLockRuneIndex()
	{
		__pbn__LockRuneIndex = null;
	}

	public bool ShouldSerializeIsRetry()
	{
		return __pbn__IsRetry.HasValue;
	}

	public void ResetIsRetry()
	{
		__pbn__IsRetry = null;
	}

	public bool ShouldSerializeTimesType()
	{
		return __pbn__TimesType.HasValue;
	}

	public void ResetTimesType()
	{
		__pbn__TimesType = null;
	}
}
