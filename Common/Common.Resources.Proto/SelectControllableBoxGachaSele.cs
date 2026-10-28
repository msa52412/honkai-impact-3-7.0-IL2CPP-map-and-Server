using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SelectControllableBoxGachaSelectableGroupItemReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4718
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GachaId;

	private uint? __pbn__GroupId;

	private uint? __pbn__SelectItemId;

	[ProtoMember(1, Name = "gacha_id")]
	public uint GachaId
	{
		get
		{
			return __pbn__GachaId.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaId = value;
		}
	}

	[ProtoMember(2, Name = "group_id")]
	public uint GroupId
	{
		get
		{
			return __pbn__GroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupId = value;
		}
	}

	[ProtoMember(3, Name = "select_item_id")]
	public uint SelectItemId
	{
		get
		{
			return __pbn__SelectItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__SelectItemId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGachaId()
	{
		return __pbn__GachaId.HasValue;
	}

	public void ResetGachaId()
	{
		__pbn__GachaId = null;
	}

	public bool ShouldSerializeGroupId()
	{
		return __pbn__GroupId.HasValue;
	}

	public void ResetGroupId()
	{
		__pbn__GroupId = null;
	}

	public bool ShouldSerializeSelectItemId()
	{
		return __pbn__SelectItemId.HasValue;
	}

	public void ResetSelectItemId()
	{
		__pbn__SelectItemId = null;
	}
}
[ProtoContract]
public class SelectControllableBoxGachaSelectableGroupItemRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4719
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "GACHA_ID_ERROR")]
		GachaIdError,
		[ProtoEnum(Name = "GROUP_ID_ERROR")]
		GroupIdError,
		[ProtoEnum(Name = "ITEM_ID_ERROR")]
		ItemIdError,
		[ProtoEnum(Name = "FIRST_IS_TAKEN")]
		FirstIsTaken
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GachaId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "gacha_id")]
	public uint GachaId
	{
		get
		{
			return __pbn__GachaId.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaId = value;
		}
	}

	[ProtoMember(3, Name = "group_info")]
	public ControllableBoxGachaSelectableGroup GroupInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGachaId()
	{
		return __pbn__GachaId.HasValue;
	}

	public void ResetGachaId()
	{
		__pbn__GachaId = null;
	}
}
