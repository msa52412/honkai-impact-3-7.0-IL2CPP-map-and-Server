using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WikiDetailData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__WikiDetailType;

	private uint? __pbn__WikiDetailNumGet;

	private uint? __pbn__WikiDetailScore;

	private uint? __pbn__WikiDetailNumTotal;

	[ProtoMember(1, Name = "wiki_detail_type")]
	public uint WikiDetailType
	{
		get
		{
			return __pbn__WikiDetailType.GetValueOrDefault();
		}
		set
		{
			__pbn__WikiDetailType = value;
		}
	}

	[ProtoMember(2, Name = "wiki_detail_num_get")]
	public uint WikiDetailNumGet
	{
		get
		{
			return __pbn__WikiDetailNumGet.GetValueOrDefault();
		}
		set
		{
			__pbn__WikiDetailNumGet = value;
		}
	}

	[ProtoMember(3, Name = "wiki_detail_score")]
	public uint WikiDetailScore
	{
		get
		{
			return __pbn__WikiDetailScore.GetValueOrDefault();
		}
		set
		{
			__pbn__WikiDetailScore = value;
		}
	}

	[ProtoMember(4, Name = "wiki_detail_num_total")]
	public uint WikiDetailNumTotal
	{
		get
		{
			return __pbn__WikiDetailNumTotal.GetValueOrDefault();
		}
		set
		{
			__pbn__WikiDetailNumTotal = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeWikiDetailType()
	{
		return __pbn__WikiDetailType.HasValue;
	}

	public void ResetWikiDetailType()
	{
		__pbn__WikiDetailType = null;
	}

	public bool ShouldSerializeWikiDetailNumGet()
	{
		return __pbn__WikiDetailNumGet.HasValue;
	}

	public void ResetWikiDetailNumGet()
	{
		__pbn__WikiDetailNumGet = null;
	}

	public bool ShouldSerializeWikiDetailScore()
	{
		return __pbn__WikiDetailScore.HasValue;
	}

	public void ResetWikiDetailScore()
	{
		__pbn__WikiDetailScore = null;
	}

	public bool ShouldSerializeWikiDetailNumTotal()
	{
		return __pbn__WikiDetailNumTotal.HasValue;
	}

	public void ResetWikiDetailNumTotal()
	{
		__pbn__WikiDetailNumTotal = null;
	}
}
