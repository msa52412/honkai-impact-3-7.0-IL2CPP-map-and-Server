using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetOpenworldQuestActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4547
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__TotalScore;

	private uint? __pbn__CycleQuestTakenNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "total_score")]
	public uint TotalScore
	{
		get
		{
			return __pbn__TotalScore.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalScore = value;
		}
	}

	[ProtoMember(4, Name = "cycle_quest_taken_num")]
	public uint CycleQuestTakenNum
	{
		get
		{
			return __pbn__CycleQuestTakenNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CycleQuestTakenNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeTotalScore()
	{
		return __pbn__TotalScore.HasValue;
	}

	public void ResetTotalScore()
	{
		__pbn__TotalScore = null;
	}

	public bool ShouldSerializeCycleQuestTakenNum()
	{
		return __pbn__CycleQuestTakenNum.HasValue;
	}

	public void ResetCycleQuestTakenNum()
	{
		__pbn__CycleQuestTakenNum = null;
	}
}
