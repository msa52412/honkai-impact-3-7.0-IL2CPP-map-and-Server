using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatchThreeGem : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__GemId;

	private uint? __pbn__Level;

	[ProtoMember(1, Name = "gem_id")]
	public uint GemId
	{
		get
		{
			return __pbn__GemId.GetValueOrDefault();
		}
		set
		{
			__pbn__GemId = value;
		}
	}

	[ProtoMember(2, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGemId()
	{
		return __pbn__GemId.HasValue;
	}

	public void ResetGemId()
	{
		__pbn__GemId = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}
}
