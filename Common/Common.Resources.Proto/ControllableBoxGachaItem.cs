using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ControllableBoxGachaItem : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__DropId;

	private uint? __pbn__ItemId;

	private uint? __pbn__ItemLevel;

	private uint? __pbn__ItemNum;

	private bool? __pbn__IsShining;

	private bool? __pbn__IsCore;

	[ProtoMember(1, Name = "drop_id")]
	public uint DropId
	{
		get
		{
			return __pbn__DropId.GetValueOrDefault();
		}
		set
		{
			__pbn__DropId = value;
		}
	}

	[ProtoMember(2, Name = "item_id")]
	public uint ItemId
	{
		get
		{
			return __pbn__ItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemId = value;
		}
	}

	[ProtoMember(3, Name = "item_level")]
	public uint ItemLevel
	{
		get
		{
			return __pbn__ItemLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemLevel = value;
		}
	}

	[ProtoMember(4, Name = "item_num")]
	public uint ItemNum
	{
		get
		{
			return __pbn__ItemNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemNum = value;
		}
	}

	[ProtoMember(5, Name = "is_shining")]
	public bool IsShining
	{
		get
		{
			return __pbn__IsShining.GetValueOrDefault();
		}
		set
		{
			__pbn__IsShining = value;
		}
	}

	[ProtoMember(6, Name = "is_core")]
	public bool IsCore
	{
		get
		{
			return __pbn__IsCore.GetValueOrDefault();
		}
		set
		{
			__pbn__IsCore = value;
		}
	}

	[ProtoMember(9, Name = "display_setting_dict")]
	public SettingDict DisplaySettingDict { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDropId()
	{
		return __pbn__DropId.HasValue;
	}

	public void ResetDropId()
	{
		__pbn__DropId = null;
	}

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}

	public bool ShouldSerializeItemLevel()
	{
		return __pbn__ItemLevel.HasValue;
	}

	public void ResetItemLevel()
	{
		__pbn__ItemLevel = null;
	}

	public bool ShouldSerializeItemNum()
	{
		return __pbn__ItemNum.HasValue;
	}

	public void ResetItemNum()
	{
		__pbn__ItemNum = null;
	}

	public bool ShouldSerializeIsShining()
	{
		return __pbn__IsShining.HasValue;
	}

	public void ResetIsShining()
	{
		__pbn__IsShining = null;
	}

	public bool ShouldSerializeIsCore()
	{
		return __pbn__IsCore.HasValue;
	}

	public void ResetIsCore()
	{
		__pbn__IsCore = null;
	}
}
