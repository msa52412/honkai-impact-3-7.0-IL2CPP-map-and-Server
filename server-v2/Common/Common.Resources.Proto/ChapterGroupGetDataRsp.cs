using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterGroupGetDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1661
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_EXIST")]
		NotExist
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsAll;

	private uint? __pbn__ChapterGroupId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chapter_group_list")]
	public List<ChapterGroup> ChapterGroupLists { get; } = new List<ChapterGroup>();


	[ProtoMember(3, Name = "is_all")]
	public bool IsAll
	{
		get
		{
			return __pbn__IsAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAll = value;
		}
	}

	[ProtoMember(4, Name = "chapter_group_id")]
	public uint ChapterGroupId
	{
		get
		{
			return __pbn__ChapterGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChapterGroupId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsAll()
	{
		return __pbn__IsAll.HasValue;
	}

	public void ResetIsAll()
	{
		__pbn__IsAll = null;
	}

	public bool ShouldSerializeChapterGroupId()
	{
		return __pbn__ChapterGroupId.HasValue;
	}

	public void ResetChapterGroupId()
	{
		__pbn__ChapterGroupId = null;
	}
}
