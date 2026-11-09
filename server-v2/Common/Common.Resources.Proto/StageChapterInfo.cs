using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageChapterInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

	private ulong? __pbn__HasTakeChallenge;

	private uint? __pbn__EnterPlayerLevel;

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

	[ProtoMember(2, Name = "has_take_challenge")]
	public ulong HasTakeChallenge
	{
		get
		{
			return __pbn__HasTakeChallenge.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeChallenge = value;
		}
	}

	[ProtoMember(3, Name = "enter_player_level")]
	public uint EnterPlayerLevel
	{
		get
		{
			return __pbn__EnterPlayerLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__EnterPlayerLevel = value;
		}
	}

	[ProtoMember(4, Name = "boss_list")]
	public List<StageChapterBoss> BossLists { get; } = new List<StageChapterBoss>();


	[ProtoMember(5, Name = "exclusive_stage_list")]
	public List<StageChapterExclusive> ExclusiveStageLists { get; } = new List<StageChapterExclusive>();


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

	public bool ShouldSerializeHasTakeChallenge()
	{
		return __pbn__HasTakeChallenge.HasValue;
	}

	public void ResetHasTakeChallenge()
	{
		__pbn__HasTakeChallenge = null;
	}

	public bool ShouldSerializeEnterPlayerLevel()
	{
		return __pbn__EnterPlayerLevel.HasValue;
	}

	public void ResetEnterPlayerLevel()
	{
		__pbn__EnterPlayerLevel = null;
	}
}
