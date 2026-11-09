using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WaveRushLevelUpBuffRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5966
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "CANNOT_UNLOCK_BY_LEVEL_UP")]
		CannotUnlockByLevelUp,
		[ProtoEnum(Name = "BUFF_ERROR")]
		BuffError,
		[ProtoEnum(Name = "NOT_MEET_CONDITION")]
		NotMeetCondition,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
