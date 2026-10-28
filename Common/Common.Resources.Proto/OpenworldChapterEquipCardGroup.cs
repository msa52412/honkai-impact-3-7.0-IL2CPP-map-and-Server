using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterEquipCardGroupReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4585
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupIdx;

	private uint? __pbn__HeroId;

	[ProtoMember(1, Name = "group_idx")]
	public uint GroupIdx
	{
		get
		{
			return __pbn__GroupIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupIdx = value;
		}
	}

	[ProtoMember(2, Name = "hero_id")]
	public uint HeroId
	{
		get
		{
			return __pbn__HeroId.GetValueOrDefault();
		}
		set
		{
			__pbn__HeroId = value;
		}
	}

	[ProtoMember(3, Name = "slot_list")]
	public List<OpenworldChapterHeroCardSlot> SlotLists { get; } = new List<OpenworldChapterHeroCardSlot>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGroupIdx()
	{
		return __pbn__GroupIdx.HasValue;
	}

	public void ResetGroupIdx()
	{
		__pbn__GroupIdx = null;
	}

	public bool ShouldSerializeHeroId()
	{
		return __pbn__HeroId.HasValue;
	}

	public void ResetHeroId()
	{
		__pbn__HeroId = null;
	}
}
[ProtoContract]
public class OpenworldChapterEquipCardGroupRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4586
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "GROUP_ERROR")]
		GroupError,
		[ProtoEnum(Name = "HERO_ERROR")]
		HeroError,
		[ProtoEnum(Name = "CARD_REPEATED")]
		CardRepeated,
		[ProtoEnum(Name = "CARD_NOT_EXIST")]
		CardNotExist,
		[ProtoEnum(Name = "SPECIAL_CARD_SLOT_LIMIT")]
		SpecialCardSlotLimit,
		[ProtoEnum(Name = "SLOT_LOCKED")]
		SlotLocked,
		[ProtoEnum(Name = "SLOT_REPEATED")]
		SlotRepeated
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
