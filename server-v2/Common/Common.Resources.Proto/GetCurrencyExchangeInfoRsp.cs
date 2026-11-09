using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetCurrencyExchangeInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1481
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PurpleJadeExchangedNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(3, Name = "purple_jade_exchanged_num")]
	public uint PurpleJadeExchangedNum
	{
		get
		{
			return __pbn__PurpleJadeExchangedNum.GetValueOrDefault();
		}
		set
		{
			__pbn__PurpleJadeExchangedNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePurpleJadeExchangedNum()
	{
		return __pbn__PurpleJadeExchangedNum.HasValue;
	}

	public void ResetPurpleJadeExchangedNum()
	{
		__pbn__PurpleJadeExchangedNum = null;
	}
}
