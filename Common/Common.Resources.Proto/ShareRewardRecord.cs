using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ShareRewardRecord : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ShareId;

	private uint? __pbn__LastTakeTime;

	[ProtoMember(1, Name = "share_id")]
	public uint ShareId
	{
		get
		{
			return __pbn__ShareId.GetValueOrDefault();
		}
		set
		{
			__pbn__ShareId = value;
		}
	}

	[ProtoMember(2, Name = "last_take_time")]
	public uint LastTakeTime
	{
		get
		{
			return __pbn__LastTakeTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastTakeTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeShareId()
	{
		return __pbn__ShareId.HasValue;
	}

	public void ResetShareId()
	{
		__pbn__ShareId = null;
	}

	public bool ShouldSerializeLastTakeTime()
	{
		return __pbn__LastTakeTime.HasValue;
	}

	public void ResetLastTakeTime()
	{
		__pbn__LastTakeTime = null;
	}
}
