using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageChallengeData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ChallengeIndex;

	[ProtoMember(1, Name = "challenge_index")]
	public uint ChallengeIndex
	{
		get
		{
			return __pbn__ChallengeIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__ChallengeIndex = value;
		}
	}

	[ProtoMember(2, Name = "reward")]
	public RewardData Reward { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChallengeIndex()
	{
		return __pbn__ChallengeIndex.HasValue;
	}

	public void ResetChallengeIndex()
	{
		__pbn__ChallengeIndex = null;
	}
}
