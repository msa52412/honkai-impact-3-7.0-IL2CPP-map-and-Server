using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldBuff : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BuffId;

	private ulong? __pbn__ExpireTimeMsU64;

	[ProtoMember(1, Name = "buff_id")]
	public uint BuffId
	{
		get
		{
			return __pbn__BuffId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffId = value;
		}
	}

	[ProtoMember(2, Name = "expire_time_ms_u64")]
	public ulong ExpireTimeMsU64
	{
		get
		{
			return __pbn__ExpireTimeMsU64.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpireTimeMsU64 = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBuffId()
	{
		return __pbn__BuffId.HasValue;
	}

	public void ResetBuffId()
	{
		__pbn__BuffId = null;
	}

	public bool ShouldSerializeExpireTimeMsU64()
	{
		return __pbn__ExpireTimeMsU64.HasValue;
	}

	public void ResetExpireTimeMsU64()
	{
		__pbn__ExpireTimeMsU64 = null;
	}
}
