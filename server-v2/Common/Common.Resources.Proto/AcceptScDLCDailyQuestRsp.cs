using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AcceptScDLCDailyQuestRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6321
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "QUEST_ERROR")]
		QuestError,
		[ProtoEnum(Name = "STAMINA_LACK")]
		StaminaLack,
		[ProtoEnum(Name = "ACCEPT_TIMES_LACK")]
		AcceptTimesLack,
		[ProtoEnum(Name = "SLOT_LACK")]
		SlotLack
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "quest")]
	public OpenworldQuest Quest { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
