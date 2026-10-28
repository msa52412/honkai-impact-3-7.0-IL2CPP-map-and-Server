using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TowerRaidBossStage : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__BossLeftHp;

	[ProtoMember(1, Name = "stage_id")]
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

	[ProtoMember(2, Name = "boss_left_hp")]
	public uint BossLeftHp
	{
		get
		{
			return __pbn__BossLeftHp.GetValueOrDefault();
		}
		set
		{
			__pbn__BossLeftHp = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeBossLeftHp()
	{
		return __pbn__BossLeftHp.HasValue;
	}

	public void ResetBossLeftHp()
	{
		__pbn__BossLeftHp = null;
	}
}
