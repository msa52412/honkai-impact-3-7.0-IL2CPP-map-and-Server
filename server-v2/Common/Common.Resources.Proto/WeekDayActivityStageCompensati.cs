using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WeekDayActivityStageCompensation : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private bool? __pbn__IsFirstWin;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "is_first_win")]
	public bool IsFirstWin
	{
		get
		{
			return __pbn__IsFirstWin.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFirstWin = value;
		}
	}

	[ProtoMember(3, Name = "challenge_reward_list")]
	public uint[] ChallengeRewardLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeIsFirstWin()
	{
		return __pbn__IsFirstWin.HasValue;
	}

	public void ResetIsFirstWin()
	{
		__pbn__IsFirstWin = null;
	}
}
