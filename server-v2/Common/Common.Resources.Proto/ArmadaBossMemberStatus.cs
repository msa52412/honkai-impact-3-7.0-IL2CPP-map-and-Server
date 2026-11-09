using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaBossMemberStatus : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__ErrorStatus;

	private uint? __pbn__NextFightTime;

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

	[ProtoMember(2, Name = "error_status")]
	public uint ErrorStatus
	{
		get
		{
			return __pbn__ErrorStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__ErrorStatus = value;
		}
	}

	[ProtoMember(3, Name = "next_fight_time")]
	public uint NextFightTime
	{
		get
		{
			return __pbn__NextFightTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextFightTime = value;
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

	public bool ShouldSerializeErrorStatus()
	{
		return __pbn__ErrorStatus.HasValue;
	}

	public void ResetErrorStatus()
	{
		__pbn__ErrorStatus = null;
	}

	public bool ShouldSerializeNextFightTime()
	{
		return __pbn__NextFightTime.HasValue;
	}

	public void ResetNextFightTime()
	{
		__pbn__NextFightTime = null;
	}
}
