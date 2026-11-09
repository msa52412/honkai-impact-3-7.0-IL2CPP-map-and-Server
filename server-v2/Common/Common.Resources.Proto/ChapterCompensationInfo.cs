using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterCompensationInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CompensationId;

	private bool? __pbn__IsTakenCompensation;

	private uint? __pbn__ChapterId;

	[ProtoMember(1, Name = "compensation_id")]
	public uint CompensationId
	{
		get
		{
			return __pbn__CompensationId.GetValueOrDefault();
		}
		set
		{
			__pbn__CompensationId = value;
		}
	}

	[ProtoMember(2, Name = "stage_compensation_list")]
	public List<ChapterStageCompensation> StageCompensationLists { get; } = new List<ChapterStageCompensation>();


	[ProtoMember(3, Name = "act_compensation_list")]
	public List<ChapterActCompensation> ActCompensationLists { get; } = new List<ChapterActCompensation>();


	[ProtoMember(4, Name = "is_taken_compensation")]
	public bool IsTakenCompensation
	{
		get
		{
			return __pbn__IsTakenCompensation.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakenCompensation = value;
		}
	}

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

	public bool ShouldSerializeCompensationId()
	{
		return __pbn__CompensationId.HasValue;
	}

	public void ResetCompensationId()
	{
		__pbn__CompensationId = null;
	}

	public bool ShouldSerializeIsTakenCompensation()
	{
		return __pbn__IsTakenCompensation.HasValue;
	}

	public void ResetIsTakenCompensation()
	{
		__pbn__IsTakenCompensation = null;
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
