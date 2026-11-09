using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownBoss : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MonsterId;

	private uint? __pbn__UniqueId;

	private uint? __pbn__Strength;

	private uint? __pbn__LeftHpRatio;

	private uint? __pbn__BornRound;

	private uint? __pbn__NextBossBornRound;

	private uint? __pbn__NextBossMonsterId;

	[ProtoMember(1, Name = "monster_id")]
	public uint MonsterId
	{
		get
		{
			return __pbn__MonsterId.GetValueOrDefault();
		}
		set
		{
			__pbn__MonsterId = value;
		}
	}

	[ProtoMember(2, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(3, Name = "strength")]
	public uint Strength
	{
		get
		{
			return __pbn__Strength.GetValueOrDefault();
		}
		set
		{
			__pbn__Strength = value;
		}
	}

	[ProtoMember(4, Name = "left_hp_ratio")]
	public uint LeftHpRatio
	{
		get
		{
			return __pbn__LeftHpRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftHpRatio = value;
		}
	}

	[ProtoMember(5, Name = "born_round")]
	public uint BornRound
	{
		get
		{
			return __pbn__BornRound.GetValueOrDefault();
		}
		set
		{
			__pbn__BornRound = value;
		}
	}

	[ProtoMember(6, Name = "next_boss_born_round")]
	public uint NextBossBornRound
	{
		get
		{
			return __pbn__NextBossBornRound.GetValueOrDefault();
		}
		set
		{
			__pbn__NextBossBornRound = value;
		}
	}

	[ProtoMember(7, Name = "next_boss_monster_id")]
	public uint NextBossMonsterId
	{
		get
		{
			return __pbn__NextBossMonsterId.GetValueOrDefault();
		}
		set
		{
			__pbn__NextBossMonsterId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMonsterId()
	{
		return __pbn__MonsterId.HasValue;
	}

	public void ResetMonsterId()
	{
		__pbn__MonsterId = null;
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeStrength()
	{
		return __pbn__Strength.HasValue;
	}

	public void ResetStrength()
	{
		__pbn__Strength = null;
	}

	public bool ShouldSerializeLeftHpRatio()
	{
		return __pbn__LeftHpRatio.HasValue;
	}

	public void ResetLeftHpRatio()
	{
		__pbn__LeftHpRatio = null;
	}

	public bool ShouldSerializeBornRound()
	{
		return __pbn__BornRound.HasValue;
	}

	public void ResetBornRound()
	{
		__pbn__BornRound = null;
	}

	public bool ShouldSerializeNextBossBornRound()
	{
		return __pbn__NextBossBornRound.HasValue;
	}

	public void ResetNextBossBornRound()
	{
		__pbn__NextBossBornRound = null;
	}

	public bool ShouldSerializeNextBossMonsterId()
	{
		return __pbn__NextBossMonsterId.HasValue;
	}

	public void ResetNextBossMonsterId()
	{
		__pbn__NextBossMonsterId = null;
	}
}
