using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarBuff : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BuffId;

	private uint? __pbn__BuffLevel;

	[ProtoMember(1, Name = "buff_id")]
	public uint BuffId
	{
		get
		{
			return __pbn__BuffId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffId = value;
		}
	}

	[ProtoMember(2, Name = "buff_level")]
	public uint BuffLevel
	{
		get
		{
			return __pbn__BuffLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBuffId()
	{
		return __pbn__BuffId.HasValue;
	}

	public void ResetBuffId()
	{
		__pbn__BuffId = null;
	}

	public bool ShouldSerializeBuffLevel()
	{
		return __pbn__BuffLevel.HasValue;
	}

	public void ResetBuffLevel()
	{
		__pbn__BuffLevel = null;
	}
}
