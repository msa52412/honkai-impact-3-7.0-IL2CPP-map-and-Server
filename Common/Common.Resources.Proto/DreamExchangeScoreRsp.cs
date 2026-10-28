using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DreamExchangeScoreRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5757
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVITY_NOT_EXIST")]
		ActivityNotExist,
		[ProtoEnum(Name = "DREAM_NOT_EXIST")]
		DreamNotExist,
		[ProtoEnum(Name = "TIMES_NOT_ENOUGH")]
		TimesNotEnough,
		[ProtoEnum(Name = "HCOIN_NOT_ENOUGH")]
		HcoinNotEnough,
		[ProtoEnum(Name = "SCORE_FULL")]
		ScoreFull
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
