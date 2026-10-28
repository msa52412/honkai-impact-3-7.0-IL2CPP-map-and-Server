using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BoxGachaDisplayData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PreGachaId;

	private string __pbn__WebLink;

	private uint? __pbn__LinkType;

	private string __pbn__LinkPara;

	private string __pbn__LinkParaStr;

	[ProtoMember(1, Name = "item_list")]
	public List<BoxGachaItemData> ItemLists { get; } = new List<BoxGachaItemData>();


	[ProtoMember(2, Name = "show_list")]
	public List<GachaItem> ShowLists { get; } = new List<GachaItem>();


	[ProtoMember(3, Name = "pre_gacha_id")]
	public uint PreGachaId
	{
		get
		{
			return __pbn__PreGachaId.GetValueOrDefault();
		}
		set
		{
			__pbn__PreGachaId = value;
		}
	}

	[ProtoMember(4, Name = "web_link")]
	[DefaultValue("")]
	public string WebLink
	{
		get
		{
			return __pbn__WebLink ?? "";
		}
		set
		{
			__pbn__WebLink = value;
		}
	}

	[ProtoMember(5, Name = "link_type")]
	public uint LinkType
	{
		get
		{
			return __pbn__LinkType.GetValueOrDefault();
		}
		set
		{
			__pbn__LinkType = value;
		}
	}

	[ProtoMember(6, Name = "link_para")]
	[DefaultValue("")]
	public string LinkPara
	{
		get
		{
			return __pbn__LinkPara ?? "";
		}
		set
		{
			__pbn__LinkPara = value;
		}
	}

	[ProtoMember(7, Name = "link_para_str")]
	[DefaultValue("")]
	public string LinkParaStr
	{
		get
		{
			return __pbn__LinkParaStr ?? "";
		}
		set
		{
			__pbn__LinkParaStr = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePreGachaId()
	{
		return __pbn__PreGachaId.HasValue;
	}

	public void ResetPreGachaId()
	{
		__pbn__PreGachaId = null;
	}

	public bool ShouldSerializeWebLink()
	{
		return __pbn__WebLink != null;
	}

	public void ResetWebLink()
	{
		__pbn__WebLink = null;
	}

	public bool ShouldSerializeLinkType()
	{
		return __pbn__LinkType.HasValue;
	}

	public void ResetLinkType()
	{
		__pbn__LinkType = null;
	}

	public bool ShouldSerializeLinkPara()
	{
		return __pbn__LinkPara != null;
	}

	public void ResetLinkPara()
	{
		__pbn__LinkPara = null;
	}

	public bool ShouldSerializeLinkParaStr()
	{
		return __pbn__LinkParaStr != null;
	}

	public void ResetLinkParaStr()
	{
		__pbn__LinkParaStr = null;
	}
}
