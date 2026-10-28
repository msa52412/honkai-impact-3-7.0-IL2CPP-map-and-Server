using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlayerOnlineStatusInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__OnlineStatus;

	private uint? __pbn__LastLoginTime;

	private uint? __pbn__LastLogoutTime;

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

	[ProtoMember(2, Name = "online_status")]
	public uint OnlineStatus
	{
		get
		{
			return __pbn__OnlineStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__OnlineStatus = value;
		}
	}

	[ProtoMember(3, Name = "last_login_time")]
	public uint LastLoginTime
	{
		get
		{
			return __pbn__LastLoginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastLoginTime = value;
		}
	}

	[ProtoMember(4, Name = "last_logout_time")]
	public uint LastLogoutTime
	{
		get
		{
			return __pbn__LastLogoutTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastLogoutTime = value;
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

	public bool ShouldSerializeOnlineStatus()
	{
		return __pbn__OnlineStatus.HasValue;
	}

	public void ResetOnlineStatus()
	{
		__pbn__OnlineStatus = null;
	}

	public bool ShouldSerializeLastLoginTime()
	{
		return __pbn__LastLoginTime.HasValue;
	}

	public void ResetLastLoginTime()
	{
		__pbn__LastLoginTime = null;
	}

	public bool ShouldSerializeLastLogoutTime()
	{
		return __pbn__LastLogoutTime.HasValue;
	}

	public void ResetLastLogoutTime()
	{
		__pbn__LastLogoutTime = null;
	}
}
