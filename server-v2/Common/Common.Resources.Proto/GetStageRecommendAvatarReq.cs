using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetStageRecommendAvatarReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1541
	}

	private IExtension __pbn__extensionData;

	private AvatarRecommendType? __pbn__Type;

	[ProtoMember(1, Name = "id_list")]
	public List<string> IdLists { get; } = new List<string>();


	[ProtoMember(2, Name = "type")]
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

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}
}
