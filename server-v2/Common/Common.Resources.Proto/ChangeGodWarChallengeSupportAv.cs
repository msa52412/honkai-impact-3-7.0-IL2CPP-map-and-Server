using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChangeGodWarChallengeSupportAvatarReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6154
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GodWarId;

	private uint? __pbn__TaleId;

	private uint? __pbn__ExtraItemId;

	private uint? __pbn__AssistantExtraItemId;

	[ProtoMember(1, Name = "god_war_id")]
	public uint GodWarId
	{
		get
		{
			return __pbn__GodWarId.GetValueOrDefault();
		}
		set
		{
			__pbn__GodWarId = value;
		}
	}

	[ProtoMember(2, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
		}
	}

	[ProtoMember(3, Name = "support_avatar_id_list")]
	public uint[] SupportAvatarIdLists { get; set; }

	[ProtoMember(4, Name = "extra_item_id")]
	public uint ExtraItemId
	{
		get
		{
			return __pbn__ExtraItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ExtraItemId = value;
		}
	}

	[ProtoMember(5, Name = "assistant_extra_item_id")]
	public uint AssistantExtraItemId
	{
		get
		{
			return __pbn__AssistantExtraItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__AssistantExtraItemId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGodWarId()
	{
		return __pbn__GodWarId.HasValue;
	}

	public void ResetGodWarId()
	{
		__pbn__GodWarId = null;
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}

	public bool ShouldSerializeExtraItemId()
	{
		return __pbn__ExtraItemId.HasValue;
	}

	public void ResetExtraItemId()
	{
		__pbn__ExtraItemId = null;
	}

	public bool ShouldSerializeAssistantExtraItemId()
	{
		return __pbn__AssistantExtraItemId.HasValue;
	}

	public void ResetAssistantExtraItemId()
	{
		__pbn__AssistantExtraItemId = null;
	}
}
[ProtoContract]
public class ChangeGodWarChallengeSupportAvatarRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6155
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SUPPORT_AVATAR_ERROR")]
		SupportAvatarError,
		[ProtoEnum(Name = "FEATURE_CLOSE")]
		FeatureClose,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "EXTRA_ITEM_ERROR")]
		ExtraItemError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GodWarId;

	private uint? __pbn__TaleId;

	private uint? __pbn__ExtraItemId;

	private uint? __pbn__AssistantExtraItemId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "god_war_id")]
	public uint GodWarId
	{
		get
		{
			return __pbn__GodWarId.GetValueOrDefault();
		}
		set
		{
			__pbn__GodWarId = value;
		}
	}

	[ProtoMember(3, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
		}
	}

	[ProtoMember(4, Name = "support_avatar_id_list")]
	public uint[] SupportAvatarIdLists { get; set; }

	[ProtoMember(5, Name = "extra_item_id")]
	public uint ExtraItemId
	{
		get
		{
			return __pbn__ExtraItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ExtraItemId = value;
		}
	}

	[ProtoMember(6, Name = "assistant_extra_item_id")]
	public uint AssistantExtraItemId
	{
		get
		{
			return __pbn__AssistantExtraItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__AssistantExtraItemId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGodWarId()
	{
		return __pbn__GodWarId.HasValue;
	}

	public void ResetGodWarId()
	{
		__pbn__GodWarId = null;
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}

	public bool ShouldSerializeExtraItemId()
	{
		return __pbn__ExtraItemId.HasValue;
	}

	public void ResetExtraItemId()
	{
		__pbn__ExtraItemId = null;
	}

	public bool ShouldSerializeAssistantExtraItemId()
	{
		return __pbn__AssistantExtraItemId.HasValue;
	}

	public void ResetAssistantExtraItemId()
	{
		__pbn__AssistantExtraItemId = null;
	}
}
