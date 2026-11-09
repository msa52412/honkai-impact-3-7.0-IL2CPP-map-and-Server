using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterHeroCardGroup : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__HeroId;

	private string __pbn__GroupName;

	private bool? __pbn__IsActive;

	[ProtoMember(1, Name = "hero_id")]
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

	[ProtoMember(2, Name = "group_name")]
	[DefaultValue("")]
	public string GroupName
	{
		get
		{
			return __pbn__GroupName ?? "";
		}
		set
		{
			__pbn__GroupName = value;
		}
	}

	[ProtoMember(3, Name = "is_active")]
	public bool IsActive
	{
		get
		{
			return __pbn__IsActive.GetValueOrDefault();
		}
		set
		{
			__pbn__IsActive = value;
		}
	}

	[ProtoMember(4, Name = "slot_list")]
	public List<OpenworldChapterHeroCardSlot> SlotLists { get; } = new List<OpenworldChapterHeroCardSlot>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHeroId()
	{
		return __pbn__HeroId.HasValue;
	}

	public void ResetHeroId()
	{
		__pbn__HeroId = null;
	}

	public bool ShouldSerializeGroupName()
	{
		return __pbn__GroupName != null;
	}

	public void ResetGroupName()
	{
		__pbn__GroupName = null;
	}

	public bool ShouldSerializeIsActive()
	{
		return __pbn__IsActive.HasValue;
	}

	public void ResetIsActive()
	{
		__pbn__IsActive = null;
	}
}
