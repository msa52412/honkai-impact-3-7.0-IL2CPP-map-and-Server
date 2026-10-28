using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CardSuiteEpicActiveTime : IExtensible
{
	private IExtension __pbn__extensionData;

	private int? __pbn__Id;

	private uint? __pbn__TimeStamp;

	[ProtoMember(1, Name = "id")]
	public int Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "time_stamp")]
	public uint TimeStamp
	{
		get
		{
			return __pbn__TimeStamp.GetValueOrDefault();
		}
		set
		{
			__pbn__TimeStamp = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeTimeStamp()
	{
		return __pbn__TimeStamp.HasValue;
	}

	public void ResetTimeStamp()
	{
		__pbn__TimeStamp = null;
	}
}
