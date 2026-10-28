using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DanmakuBlacklistNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1551
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BlacklistExpireTime;

	[ProtoMember(1, Name = "blacklist_expire_time")]
	public uint BlacklistExpireTime
	{
		get
		{
			return __pbn__BlacklistExpireTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BlacklistExpireTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBlacklistExpireTime()
	{
		return __pbn__BlacklistExpireTime.HasValue;
	}

	public void ResetBlacklistExpireTime()
	{
		__pbn__BlacklistExpireTime = null;
	}
}
