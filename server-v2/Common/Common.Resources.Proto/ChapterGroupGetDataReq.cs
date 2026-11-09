using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterGroupGetDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1660
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterGroupId;

	[ProtoMember(1, Name = "chapter_group_id")]
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

	public bool ShouldSerializeChapterGroupId()
	{
		return __pbn__ChapterGroupId.HasValue;
	}

	public void ResetChapterGroupId()
	{
		__pbn__ChapterGroupId = null;
	}
}
