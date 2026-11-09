using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AbandonOpenworldQuestRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4485
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "IS_CLOSE")]
		IsClose,
		[ProtoEnum(Name = "REWARD_NOT_TAKE")]
		RewardNotTake,
		[ProtoEnum(Name = "NOT_FOUND")]
		NotFound,
		[ProtoEnum(Name = "NOT_DOING")]
		NotDoing,
		[ProtoEnum(Name = "HAS_ABANDAN")]
		HasAbandan,
		[ProtoEnum(Name = "MAP_NOT_FOUND")]
		MapNotFound,
		[ProtoEnum(Name = "CANNOT_ABANDON")]
		CannotAbandon
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
