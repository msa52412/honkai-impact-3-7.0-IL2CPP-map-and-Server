using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeChapterChallengeRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 967
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

	private uint? __pbn__ChallengeNumIndex;

	[ProtoMember(1, Name = "chapter_id")]
	public uint ChapterId
	{
		get
		{
			return __pbn__ChapterId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChapterId = value;
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

	public bool ShouldSerializeChapterId()
	{
		return __pbn__ChapterId.HasValue;
	}

	public void ResetChapterId()
	{
		__pbn__ChapterId = null;
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
