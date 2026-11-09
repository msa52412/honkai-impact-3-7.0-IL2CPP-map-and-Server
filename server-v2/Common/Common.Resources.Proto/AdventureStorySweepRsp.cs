using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AdventureStorySweepRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3913
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_MEET_RESTRICT")]
		NotMeetRestrict,
		[ProtoEnum(Name = "HAS_TAKEN")]
		HasTaken,
		[ProtoEnum(Name = "LACK_MATERIAL")]
		LackMaterial,
		[ProtoEnum(Name = "STAMINA_LACK")]
		StaminaLack,
		[ProtoEnum(Name = "EQUIPMENT_FULL")]
		EquipmentFull,
		[ProtoEnum(Name = "EQUIPMENT_MATERIAL_LIMIT")]
		EquipmentMaterialLimit,
		[ProtoEnum(Name = "EQUIPMENT_FRAGMENT_LIMIT")]
		EquipmentFragmentLimit,
		[ProtoEnum(Name = "ENTER_TIMES_LACK")]
		EnterTimesLack,
		[ProtoEnum(Name = "NOT_MEET_FAST")]
		NotMeetFast,
		[ProtoEnum(Name = "EXCEED_STAMINA")]
		ExceedStamina,
		[ProtoEnum(Name = "NOT_FINISH_CHALLENGE")]
		NotFinishChallenge,
		[ProtoEnum(Name = "AVATAR_SWEEPING")]
		AvatarSweeping,
		[ProtoEnum(Name = "TEAM_LIMIT")]
		TeamLimit,
		[ProtoEnum(Name = "AVATAR_ERROR")]
		AvatarError,
		[ProtoEnum(Name = "STAGE_NOT_EXIST")]
		StageNotExist,
		[ProtoEnum(Name = "ACTIVITY_ENTER_TIMES_LACK")]
		ActivityEnterTimesLack,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__UsedStamina;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "sweep_data")]
	public IslandStorySweepData SweepData { get; set; }

	[ProtoMember(3, Name = "used_stamina")]
	public uint UsedStamina
	{
		get
		{
			return __pbn__UsedStamina.GetValueOrDefault();
		}
		set
		{
			__pbn__UsedStamina = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUsedStamina()
	{
		return __pbn__UsedStamina.HasValue;
	}

	public void ResetUsedStamina()
	{
		__pbn__UsedStamina = null;
	}
}
