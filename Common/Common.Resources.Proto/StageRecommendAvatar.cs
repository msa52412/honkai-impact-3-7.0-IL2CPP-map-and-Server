using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageRecommendAvatar : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__Id;

	private AvatarRecommendType? __pbn__Type;

	[ProtoMember(1, Name = "id")]
	[DefaultValue("")]
	public string Id
	{
		get
		{
			return __pbn__Id ?? "";
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "recommend_avatar_list")]
	public List<RecommendAvatar> RecommendAvatarLists { get; } = new List<RecommendAvatar>();


	[ProtoMember(3, Name = "type")]
	[DefaultValue(AvatarRecommendType.AvatarRecommendNone)]
	public AvatarRecommendType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id != null;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}
}
