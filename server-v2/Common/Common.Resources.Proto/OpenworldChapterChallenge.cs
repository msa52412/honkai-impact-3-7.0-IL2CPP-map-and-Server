using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterChallenge : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TodayChallengeTimes;

	private uint? __pbn__LastChallengeTime;

	[ProtoMember(1, Name = "today_challenge_times")]
	public uint TodayChallengeTimes
	{
		get
		{
			return __pbn__TodayChallengeTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayChallengeTimes = value;
		}
	}

	[ProtoMember(2, Name = "last_challenge_time")]
	public uint LastChallengeTime
	{
		get
		{
			return __pbn__LastChallengeTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastChallengeTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTodayChallengeTimes()
	{
		return __pbn__TodayChallengeTimes.HasValue;
	}

	public void ResetTodayChallengeTimes()
	{
		__pbn__TodayChallengeTimes = null;
	}

	public bool ShouldSerializeLastChallengeTime()
	{
		return __pbn__LastChallengeTime.HasValue;
	}

	public void ResetLastChallengeTime()
	{
		__pbn__LastChallengeTime = null;
	}
}
