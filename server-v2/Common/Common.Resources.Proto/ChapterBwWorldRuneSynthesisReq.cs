using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterBwWorldRuneSynthesisReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7656
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

	private uint? __pbn__MainRuneUniqueId;

	private uint? __pbn__AssistRuneUniqueId;

	private uint? __pbn__MainRuneAffixIndex;

	private uint? __pbn__AssistRuneAffixIndex;

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

	[ProtoMember(2, Name = "main_rune_unique_id")]
	public uint MainRuneUniqueId
	{
		get
		{
			return __pbn__MainRuneUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__MainRuneUniqueId = value;
		}
	}

	[ProtoMember(3, Name = "assist_rune_unique_id")]
	public uint AssistRuneUniqueId
	{
		get
		{
			return __pbn__AssistRuneUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__AssistRuneUniqueId = value;
		}
	}

	[ProtoMember(4, Name = "main_rune_affix_index")]
	public uint MainRuneAffixIndex
	{
		get
		{
			return __pbn__MainRuneAffixIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__MainRuneAffixIndex = value;
		}
	}

	[ProtoMember(5, Name = "assist_rune_affix_index")]
	public uint AssistRuneAffixIndex
	{
		get
		{
			return __pbn__AssistRuneAffixIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__AssistRuneAffixIndex = value;
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

	public bool ShouldSerializeMainRuneUniqueId()
	{
		return __pbn__MainRuneUniqueId.HasValue;
	}

	public void ResetMainRuneUniqueId()
	{
		__pbn__MainRuneUniqueId = null;
	}

	public bool ShouldSerializeAssistRuneUniqueId()
	{
		return __pbn__AssistRuneUniqueId.HasValue;
	}

	public void ResetAssistRuneUniqueId()
	{
		__pbn__AssistRuneUniqueId = null;
	}

	public bool ShouldSerializeMainRuneAffixIndex()
	{
		return __pbn__MainRuneAffixIndex.HasValue;
	}

	public void ResetMainRuneAffixIndex()
	{
		__pbn__MainRuneAffixIndex = null;
	}

	public bool ShouldSerializeAssistRuneAffixIndex()
	{
		return __pbn__AssistRuneAffixIndex.HasValue;
	}

	public void ResetAssistRuneAffixIndex()
	{
		__pbn__AssistRuneAffixIndex = null;
	}
}
