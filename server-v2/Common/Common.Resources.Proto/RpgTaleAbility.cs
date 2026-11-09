using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgTaleAbility : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AbilityId;

	private uint? __pbn__AbilityLevel;

	[ProtoMember(1, Name = "ability_id")]
	public uint AbilityId
	{
		get
		{
			return __pbn__AbilityId.GetValueOrDefault();
		}
		set
		{
			__pbn__AbilityId = value;
		}
	}

	[ProtoMember(2, Name = "ability_level")]
	public uint AbilityLevel
	{
		get
		{
			return __pbn__AbilityLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__AbilityLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAbilityId()
	{
		return __pbn__AbilityId.HasValue;
	}

	public void ResetAbilityId()
	{
		__pbn__AbilityId = null;
	}

	public bool ShouldSerializeAbilityLevel()
	{
		return __pbn__AbilityLevel.HasValue;
	}

	public void ResetAbilityLevel()
	{
		__pbn__AbilityLevel = null;
	}
}
