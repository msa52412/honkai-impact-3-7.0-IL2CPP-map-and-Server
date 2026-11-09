using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeScDLCDailyQuestRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6325
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "QUEST_NOT_EXIST")]
		QuestNotExist,
		[ProtoEnum(Name = "NOT_FINISH")]
		NotFinish
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__QuestId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "quest_id")]
	public uint QuestId
	{
		get
		{
			return __pbn__QuestId.GetValueOrDefault();
		}
		set
		{
			__pbn__QuestId = value;
		}
	}

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeQuestId()
	{
		return __pbn__QuestId.HasValue;
	}

	public void ResetQuestId()
	{
		__pbn__QuestId = null;
	}
}
