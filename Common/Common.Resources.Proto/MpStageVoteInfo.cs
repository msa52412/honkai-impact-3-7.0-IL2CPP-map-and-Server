using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpStageVoteInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__Option;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "option")]
	public uint Option
	{
		get
		{
			return __pbn__Option.GetValueOrDefault();
		}
		set
		{
			__pbn__Option = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeOption()
	{
		return __pbn__Option.HasValue;
	}

	public void ResetOption()
	{
		__pbn__Option = null;
	}
}
