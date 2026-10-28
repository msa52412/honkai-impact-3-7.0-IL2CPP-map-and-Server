using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetExtraStoryChallengeModeDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 502
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

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
}
[ProtoContract]
public class GetExtraStoryChallengeModeDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 503
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 0,
		[ProtoEnum(Name = "FAIL")]
		Fail = 1,
		[ProtoEnum(Name = "EXTRA_STORY_NOT_OPEN")]
		ExtraStoryNotOpen = 3
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChooseDifficulty;

	private bool? __pbn__IsCanReset;

	private uint? __pbn__ChapterId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "choose_difficulty")]
	public uint ChooseDifficulty
	{
		get
		{
			return __pbn__ChooseDifficulty.GetValueOrDefault();
		}
		set
		{
			__pbn__ChooseDifficulty = value;
		}
	}

	[ProtoMember(3, Name = "is_can_reset")]
	public bool IsCanReset
	{
		get
		{
			return __pbn__IsCanReset.GetValueOrDefault();
		}
		set
		{
			__pbn__IsCanReset = value;
		}
	}

	[ProtoMember(4, Name = "challenge_area_list")]
	public List<ChallengeModeArea> ChallengeAreaLists { get; } = new List<ChallengeModeArea>();


	[ProtoMember(5, Name = "chapter_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChooseDifficulty()
	{
		return __pbn__ChooseDifficulty.HasValue;
	}

	public void ResetChooseDifficulty()
	{
		__pbn__ChooseDifficulty = null;
	}

	public bool ShouldSerializeIsCanReset()
	{
		return __pbn__IsCanReset.HasValue;
	}

	public void ResetIsCanReset()
	{
		__pbn__IsCanReset = null;
	}

	public bool ShouldSerializeChapterId()
	{
		return __pbn__ChapterId.HasValue;
	}

	public void ResetChapterId()
	{
		__pbn__ChapterId = null;
	}
}
