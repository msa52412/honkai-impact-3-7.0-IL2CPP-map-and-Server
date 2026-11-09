using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GachaProtectDisplayInfo : IExtensible
{
	[ProtoContract]
	public enum ProtectDisplayType
	{
		[ProtoEnum(Name = "NO_DISPLAY")]
		NoDisplay,
		[ProtoEnum(Name = "AVATAR_DISPLAY")]
		AvatarDisplay,
		[ProtoEnum(Name = "EQUIP_DISPLAY")]
		EquipDisplay
	}

	private IExtension __pbn__extensionData;

	private ProtectDisplayType? __pbn__protect_display_type;

	private uint? __pbn__DisplayKeyAvatar;

	private uint? __pbn__NoProtectGachaTimes;

	[ProtoMember(1)]
	[DefaultValue(ProtectDisplayType.NoDisplay)]
	public ProtectDisplayType protect_display_type
	{
		get
		{
			return __pbn__protect_display_type.GetValueOrDefault();
		}
		set
		{
			__pbn__protect_display_type = value;
		}
	}

	[ProtoMember(2, Name = "display_key_avatar")]
	public uint DisplayKeyAvatar
	{
		get
		{
			return __pbn__DisplayKeyAvatar.GetValueOrDefault();
		}
		set
		{
			__pbn__DisplayKeyAvatar = value;
		}
	}

	[ProtoMember(3, Name = "display_key_item_list")]
	public uint[] DisplayKeyItemLists { get; set; }

	[ProtoMember(4, Name = "droped_up_item_list")]
	public uint[] DropedUpItemLists { get; set; }

	[ProtoMember(5, Name = "no_protect_gacha_times")]
	public uint NoProtectGachaTimes
	{
		get
		{
			return __pbn__NoProtectGachaTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__NoProtectGachaTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeprotect_display_type()
	{
		return __pbn__protect_display_type.HasValue;
	}

	public void Resetprotect_display_type()
	{
		__pbn__protect_display_type = null;
	}

	public bool ShouldSerializeDisplayKeyAvatar()
	{
		return __pbn__DisplayKeyAvatar.HasValue;
	}

	public void ResetDisplayKeyAvatar()
	{
		__pbn__DisplayKeyAvatar = null;
	}

	public bool ShouldSerializeNoProtectGachaTimes()
	{
		return __pbn__NoProtectGachaTimes.HasValue;
	}

	public void ResetNoProtectGachaTimes()
	{
		__pbn__NoProtectGachaTimes = null;
	}
}
