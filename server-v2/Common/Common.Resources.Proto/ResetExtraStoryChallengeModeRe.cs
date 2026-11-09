using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ResetExtraStoryChallengeModeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 504
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

	private uint? __pbn__Difficulty;

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

	public bool ShouldSerializeDifficulty()
	{
		return __pbn__Difficulty.HasValue;
	}

	public void ResetDifficulty()
	{
		__pbn__Difficulty = null;
	}
}
