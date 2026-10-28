using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PromoteStigmataRuneRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 333
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "STIGMATA_NOT_EXIST")]
		StigmataNotExist,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "STIGMATA_NOT_IDENTIFY")]
		StigmataNotIdentify,
		[ProtoEnum(Name = "CAN_NOT_PROMOTE")]
		CanNotPromote,
		[ProtoEnum(Name = "TIMES_LIMIT")]
		TimesLimit,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "TOO_MANY_WAIT_RUNE")]
		TooManyWaitRune
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(5, Name = "wait_select_stgimata_list")]
	public uint[] WaitSelectStgimataLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
