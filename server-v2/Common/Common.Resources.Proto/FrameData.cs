using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FrameData : IExtensible
{
	[ProtoContract]
	public enum EvaluationType
	{
		Evaluate,
		Playback
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__ExpireTime;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "expire_time")]
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

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
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
