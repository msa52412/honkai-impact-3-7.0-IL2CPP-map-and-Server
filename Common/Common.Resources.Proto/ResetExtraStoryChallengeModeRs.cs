using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ResetExtraStoryChallengeModeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 505
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "TIME_LIMIT")]
		TimeLimit,
		[ProtoEnum(Name = "EXTRA_STORY_NOT_OPEN")]
		ExtraStoryNotOpen,
		[ProtoEnum(Name = "RESET_TIMES_LACK")]
		ResetTimesLack,
		[ProtoEnum(Name = "DIFFICULTY_NOT_OPEN")]
		DifficultyNotOpen,
		[ProtoEnum(Name = "LEVEL_LIMIT")]
		LevelLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

	private uint? __pbn__Difficulty;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chapter_id")]
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

	[ProtoMember(3, Name = "difficulty")]
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
