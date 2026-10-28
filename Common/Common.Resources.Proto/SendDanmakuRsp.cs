using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SendDanmakuRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1544
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SENSITIVE_WORDS")]
		SensitiveWords,
		[ProtoEnum(Name = "CHAT_FORBIDEN")]
		ChatForbiden,
		[ProtoEnum(Name = "TOO_FREQUENT")]
		TooFrequent,
		[ProtoEnum(Name = "DANMAKU_CLOSED")]
		DanmakuClosed,
		[ProtoEnum(Name = "IN_BLACKLIST")]
		InBlacklist,
		[ProtoEnum(Name = "NUM_LIMIT")]
		NumLimit,
		[ProtoEnum(Name = "LENGTH_LIMIT")]
		LengthLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__DanmakuSlot;

	private uint? __pbn__BlacklistExpireTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "danmaku_slot")]
	public uint DanmakuSlot
	{
		get
		{
			return __pbn__DanmakuSlot.GetValueOrDefault();
		}
		set
		{
			__pbn__DanmakuSlot = value;
		}
	}

	[ProtoMember(3, Name = "content")]
	public DanmakuContent Content { get; set; }

	[ProtoMember(4, Name = "blacklist_expire_time")]
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

	public bool ShouldSerializeDanmakuSlot()
	{
		return __pbn__DanmakuSlot.HasValue;
	}

	public void ResetDanmakuSlot()
	{
		__pbn__DanmakuSlot = null;
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
