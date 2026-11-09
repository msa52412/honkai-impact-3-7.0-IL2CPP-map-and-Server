using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OptionalGachaItemData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SubGachaId;

	private string __pbn__ChooseImage;

	private bool? __pbn__IsRecommend;

	[ProtoMember(1, Name = "sub_gacha_id")]
	public uint SubGachaId
	{
		get
		{
			return __pbn__SubGachaId.GetValueOrDefault();
		}
		set
		{
			__pbn__SubGachaId = value;
		}
	}

	[ProtoMember(2, Name = "choose_image")]
	[DefaultValue("")]
	public string ChooseImage
	{
		get
		{
			return __pbn__ChooseImage ?? "";
		}
		set
		{
			__pbn__ChooseImage = value;
		}
	}

	[ProtoMember(3, Name = "is_recommend")]
	public bool IsRecommend
	{
		get
		{
			return __pbn__IsRecommend.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRecommend = value;
		}
	}

	[ProtoMember(4, Name = "common_data")]
	public GachaDisplayCommonData CommonData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSubGachaId()
	{
		return __pbn__SubGachaId.HasValue;
	}

	public void ResetSubGachaId()
	{
		__pbn__SubGachaId = null;
	}

	public bool ShouldSerializeChooseImage()
	{
		return __pbn__ChooseImage != null;
	}

	public void ResetChooseImage()
	{
		__pbn__ChooseImage = null;
	}

	public bool ShouldSerializeIsRecommend()
	{
		return __pbn__IsRecommend.HasValue;
	}

	public void ResetIsRecommend()
	{
		__pbn__IsRecommend = null;
	}
}
