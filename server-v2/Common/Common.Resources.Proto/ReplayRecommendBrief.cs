using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReplayRecommendBrief : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Num;

	[ProtoMember(1, Name = "key")]
	public ReplayRecommendBriefKey Key { get; set; }

	[ProtoMember(2, Name = "num")]
	public uint Num
	{
		get
		{
			return __pbn__Num.GetValueOrDefault();
		}
		set
		{
			__pbn__Num = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNum()
	{
		return __pbn__Num.HasValue;
	}

	public void ResetNum()
	{
		__pbn__Num = null;
	}
}
