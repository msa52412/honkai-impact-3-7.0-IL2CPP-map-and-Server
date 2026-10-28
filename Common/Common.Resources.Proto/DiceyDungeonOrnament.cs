using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DiceyDungeonOrnament : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__OrnamentId;

	private uint? __pbn__Level;

	[ProtoMember(1, Name = "ornament_id")]
	public uint OrnamentId
	{
		get
		{
			return __pbn__OrnamentId.GetValueOrDefault();
		}
		set
		{
			__pbn__OrnamentId = value;
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

	public bool ShouldSerializeOrnamentId()
	{
		return __pbn__OrnamentId.HasValue;
	}

	public void ResetOrnamentId()
	{
		__pbn__OrnamentId = null;
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
