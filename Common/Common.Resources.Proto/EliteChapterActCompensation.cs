using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EliteChapterActCompensation : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActId;

	private uint? __pbn__Difficulty;

	[ProtoMember(1, Name = "act_id")]
	public uint ActId
	{
		get
		{
			return __pbn__ActId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActId = value;
		}
	}

	[ProtoMember(2, Name = "difficulty")]
	public uint Difficulty
	{
		get
		{
			return __pbn__Difficulty.GetValueOrDefault();
		}
		set
		{
			__pbn__Difficulty = value;
		}
	}

	[ProtoMember(3, Name = "challenge_reward_id_list")]
	public uint[] ChallengeRewardIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActId()
	{
		return __pbn__ActId.HasValue;
	}

	public void ResetActId()
	{
		__pbn__ActId = null;
	}

	public bool ShouldSerializeDifficulty()
	{
		return __pbn__Difficulty.HasValue;
	}

	public void ResetDifficulty()
	{
		__pbn__Difficulty = null;
	}
}
