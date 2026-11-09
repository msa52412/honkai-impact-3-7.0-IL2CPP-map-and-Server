using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GachaLog : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Time;

	[ProtoMember(1, Name = "time")]
	public uint Time
	{
		get
		{
			return __pbn__Time.GetValueOrDefault();
		}
		set
		{
			__pbn__Time = value;
		}
	}

	[ProtoMember(2, Name = "item")]
	public GachaItem Item { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTime()
	{
		return __pbn__Time.HasValue;
	}

	public void ResetTime()
	{
		__pbn__Time = null;
	}
}
