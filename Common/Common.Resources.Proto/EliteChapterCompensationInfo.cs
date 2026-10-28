using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EliteChapterCompensationInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

	private bool? __pbn__HasTakenCompensation;

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

	[ProtoMember(2, Name = "stage_compensation_list")]
	public List<EliteChapterStageCompensation> StageCompensationLists { get; } = new List<EliteChapterStageCompensation>();


	[ProtoMember(3, Name = "act_compensation_list")]
	public List<EliteChapterActCompensation> ActCompensationLists { get; } = new List<EliteChapterActCompensation>();


	[ProtoMember(4, Name = "has_taken_compensation")]
	public bool HasTakenCompensation
	{
		get
		{
			return __pbn__HasTakenCompensation.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakenCompensation = value;
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

	public bool ShouldSerializeHasTakenCompensation()
	{
		return __pbn__HasTakenCompensation.HasValue;
	}

	public void ResetHasTakenCompensation()
	{
		__pbn__HasTakenCompensation = null;
	}
}
