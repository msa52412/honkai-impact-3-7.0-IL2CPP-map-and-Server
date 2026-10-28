using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterBwWorld : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

	private uint? __pbn__RewardLineTakenProgress;

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

	[ProtoMember(2, Name = "equip_rune_unique_id_list")]
	public uint[] EquipRuneUniqueIdLists { get; set; }

	[ProtoMember(3, Name = "rune_list")]
	public List<ChapterBwWorldRune> RuneLists { get; } = new List<ChapterBwWorldRune>();


	[ProtoMember(4, Name = "reward_line_taken_progress")]
	public uint RewardLineTakenProgress
	{
		get
		{
			return __pbn__RewardLineTakenProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardLineTakenProgress = value;
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

	public bool ShouldSerializeRewardLineTakenProgress()
	{
		return __pbn__RewardLineTakenProgress.HasValue;
	}

	public void ResetRewardLineTakenProgress()
	{
		__pbn__RewardLineTakenProgress = null;
	}
}
