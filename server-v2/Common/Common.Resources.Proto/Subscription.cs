using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Subscription : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SubsId;

	private uint? __pbn__ExpiresTime;

	[ProtoMember(1, Name = "subs_id")]
	public uint SubsId
	{
		get
		{
			return __pbn__SubsId.GetValueOrDefault();
		}
		set
		{
			__pbn__SubsId = value;
		}
	}

	[ProtoMember(2, Name = "expires_time")]
	public uint ExpiresTime
	{
		get
		{
			return __pbn__ExpiresTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpiresTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSubsId()
	{
		return __pbn__SubsId.HasValue;
	}

	public void ResetSubsId()
	{
		__pbn__SubsId = null;
	}

	public bool ShouldSerializeExpiresTime()
	{
		return __pbn__ExpiresTime.HasValue;
	}

	public void ResetExpiresTime()
	{
		__pbn__ExpiresTime = null;
	}
}
