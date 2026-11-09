using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleBuff : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BuffId;

	private QAvatarBuffType? __pbn__BuffType;

	private ulong? __pbn__ExpireTimeMsU64;

	private uint? __pbn__Num;

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

	[ProtoMember(2, Name = "buff_type")]
	[DefaultValue(QAvatarBuffType.QavatarBuffNone)]
	public QAvatarBuffType BuffType
	{
		get
		{
			return __pbn__BuffType.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffType = value;
		}
	}

	[ProtoMember(4, Name = "expire_time_ms_u64")]
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

	[ProtoMember(5, Name = "num")]
	public uint Num
	{
		get
		{
			return __pbn__Num.GetValueOrDefault();
		}
		set
		{
			__pbn__Num = value;
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

	public bool ShouldSerializeBuffType()
	{
		return __pbn__BuffType.HasValue;
	}

	public void ResetBuffType()
	{
		__pbn__BuffType = null;
	}

	public bool ShouldSerializeExpireTimeMsU64()
	{
		return __pbn__ExpireTimeMsU64.HasValue;
	}

	public void ResetExpireTimeMsU64()
	{
		__pbn__ExpireTimeMsU64 = null;
	}

	public bool ShouldSerializeNum()
	{
		return __pbn__Num.HasValue;
	}

	public void ResetNum()
	{
		__pbn__Num = null;
	}
}
