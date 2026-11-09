using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeActivityChallengeRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 460
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__ChallengeNumIndex;

	[ProtoMember(1, Name = "activity_id")]
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

	[ProtoMember(2, Name = "challenge_num_index")]
	public uint ChallengeNumIndex
	{
		get
		{
			return __pbn__ChallengeNumIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__ChallengeNumIndex = value;
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

	public bool ShouldSerializeChallengeNumIndex()
	{
		return __pbn__ChallengeNumIndex.HasValue;
	}

	public void ResetChallengeNumIndex()
	{
		__pbn__ChallengeNumIndex = null;
	}
}
