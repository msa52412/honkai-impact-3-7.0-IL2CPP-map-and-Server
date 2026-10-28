using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EndlessSwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EndlessType;

	private uint? __pbn__PlayerGroup;

	[ProtoMember(1, Name = "endless_type")]
	public uint EndlessType
	{
		get
		{
			return __pbn__EndlessType.GetValueOrDefault();
		}
		set
		{
			__pbn__EndlessType = value;
		}
	}

	[ProtoMember(2, Name = "player_group")]
	public uint PlayerGroup
	{
		get
		{
			return __pbn__PlayerGroup.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerGroup = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEndlessType()
	{
		return __pbn__EndlessType.HasValue;
	}

	public void ResetEndlessType()
	{
		__pbn__EndlessType = null;
	}

	public bool ShouldSerializePlayerGroup()
	{
		return __pbn__PlayerGroup.HasValue;
	}

	public void ResetPlayerGroup()
	{
		__pbn__PlayerGroup = null;
	}
}
