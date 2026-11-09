using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BulletinActivityConfig : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__ActivityType;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__BulletinId;

	private string __pbn__TypeParamStr;

	private uint? __pbn__MinPlayerLevel;

	private uint? __pbn__MaxPlayerLevel;

	private string __pbn__TitleName;

	private string __pbn__TitleImage;

	private string __pbn__ImagePath;

	private string __pbn__LinkButton;

	private string __pbn__Description;

	private uint? __pbn__Weight;

	private string __pbn__BackgroundPath;

	private string __pbn__TextImagePath;

	private string __pbn__UnlockParamStr;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "activity_type")]
	public uint ActivityType
	{
		get
		{
			return __pbn__ActivityType.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityType = value;
		}
	}

	[ProtoMember(3, Name = "begin_time")]
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

	[ProtoMember(4, Name = "end_time")]
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

	[ProtoMember(5, Name = "bulletin_id")]
	public uint BulletinId
	{
		get
		{
			return __pbn__BulletinId.GetValueOrDefault();
		}
		set
		{
			__pbn__BulletinId = value;
		}
	}

	[ProtoMember(6, Name = "mission_ids")]
	public uint[] MissionIds { get; set; }

	[ProtoMember(7, Name = "type_param_list")]
	public uint[] TypeParamLists { get; set; }

	[ProtoMember(8, Name = "type_param_str")]
	[DefaultValue("")]
	public string TypeParamStr
	{
		get
		{
			return __pbn__TypeParamStr ?? "";
		}
		set
		{
			__pbn__TypeParamStr = value;
		}
	}

	[ProtoMember(9, Name = "min_player_level")]
	public uint MinPlayerLevel
	{
		get
		{
			return __pbn__MinPlayerLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MinPlayerLevel = value;
		}
	}

	[ProtoMember(10, Name = "max_player_level")]
	public uint MaxPlayerLevel
	{
		get
		{
			return __pbn__MaxPlayerLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxPlayerLevel = value;
		}
	}

	[ProtoMember(11, Name = "item_list")]
	public uint[] ItemLists { get; set; }

	[ProtoMember(12, Name = "title_name")]
	[DefaultValue("")]
	public string TitleName
	{
		get
		{
			return __pbn__TitleName ?? "";
		}
		set
		{
			__pbn__TitleName = value;
		}
	}

	[ProtoMember(13, Name = "title_image")]
	[DefaultValue("")]
	public string TitleImage
	{
		get
		{
			return __pbn__TitleImage ?? "";
		}
		set
		{
			__pbn__TitleImage = value;
		}
	}

	[ProtoMember(14, Name = "image_path")]
	[DefaultValue("")]
	public string ImagePath
	{
		get
		{
			return __pbn__ImagePath ?? "";
		}
		set
		{
			__pbn__ImagePath = value;
		}
	}

	[ProtoMember(15, Name = "link_button")]
	[DefaultValue("")]
	public string LinkButton
	{
		get
		{
			return __pbn__LinkButton ?? "";
		}
		set
		{
			__pbn__LinkButton = value;
		}
	}

	[ProtoMember(16, Name = "description")]
	[DefaultValue("")]
	public string Description
	{
		get
		{
			return __pbn__Description ?? "";
		}
		set
		{
			__pbn__Description = value;
		}
	}

	[ProtoMember(17, Name = "weight")]
	public uint Weight
	{
		get
		{
			return __pbn__Weight.GetValueOrDefault();
		}
		set
		{
			__pbn__Weight = value;
		}
	}

	[ProtoMember(18, Name = "background_path")]
	[DefaultValue("")]
	public string BackgroundPath
	{
		get
		{
			return __pbn__BackgroundPath ?? "";
		}
		set
		{
			__pbn__BackgroundPath = value;
		}
	}

	[ProtoMember(19, Name = "text_image_path")]
	[DefaultValue("")]
	public string TextImagePath
	{
		get
		{
			return __pbn__TextImagePath ?? "";
		}
		set
		{
			__pbn__TextImagePath = value;
		}
	}

	[ProtoMember(20, Name = "currency_list")]
	public uint[] CurrencyLists { get; set; }

	[ProtoMember(21, Name = "unlock_param_str")]
	[DefaultValue("")]
	public string UnlockParamStr
	{
		get
		{
			return __pbn__UnlockParamStr ?? "";
		}
		set
		{
			__pbn__UnlockParamStr = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeActivityType()
	{
		return __pbn__ActivityType.HasValue;
	}

	public void ResetActivityType()
	{
		__pbn__ActivityType = null;
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

	public bool ShouldSerializeBulletinId()
	{
		return __pbn__BulletinId.HasValue;
	}

	public void ResetBulletinId()
	{
		__pbn__BulletinId = null;
	}

	public bool ShouldSerializeTypeParamStr()
	{
		return __pbn__TypeParamStr != null;
	}

	public void ResetTypeParamStr()
	{
		__pbn__TypeParamStr = null;
	}

	public bool ShouldSerializeMinPlayerLevel()
	{
		return __pbn__MinPlayerLevel.HasValue;
	}

	public void ResetMinPlayerLevel()
	{
		__pbn__MinPlayerLevel = null;
	}

	public bool ShouldSerializeMaxPlayerLevel()
	{
		return __pbn__MaxPlayerLevel.HasValue;
	}

	public void ResetMaxPlayerLevel()
	{
		__pbn__MaxPlayerLevel = null;
	}

	public bool ShouldSerializeTitleName()
	{
		return __pbn__TitleName != null;
	}

	public void ResetTitleName()
	{
		__pbn__TitleName = null;
	}

	public bool ShouldSerializeTitleImage()
	{
		return __pbn__TitleImage != null;
	}

	public void ResetTitleImage()
	{
		__pbn__TitleImage = null;
	}

	public bool ShouldSerializeImagePath()
	{
		return __pbn__ImagePath != null;
	}

	public void ResetImagePath()
	{
		__pbn__ImagePath = null;
	}

	public bool ShouldSerializeLinkButton()
	{
		return __pbn__LinkButton != null;
	}

	public void ResetLinkButton()
	{
		__pbn__LinkButton = null;
	}

	public bool ShouldSerializeDescription()
	{
		return __pbn__Description != null;
	}

	public void ResetDescription()
	{
		__pbn__Description = null;
	}

	public bool ShouldSerializeWeight()
	{
		return __pbn__Weight.HasValue;
	}

	public void ResetWeight()
	{
		__pbn__Weight = null;
	}

	public bool ShouldSerializeBackgroundPath()
	{
		return __pbn__BackgroundPath != null;
	}

	public void ResetBackgroundPath()
	{
		__pbn__BackgroundPath = null;
	}

	public bool ShouldSerializeTextImagePath()
	{
		return __pbn__TextImagePath != null;
	}

	public void ResetTextImagePath()
	{
		__pbn__TextImagePath = null;
	}

	public bool ShouldSerializeUnlockParamStr()
	{
		return __pbn__UnlockParamStr != null;
	}

	public void ResetUnlockParamStr()
	{
		__pbn__UnlockParamStr = null;
	}
}
