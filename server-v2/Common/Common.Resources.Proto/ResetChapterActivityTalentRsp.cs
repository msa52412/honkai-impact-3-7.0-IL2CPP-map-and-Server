using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ResetChapterActivityTalentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4389
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVITY_NOT_OPEN")]
		ActivityNotOpen,
		[ProtoEnum(Name = "NOT_ALLOWED")]
		NotAllowed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

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
