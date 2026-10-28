using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgBattlePointBossData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BossId;

	private SlgBossType? __pbn__Type;

	private bool? __pbn__IsAlive;

	private uint? __pbn__BossScheduleId;

	[ProtoMember(1, Name = "boss_id")]
	public uint BossId
	{
		get
		{
			return __pbn__BossId.GetValueOrDefault();
		}
		set
		{
			__pbn__BossId = value;
		}
	}

	[ProtoMember(2, Name = "type")]
	[DefaultValue(SlgBossType.SlgBossTypeNone)]
	public SlgBossType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(3, Name = "is_alive")]
	public bool IsAlive
	{
		get
		{
			return __pbn__IsAlive.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAlive = value;
		}
	}

	[ProtoMember(4, Name = "boss_schedule_id")]
	public uint BossScheduleId
	{
		get
		{
			return __pbn__BossScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BossScheduleId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBossId()
	{
		return __pbn__BossId.HasValue;
	}

	public void ResetBossId()
	{
		__pbn__BossId = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeIsAlive()
	{
		return __pbn__IsAlive.HasValue;
	}

	public void ResetIsAlive()
	{
		__pbn__IsAlive = null;
	}

	public bool ShouldSerializeBossScheduleId()
	{
		return __pbn__BossScheduleId.HasValue;
	}

	public void ResetBossScheduleId()
	{
		__pbn__BossScheduleId = null;
	}
}
