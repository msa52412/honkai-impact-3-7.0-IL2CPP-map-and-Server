using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownMonster : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

	private uint? __pbn__MonsterId;

	private uint? __pbn__Position;

	private uint? __pbn__Strength;

	private uint? __pbn__LeftHpRatio;

	[ProtoMember(1, Name = "unique_id")]
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

	[ProtoMember(2, Name = "monster_id")]
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

	[ProtoMember(3, Name = "position")]
	public uint Position
	{
		get
		{
			return __pbn__Position.GetValueOrDefault();
		}
		set
		{
			__pbn__Position = value;
		}
	}

	[ProtoMember(4, Name = "strength")]
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

	[ProtoMember(5, Name = "left_hp_ratio")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeMonsterId()
	{
		return __pbn__MonsterId.HasValue;
	}

	public void ResetMonsterId()
	{
		__pbn__MonsterId = null;
	}

	public bool ShouldSerializePosition()
	{
		return __pbn__Position.HasValue;
	}

	public void ResetPosition()
	{
		__pbn__Position = null;
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
}
