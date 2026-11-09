using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleSkillInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SkillId;

	private uint? __pbn__SkillType;

	private ulong? __pbn__BtnHoldupTimeMsU64;

	private uint? __pbn__ComboNum;

	private uint? __pbn__MaxBulletNum;

	private uint? __pbn__CurBulletNum;

	private uint? __pbn__MaxChargeNum;

	private uint? __pbn__CurChargeNum;

	private ulong? __pbn__SkillCdMsU64;

	[ProtoMember(1, Name = "skill_id")]
	public uint SkillId
	{
		get
		{
			return __pbn__SkillId.GetValueOrDefault();
		}
		set
		{
			__pbn__SkillId = value;
		}
	}

	[ProtoMember(2, Name = "skill_type")]
	public uint SkillType
	{
		get
		{
			return __pbn__SkillType.GetValueOrDefault();
		}
		set
		{
			__pbn__SkillType = value;
		}
	}

	[ProtoMember(4, Name = "btn_holdup_time_ms_u64")]
	public ulong BtnHoldupTimeMsU64
	{
		get
		{
			return __pbn__BtnHoldupTimeMsU64.GetValueOrDefault();
		}
		set
		{
			__pbn__BtnHoldupTimeMsU64 = value;
		}
	}

	[ProtoMember(5, Name = "combo_num")]
	public uint ComboNum
	{
		get
		{
			return __pbn__ComboNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ComboNum = value;
		}
	}

	[ProtoMember(6, Name = "max_bullet_num")]
	public uint MaxBulletNum
	{
		get
		{
			return __pbn__MaxBulletNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxBulletNum = value;
		}
	}

	[ProtoMember(7, Name = "cur_bullet_num")]
	public uint CurBulletNum
	{
		get
		{
			return __pbn__CurBulletNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CurBulletNum = value;
		}
	}

	[ProtoMember(8, Name = "max_charge_num")]
	public uint MaxChargeNum
	{
		get
		{
			return __pbn__MaxChargeNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxChargeNum = value;
		}
	}

	[ProtoMember(9, Name = "cur_charge_num")]
	public uint CurChargeNum
	{
		get
		{
			return __pbn__CurChargeNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CurChargeNum = value;
		}
	}

	[ProtoMember(10, Name = "skill_cd_ms_u64")]
	public ulong SkillCdMsU64
	{
		get
		{
			return __pbn__SkillCdMsU64.GetValueOrDefault();
		}
		set
		{
			__pbn__SkillCdMsU64 = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSkillId()
	{
		return __pbn__SkillId.HasValue;
	}

	public void ResetSkillId()
	{
		__pbn__SkillId = null;
	}

	public bool ShouldSerializeSkillType()
	{
		return __pbn__SkillType.HasValue;
	}

	public void ResetSkillType()
	{
		__pbn__SkillType = null;
	}

	public bool ShouldSerializeBtnHoldupTimeMsU64()
	{
		return __pbn__BtnHoldupTimeMsU64.HasValue;
	}

	public void ResetBtnHoldupTimeMsU64()
	{
		__pbn__BtnHoldupTimeMsU64 = null;
	}

	public bool ShouldSerializeComboNum()
	{
		return __pbn__ComboNum.HasValue;
	}

	public void ResetComboNum()
	{
		__pbn__ComboNum = null;
	}

	public bool ShouldSerializeMaxBulletNum()
	{
		return __pbn__MaxBulletNum.HasValue;
	}

	public void ResetMaxBulletNum()
	{
		__pbn__MaxBulletNum = null;
	}

	public bool ShouldSerializeCurBulletNum()
	{
		return __pbn__CurBulletNum.HasValue;
	}

	public void ResetCurBulletNum()
	{
		__pbn__CurBulletNum = null;
	}

	public bool ShouldSerializeMaxChargeNum()
	{
		return __pbn__MaxChargeNum.HasValue;
	}

	public void ResetMaxChargeNum()
	{
		__pbn__MaxChargeNum = null;
	}

	public bool ShouldSerializeCurChargeNum()
	{
		return __pbn__CurChargeNum.HasValue;
	}

	public void ResetCurChargeNum()
	{
		__pbn__CurChargeNum = null;
	}

	public bool ShouldSerializeSkillCdMsU64()
	{
		return __pbn__SkillCdMsU64.HasValue;
	}

	public void ResetSkillCdMsU64()
	{
		__pbn__SkillCdMsU64 = null;
	}
}
