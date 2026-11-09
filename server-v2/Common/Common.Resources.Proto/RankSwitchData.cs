using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RankSwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__RankKey;

	[ProtoMember(1, Name = "rank_key")]
	[DefaultValue("")]
	public string RankKey
	{
		get
		{
			return __pbn__RankKey ?? "";
		}
		set
		{
			__pbn__RankKey = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRankKey()
	{
		return __pbn__RankKey != null;
	}

	public void ResetRankKey()
	{
		__pbn__RankKey = null;
	}
}
