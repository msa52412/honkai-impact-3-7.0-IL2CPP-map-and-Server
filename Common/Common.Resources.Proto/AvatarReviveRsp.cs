using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarReviveRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 107
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "HCOIN_LACK")]
		HcoinLack,
		[ProtoEnum(Name = "TIMES_LACK")]
		TimesLack,
		[ProtoEnum(Name = "NOT_IN_STAGE")]
		NotInStage,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ReviveTimes;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "revive_times")]
	public uint ReviveTimes
	{
		get
		{
			return __pbn__ReviveTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ReviveTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReviveTimes()
	{
		return __pbn__ReviveTimes.HasValue;
	}

	public void ResetReviveTimes()
	{
		__pbn__ReviveTimes = null;
	}
}
