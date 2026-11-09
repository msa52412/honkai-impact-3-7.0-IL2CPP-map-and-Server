using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SelectGodWarChallengeAvatarReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6152
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GodWarId;

	private uint? __pbn__TaleId;

	private uint? __pbn__ElfId;

	private uint? __pbn__ExtraItemId;

	private bool? __pbn__IsUseVirtualAvatar;

	private uint? __pbn__StepLevel;

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

	[ProtoMember(3, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	[ProtoMember(4, Name = "support_avatar_id_list")]
	public uint[] SupportAvatarIdLists { get; set; }

	[ProtoMember(5, Name = "elf_id")]
	public uint ElfId
	{
		get
		{
			return __pbn__ElfId.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfId = value;
		}
	}

	[ProtoMember(6, Name = "extra_item_id")]
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

	[ProtoMember(7, Name = "is_use_virtual_avatar")]
	public bool IsUseVirtualAvatar
	{
		get
		{
			return __pbn__IsUseVirtualAvatar.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUseVirtualAvatar = value;
		}
	}

	[ProtoMember(8, Name = "boon_buff_id_list")]
	public uint[] BoonBuffIdLists { get; set; }

	[ProtoMember(9, Name = "step_level")]
	public uint StepLevel
	{
		get
		{
			return __pbn__StepLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__StepLevel = value;
		}
	}

	[ProtoMember(10, Name = "assistant_extra_item_id")]
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

	public bool ShouldSerializeElfId()
	{
		return __pbn__ElfId.HasValue;
	}

	public void ResetElfId()
	{
		__pbn__ElfId = null;
	}

	public bool ShouldSerializeExtraItemId()
	{
		return __pbn__ExtraItemId.HasValue;
	}

	public void ResetExtraItemId()
	{
		__pbn__ExtraItemId = null;
	}

	public bool ShouldSerializeIsUseVirtualAvatar()
	{
		return __pbn__IsUseVirtualAvatar.HasValue;
	}

	public void ResetIsUseVirtualAvatar()
	{
		__pbn__IsUseVirtualAvatar = null;
	}

	public bool ShouldSerializeStepLevel()
	{
		return __pbn__StepLevel.HasValue;
	}

	public void ResetStepLevel()
	{
		__pbn__StepLevel = null;
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
