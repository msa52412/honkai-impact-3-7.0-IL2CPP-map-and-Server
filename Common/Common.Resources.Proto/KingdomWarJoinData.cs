using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KingdomWarJoinData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CombatPower;

	private uint? __pbn__LastKingdomId;

	[ProtoMember(1, Name = "combat_power")]
	public uint CombatPower
	{
		get
		{
			return __pbn__CombatPower.GetValueOrDefault();
		}
		set
		{
			__pbn__CombatPower = value;
		}
	}

	[ProtoMember(2, Name = "last_kingdom_id")]
	public uint LastKingdomId
	{
		get
		{
			return __pbn__LastKingdomId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastKingdomId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCombatPower()
	{
		return __pbn__CombatPower.HasValue;
	}

	public void ResetCombatPower()
	{
		__pbn__CombatPower = null;
	}

	public bool ShouldSerializeLastKingdomId()
	{
		return __pbn__LastKingdomId.HasValue;
	}

	public void ResetLastKingdomId()
	{
		__pbn__LastKingdomId = null;
	}
}
