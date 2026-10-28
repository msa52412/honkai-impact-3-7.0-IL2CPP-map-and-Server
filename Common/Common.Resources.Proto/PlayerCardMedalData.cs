using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlayerCardMedalData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Index;

	private uint? __pbn__MedalId;

	private int? __pbn__ExtraParam;

	[ProtoMember(1, Name = "index")]
	public uint Index
	{
		get
		{
			return __pbn__Index.GetValueOrDefault();
		}
		set
		{
			__pbn__Index = value;
		}
	}

	[ProtoMember(2, Name = "medal_id")]
	public uint MedalId
	{
		get
		{
			return __pbn__MedalId.GetValueOrDefault();
		}
		set
		{
			__pbn__MedalId = value;
		}
	}

	[ProtoMember(3, Name = "extra_param")]
	public int ExtraParam
	{
		get
		{
			return __pbn__ExtraParam.GetValueOrDefault();
		}
		set
		{
			__pbn__ExtraParam = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIndex()
	{
		return __pbn__Index.HasValue;
	}

	public void ResetIndex()
	{
		__pbn__Index = null;
	}

	public bool ShouldSerializeMedalId()
	{
		return __pbn__MedalId.HasValue;
	}

	public void ResetMedalId()
	{
		__pbn__MedalId = null;
	}

	public bool ShouldSerializeExtraParam()
	{
		return __pbn__ExtraParam.HasValue;
	}

	public void ResetExtraParam()
	{
		__pbn__ExtraParam = null;
	}
}
