using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarOverall : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__OverallId;

	private uint? __pbn__OverallVal;

	[ProtoMember(1, Name = "overall_id")]
	public uint OverallId
	{
		get
		{
			return __pbn__OverallId.GetValueOrDefault();
		}
		set
		{
			__pbn__OverallId = value;
		}
	}

	[ProtoMember(2, Name = "overall_val")]
	public uint OverallVal
	{
		get
		{
			return __pbn__OverallVal.GetValueOrDefault();
		}
		set
		{
			__pbn__OverallVal = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeOverallId()
	{
		return __pbn__OverallId.HasValue;
	}

	public void ResetOverallId()
	{
		__pbn__OverallId = null;
	}

	public bool ShouldSerializeOverallVal()
	{
		return __pbn__OverallVal.HasValue;
	}

	public void ResetOverallVal()
	{
		__pbn__OverallVal = null;
	}
}
