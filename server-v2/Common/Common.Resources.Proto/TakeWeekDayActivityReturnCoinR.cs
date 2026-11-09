using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeWeekDayActivityReturnCoinReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1034
	}

	private IExtension __pbn__extensionData;

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
[ProtoContract]
public class TakeWeekDayActivityReturnCoinRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1035
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_RETURN")]
		NoReturn,
		[ProtoEnum(Name = "COIN_LIMIT")]
		CoinLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Num;

	private bool? __pbn__IsCoinFullAfterTaken;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "num")]
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

	[ProtoMember(3, Name = "is_coin_full_after_taken")]
	public bool IsCoinFullAfterTaken
	{
		get
		{
			return __pbn__IsCoinFullAfterTaken.GetValueOrDefault();
		}
		set
		{
			__pbn__IsCoinFullAfterTaken = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNum()
	{
		return __pbn__Num.HasValue;
	}

	public void ResetNum()
	{
		__pbn__Num = null;
	}

	public bool ShouldSerializeIsCoinFullAfterTaken()
	{
		return __pbn__IsCoinFullAfterTaken.HasValue;
	}

	public void ResetIsCoinFullAfterTaken()
	{
		__pbn__IsCoinFullAfterTaken = null;
	}
}
