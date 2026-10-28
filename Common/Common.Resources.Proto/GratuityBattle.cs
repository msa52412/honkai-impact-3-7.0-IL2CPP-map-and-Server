using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GratuityBattle : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageUniqueId;

	private uint? __pbn__Damage;

	private uint? __pbn__StageBeginTime;

	private bool? __pbn__IsBonus;

	private uint? __pbn__StageId;

	[ProtoMember(1, Name = "stage_unique_id")]
	public uint StageUniqueId
	{
		get
		{
			return __pbn__StageUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageUniqueId = value;
		}
	}

	[ProtoMember(2, Name = "player_info")]
	public GratuityPlayer PlayerInfo { get; set; }

	[ProtoMember(3, Name = "damage")]
	public uint Damage
	{
		get
		{
			return __pbn__Damage.GetValueOrDefault();
		}
		set
		{
			__pbn__Damage = value;
		}
	}

	[ProtoMember(4, Name = "stage_begin_time")]
	public uint StageBeginTime
	{
		get
		{
			return __pbn__StageBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__StageBeginTime = value;
		}
	}

	[ProtoMember(5, Name = "is_bonus")]
	public bool IsBonus
	{
		get
		{
			return __pbn__IsBonus.GetValueOrDefault();
		}
		set
		{
			__pbn__IsBonus = value;
		}
	}

	[ProtoMember(6, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageUniqueId()
	{
		return __pbn__StageUniqueId.HasValue;
	}

	public void ResetStageUniqueId()
	{
		__pbn__StageUniqueId = null;
	}

	public bool ShouldSerializeDamage()
	{
		return __pbn__Damage.HasValue;
	}

	public void ResetDamage()
	{
		__pbn__Damage = null;
	}

	public bool ShouldSerializeStageBeginTime()
	{
		return __pbn__StageBeginTime.HasValue;
	}

	public void ResetStageBeginTime()
	{
		__pbn__StageBeginTime = null;
	}

	public bool ShouldSerializeIsBonus()
	{
		return __pbn__IsBonus.HasValue;
	}

	public void ResetIsBonus()
	{
		__pbn__IsBonus = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}
}
