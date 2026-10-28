using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarChapter : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

	private bool? __pbn__IsLocked;

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

	[ProtoMember(2, Name = "is_locked")]
	public bool IsLocked
	{
		get
		{
			return __pbn__IsLocked.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLocked = value;
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

	public bool ShouldSerializeIsLocked()
	{
		return __pbn__IsLocked.HasValue;
	}

	public void ResetIsLocked()
	{
		__pbn__IsLocked = null;
	}
}
