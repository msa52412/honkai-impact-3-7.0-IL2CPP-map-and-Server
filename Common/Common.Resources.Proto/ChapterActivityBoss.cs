using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterActivityBoss : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BossStageId;

	private uint? __pbn__LeftHp;

	private uint? __pbn__CurSectionId;

	[ProtoMember(1, Name = "boss_stage_id")]
	public uint BossStageId
	{
		get
		{
			return __pbn__BossStageId.GetValueOrDefault();
		}
		set
		{
			__pbn__BossStageId = value;
		}
	}

	[ProtoMember(2, Name = "left_hp")]
	public uint LeftHp
	{
		get
		{
			return __pbn__LeftHp.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftHp = value;
		}
	}

	[ProtoMember(3, Name = "cur_section_id")]
	public uint CurSectionId
	{
		get
		{
			return __pbn__CurSectionId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurSectionId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBossStageId()
	{
		return __pbn__BossStageId.HasValue;
	}

	public void ResetBossStageId()
	{
		__pbn__BossStageId = null;
	}

	public bool ShouldSerializeLeftHp()
	{
		return __pbn__LeftHp.HasValue;
	}

	public void ResetLeftHp()
	{
		__pbn__LeftHp = null;
	}

	public bool ShouldSerializeCurSectionId()
	{
		return __pbn__CurSectionId.HasValue;
	}

	public void ResetCurSectionId()
	{
		__pbn__CurSectionId = null;
	}
}
