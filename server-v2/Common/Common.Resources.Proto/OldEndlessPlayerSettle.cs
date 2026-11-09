using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OldEndlessPlayerSettle : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndlessType;

	private uint? __pbn__GroupLevel;

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

	[ProtoMember(2, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(3, Name = "endless_type")]
	public uint EndlessType
	{
		get
		{
			return __pbn__EndlessType.GetValueOrDefault();
		}
		set
		{
			__pbn__EndlessType = value;
		}
	}

	[ProtoMember(4, Name = "group_level")]
	public uint GroupLevel
	{
		get
		{
			return __pbn__GroupLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupLevel = value;
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

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndlessType()
	{
		return __pbn__EndlessType.HasValue;
	}

	public void ResetEndlessType()
	{
		__pbn__EndlessType = null;
	}

	public bool ShouldSerializeGroupLevel()
	{
		return __pbn__GroupLevel.HasValue;
	}

	public void ResetGroupLevel()
	{
		__pbn__GroupLevel = null;
	}
}
