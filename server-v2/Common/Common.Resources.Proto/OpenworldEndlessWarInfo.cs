using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldEndlessWarInfo : IExtensible
{
	[ProtoContract]
	public enum Type
	{
		[ProtoEnum(Name = "NONE")]
		None,
		[ProtoEnum(Name = "SET_TRAP")]
		SetTrap,
		[ProtoEnum(Name = "TRIGGER_TRAP")]
		TriggerTrap,
		[ProtoEnum(Name = "SCENE_GEAR")]
		SceneGear,
		[ProtoEnum(Name = "INVASION")]
		Invasion,
		[ProtoEnum(Name = "INVASION_SUB_SCORE")]
		InvasionSubScore,
		[ProtoEnum(Name = "FINISH_INVASION")]
		FinishInvasion
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private Type? __pbn__type;

	private uint? __pbn__ItemId;

	private uint? __pbn__BattleId;

	private uint? __pbn__TargetId;

	private uint? __pbn__FromWho;

	private uint? __pbn__InvasionId;

	private uint? __pbn__Score;

	private uint? __pbn__SceneGear;

	private uint? __pbn__Time;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2)]
	[DefaultValue(Type.None)]
	public Type type
	{
		get
		{
			return __pbn__type.GetValueOrDefault();
		}
		set
		{
			__pbn__type = value;
		}
	}

	[ProtoMember(3, Name = "item_id")]
	public uint ItemId
	{
		get
		{
			return __pbn__ItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemId = value;
		}
	}

	[ProtoMember(4, Name = "battle_id")]
	public uint BattleId
	{
		get
		{
			return __pbn__BattleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleId = value;
		}
	}

	[ProtoMember(5, Name = "target_id")]
	public uint TargetId
	{
		get
		{
			return __pbn__TargetId.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetId = value;
		}
	}

	[ProtoMember(6, Name = "from_who")]
	public uint FromWho
	{
		get
		{
			return __pbn__FromWho.GetValueOrDefault();
		}
		set
		{
			__pbn__FromWho = value;
		}
	}

	[ProtoMember(7, Name = "invasion_id")]
	public uint InvasionId
	{
		get
		{
			return __pbn__InvasionId.GetValueOrDefault();
		}
		set
		{
			__pbn__InvasionId = value;
		}
	}

	[ProtoMember(8, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(9, Name = "scene_gear")]
	public uint SceneGear
	{
		get
		{
			return __pbn__SceneGear.GetValueOrDefault();
		}
		set
		{
			__pbn__SceneGear = value;
		}
	}

	[ProtoMember(10, Name = "time")]
	public uint Time
	{
		get
		{
			return __pbn__Time.GetValueOrDefault();
		}
		set
		{
			__pbn__Time = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializetype()
	{
		return __pbn__type.HasValue;
	}

	public void Resettype()
	{
		__pbn__type = null;
	}

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}

	public bool ShouldSerializeTargetId()
	{
		return __pbn__TargetId.HasValue;
	}

	public void ResetTargetId()
	{
		__pbn__TargetId = null;
	}

	public bool ShouldSerializeFromWho()
	{
		return __pbn__FromWho.HasValue;
	}

	public void ResetFromWho()
	{
		__pbn__FromWho = null;
	}

	public bool ShouldSerializeInvasionId()
	{
		return __pbn__InvasionId.HasValue;
	}

	public void ResetInvasionId()
	{
		__pbn__InvasionId = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeSceneGear()
	{
		return __pbn__SceneGear.HasValue;
	}

	public void ResetSceneGear()
	{
		__pbn__SceneGear = null;
	}

	public bool ShouldSerializeTime()
	{
		return __pbn__Time.HasValue;
	}

	public void ResetTime()
	{
		__pbn__Time = null;
	}
}
