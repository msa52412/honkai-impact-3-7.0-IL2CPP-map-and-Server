using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GachaDisplayCommonData : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__TitleImage;

	private string __pbn__SupplyImage;

	private string __pbn__Title;

	private string __pbn__Content;

	private string __pbn__Rule;

	private string __pbn__ContentDetail;

	private string __pbn__ContentUrl;

	private string __pbn__BtnImagePath;

	private bool? __pbn__IsEnablePrompt;

	private uint? __pbn__GachaId;

	private uint? __pbn__DataBeginTime;

	private uint? __pbn__DataEndTime;

	private string __pbn__DisplayExt;

	private uint? __pbn__FreeGachaTimes;

	[ProtoMember(1, Name = "title_image")]
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

	[ProtoMember(2, Name = "supply_image")]
	[DefaultValue("")]
	public string SupplyImage
	{
		get
		{
			return __pbn__SupplyImage ?? "";
		}
		set
		{
			__pbn__SupplyImage = value;
		}
	}

	[ProtoMember(3, Name = "title")]
	[DefaultValue("")]
	public string Title
	{
		get
		{
			return __pbn__Title ?? "";
		}
		set
		{
			__pbn__Title = value;
		}
	}

	[ProtoMember(4, Name = "content")]
	[DefaultValue("")]
	public string Content
	{
		get
		{
			return __pbn__Content ?? "";
		}
		set
		{
			__pbn__Content = value;
		}
	}

	[ProtoMember(5, Name = "up_avatar_list")]
	public uint[] UpAvatarLists { get; set; }

	[ProtoMember(6, Name = "up_weapon_list")]
	public List<WeaponDetailData> UpWeaponLists { get; } = new List<WeaponDetailData>();


	[ProtoMember(7, Name = "up_stigmata_list")]
	public List<StigmataDetailData> UpStigmataLists { get; } = new List<StigmataDetailData>();


	[ProtoMember(8, Name = "rule")]
	[DefaultValue("")]
	public string Rule
	{
		get
		{
			return __pbn__Rule ?? "";
		}
		set
		{
			__pbn__Rule = value;
		}
	}

	[ProtoMember(9, Name = "content_detail")]
	[DefaultValue("")]
	public string ContentDetail
	{
		get
		{
			return __pbn__ContentDetail ?? "";
		}
		set
		{
			__pbn__ContentDetail = value;
		}
	}

	[ProtoMember(10, Name = "up_item_list")]
	public uint[] UpItemLists { get; set; }

	[ProtoMember(11, Name = "content_url")]
	[DefaultValue("")]
	public string ContentUrl
	{
		get
		{
			return __pbn__ContentUrl ?? "";
		}
		set
		{
			__pbn__ContentUrl = value;
		}
	}

	[ProtoMember(12, Name = "btn_image_path")]
	[DefaultValue("")]
	public string BtnImagePath
	{
		get
		{
			return __pbn__BtnImagePath ?? "";
		}
		set
		{
			__pbn__BtnImagePath = value;
		}
	}

	[ProtoMember(13, Name = "sort_item_list")]
	public List<GachaSortItem> SortItemLists { get; } = new List<GachaSortItem>();


	[ProtoMember(14, Name = "is_enable_prompt")]
	public bool IsEnablePrompt
	{
		get
		{
			return __pbn__IsEnablePrompt.GetValueOrDefault();
		}
		set
		{
			__pbn__IsEnablePrompt = value;
		}
	}

	[ProtoMember(15, Name = "gacha_id")]
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

	[ProtoMember(16, Name = "data_begin_time")]
	public uint DataBeginTime
	{
		get
		{
			return __pbn__DataBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__DataBeginTime = value;
		}
	}

	[ProtoMember(17, Name = "data_end_time")]
	public uint DataEndTime
	{
		get
		{
			return __pbn__DataEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__DataEndTime = value;
		}
	}

	[ProtoMember(18, Name = "up_elf_list")]
	public uint[] UpElfLists { get; set; }

	[ProtoMember(19, Name = "display_ext")]
	[DefaultValue("")]
	public string DisplayExt
	{
		get
		{
			return __pbn__DisplayExt ?? "";
		}
		set
		{
			__pbn__DisplayExt = value;
		}
	}

	[ProtoMember(20, Name = "free_gacha_times")]
	public uint FreeGachaTimes
	{
		get
		{
			return __pbn__FreeGachaTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__FreeGachaTimes = value;
		}
	}

	[ProtoMember(21, Name = "free_info_list")]
	public List<GachaFreeInfo> FreeInfoLists { get; } = new List<GachaFreeInfo>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTitleImage()
	{
		return __pbn__TitleImage != null;
	}

	public void ResetTitleImage()
	{
		__pbn__TitleImage = null;
	}

	public bool ShouldSerializeSupplyImage()
	{
		return __pbn__SupplyImage != null;
	}

	public void ResetSupplyImage()
	{
		__pbn__SupplyImage = null;
	}

	public bool ShouldSerializeTitle()
	{
		return __pbn__Title != null;
	}

	public void ResetTitle()
	{
		__pbn__Title = null;
	}

	public bool ShouldSerializeContent()
	{
		return __pbn__Content != null;
	}

	public void ResetContent()
	{
		__pbn__Content = null;
	}

	public bool ShouldSerializeRule()
	{
		return __pbn__Rule != null;
	}

	public void ResetRule()
	{
		__pbn__Rule = null;
	}

	public bool ShouldSerializeContentDetail()
	{
		return __pbn__ContentDetail != null;
	}

	public void ResetContentDetail()
	{
		__pbn__ContentDetail = null;
	}

	public bool ShouldSerializeContentUrl()
	{
		return __pbn__ContentUrl != null;
	}

	public void ResetContentUrl()
	{
		__pbn__ContentUrl = null;
	}

	public bool ShouldSerializeBtnImagePath()
	{
		return __pbn__BtnImagePath != null;
	}

	public void ResetBtnImagePath()
	{
		__pbn__BtnImagePath = null;
	}

	public bool ShouldSerializeIsEnablePrompt()
	{
		return __pbn__IsEnablePrompt.HasValue;
	}

	public void ResetIsEnablePrompt()
	{
		__pbn__IsEnablePrompt = null;
	}

	public bool ShouldSerializeGachaId()
	{
		return __pbn__GachaId.HasValue;
	}

	public void ResetGachaId()
	{
		__pbn__GachaId = null;
	}

	public bool ShouldSerializeDataBeginTime()
	{
		return __pbn__DataBeginTime.HasValue;
	}

	public void ResetDataBeginTime()
	{
		__pbn__DataBeginTime = null;
	}

	public bool ShouldSerializeDataEndTime()
	{
		return __pbn__DataEndTime.HasValue;
	}

	public void ResetDataEndTime()
	{
		__pbn__DataEndTime = null;
	}

	public bool ShouldSerializeDisplayExt()
	{
		return __pbn__DisplayExt != null;
	}

	public void ResetDisplayExt()
	{
		__pbn__DisplayExt = null;
	}

	public bool ShouldSerializeFreeGachaTimes()
	{
		return __pbn__FreeGachaTimes.HasValue;
	}

	public void ResetFreeGachaTimes()
	{
		__pbn__FreeGachaTimes = null;
	}
}
