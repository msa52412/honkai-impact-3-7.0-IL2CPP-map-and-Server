using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarChallengeHistory : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private bool? __pbn__IsChallengeSucc;

	private uint? __pbn__MaxPunishLevel;

	private uint? __pbn__MaxChallengeScore;

	[ProtoMember(1, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(2, Name = "is_challenge_succ")]
	public bool IsChallengeSucc
	{
		get
		{
			return __pbn__IsChallengeSucc.GetValueOrDefault();
		}
		set
		{
			__pbn__IsChallengeSucc = value;
		}
	}

	[ProtoMember(3, Name = "max_punish_level")]
	public uint MaxPunishLevel
	{
		get
		{
			return __pbn__MaxPunishLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxPunishLevel = value;
		}
	}

	[ProtoMember(4, Name = "pass_step_level_list")]
	public uint[] PassStepLevelLists { get; set; }

	[ProtoMember(5, Name = "max_challenge_score")]
	public uint MaxChallengeScore
	{
		get
		{
			return __pbn__MaxChallengeScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxChallengeScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeIsChallengeSucc()
	{
		return __pbn__IsChallengeSucc.HasValue;
	}

	public void ResetIsChallengeSucc()
	{
		__pbn__IsChallengeSucc = null;
	}

	public bool ShouldSerializeMaxPunishLevel()
	{
		return __pbn__MaxPunishLevel.HasValue;
	}

	public void ResetMaxPunishLevel()
	{
		__pbn__MaxPunishLevel = null;
	}

	public bool ShouldSerializeMaxChallengeScore()
	{
		return __pbn__MaxChallengeScore.HasValue;
	}

	public void ResetMaxChallengeScore()
	{
		__pbn__MaxChallengeScore = null;
	}
}
