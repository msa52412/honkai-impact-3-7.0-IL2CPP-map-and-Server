using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChooseOpenworldQuestRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4479
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "HAS_CHOOSE")]
		HasChoose,
		[ProtoEnum(Name = "STAMINA_LACK")]
		StaminaLack,
		[ProtoEnum(Name = "MAP_NOT_FOUND")]
		MapNotFound,
		[ProtoEnum(Name = "QUEST_NOT_FOUND")]
		QuestNotFound,
		[ProtoEnum(Name = "ACTIVITY_QUEST_TIMES_LIMIT")]
		ActivityQuestTimesLimit
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
