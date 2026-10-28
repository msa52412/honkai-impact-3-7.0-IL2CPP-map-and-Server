using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GratuityStageMeta : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__StageGroup;

	private uint? __pbn__ExpireTime;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "stage_group")]
	public uint StageGroup
	{
		get
		{
			return __pbn__StageGroup.GetValueOrDefault();
		}
		set
		{
			__pbn__StageGroup = value;
		}
	}

	[ProtoMember(3, Name = "expire_time")]
	public uint ExpireTime
	{
		get
		{
			return __pbn__ExpireTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpireTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeStageGroup()
	{
		return __pbn__StageGroup.HasValue;
	}

	public void ResetStageGroup()
	{
		__pbn__StageGroup = null;
	}

	public bool ShouldSerializeExpireTime()
	{
		return __pbn__ExpireTime.HasValue;
	}

	public void ResetExpireTime()
	{
		__pbn__ExpireTime = null;
	}
}
