using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Stigmata : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

	private uint? __pbn__Id;

	private uint? __pbn__Level;

	private uint? __pbn__Exp;

	private bool? __pbn__IsProtected;

	private bool? __pbn__IsAffixIdentify;

	private uint? __pbn__CancelProtectTime;

	private uint? __pbn__SlotNum;

	private uint? __pbn__RefineValue;

	private uint? __pbn__PromoteTimes;

	[ProtoMember(1, Name = "unique_id")]
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

	[ProtoMember(2, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(3, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(4, Name = "exp")]
	public uint Exp
	{
		get
		{
			return __pbn__Exp.GetValueOrDefault();
		}
		set
		{
			__pbn__Exp = value;
		}
	}

	[ProtoMember(5, Name = "is_protected")]
	public bool IsProtected
	{
		get
		{
			return __pbn__IsProtected.GetValueOrDefault();
		}
		set
		{
			__pbn__IsProtected = value;
		}
	}

	[ProtoMember(6, Name = "is_affix_identify")]
	public bool IsAffixIdentify
	{
		get
		{
			return __pbn__IsAffixIdentify.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAffixIdentify = value;
		}
	}

	[ProtoMember(7, Name = "cancel_protect_time")]
	public uint CancelProtectTime
	{
		get
		{
			return __pbn__CancelProtectTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CancelProtectTime = value;
		}
	}

	[ProtoMember(8, Name = "slot_num")]
	public uint SlotNum
	{
		get
		{
			return __pbn__SlotNum.GetValueOrDefault();
		}
		set
		{
			__pbn__SlotNum = value;
		}
	}

	[ProtoMember(9, Name = "rune_list")]
	public List<StigmataRune> RuneLists { get; } = new List<StigmataRune>();


	[ProtoMember(10, Name = "wait_select_rune_list")]
	public List<StigmataRune> WaitSelectRuneLists { get; } = new List<StigmataRune>();


	[ProtoMember(11, Name = "refine_value")]
	public uint RefineValue
	{
		get
		{
			return __pbn__RefineValue.GetValueOrDefault();
		}
		set
		{
			__pbn__RefineValue = value;
		}
	}

	[ProtoMember(12, Name = "promote_times")]
	public uint PromoteTimes
	{
		get
		{
			return __pbn__PromoteTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__PromoteTimes = value;
		}
	}

	[ProtoMember(16, Name = "wait_select_rune_group_list")]
	public List<StigmataRuneGroup> WaitSelectRuneGroupLists { get; } = new List<StigmataRuneGroup>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeExp()
	{
		return __pbn__Exp.HasValue;
	}

	public void ResetExp()
	{
		__pbn__Exp = null;
	}

	public bool ShouldSerializeIsProtected()
	{
		return __pbn__IsProtected.HasValue;
	}

	public void ResetIsProtected()
	{
		__pbn__IsProtected = null;
	}

	public bool ShouldSerializeIsAffixIdentify()
	{
		return __pbn__IsAffixIdentify.HasValue;
	}

	public void ResetIsAffixIdentify()
	{
		__pbn__IsAffixIdentify = null;
	}

	public bool ShouldSerializeCancelProtectTime()
	{
		return __pbn__CancelProtectTime.HasValue;
	}

	public void ResetCancelProtectTime()
	{
		__pbn__CancelProtectTime = null;
	}

	public bool ShouldSerializeSlotNum()
	{
		return __pbn__SlotNum.HasValue;
	}

	public void ResetSlotNum()
	{
		__pbn__SlotNum = null;
	}

	public bool ShouldSerializeRefineValue()
	{
		return __pbn__RefineValue.HasValue;
	}

	public void ResetRefineValue()
	{
		__pbn__RefineValue = null;
	}

	public bool ShouldSerializePromoteTimes()
	{
		return __pbn__PromoteTimes.HasValue;
	}

	public void ResetPromoteTimes()
	{
		__pbn__PromoteTimes = null;
	}
}
