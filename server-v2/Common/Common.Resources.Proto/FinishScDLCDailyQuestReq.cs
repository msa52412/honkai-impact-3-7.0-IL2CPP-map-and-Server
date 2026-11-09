using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishScDLCDailyQuestReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6322
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__QuestId;

	private uint? __pbn__CostTime;

	[ProtoMember(1, Name = "quest_id")]
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

	[ProtoMember(2, Name = "cost_time")]
	public uint CostTime
	{
		get
		{
			return __pbn__CostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CostTime = value;
		}
	}

	[ProtoMember(3, Name = "finish_challenge_list")]
	public uint[] FinishChallengeLists { get; set; }

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

	public bool ShouldSerializeCostTime()
	{
		return __pbn__CostTime.HasValue;
	}

	public void ResetCostTime()
	{
		__pbn__CostTime = null;
	}
}
