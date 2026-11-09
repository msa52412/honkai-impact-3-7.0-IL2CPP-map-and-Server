using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldStageBeginRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4463
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "CYCLE_NOT_OPEN")]
		CycleNotOpen,
		[ProtoEnum(Name = "AVATAR_ERROR")]
		AvatarError,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "AREA_NOT_EXIST")]
		AreaNotExist,
		[ProtoEnum(Name = "AREA_NOT_OPEN")]
		AreaNotOpen,
		[ProtoEnum(Name = "EQUIPMENT_USED")]
		EquipmentUsed,
		[ProtoEnum(Name = "AVATAR_USED")]
		AvatarUsed,
		[ProtoEnum(Name = "ELF_ERROR")]
		ElfError,
		[ProtoEnum(Name = "ELF_CARRY_LEVEL_LIMIT")]
		ElfCarryLevelLimit,
		[ProtoEnum(Name = "FEATURE_LIMIT")]
		FeatureLimit,
		[ProtoEnum(Name = "HONKAI_ENERGY_FULL")]
		HonkaiEnergyFull,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed,
		[ProtoEnum(Name = "AI_CYBER_CONDITION_NOT_MEET")]
		AiCyberConditionNotMeet
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "can_sp_recover_avatar_list")]
	public uint[] CanSpRecoverAvatarLists { get; set; }

	[ProtoMember(3, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(4, Name = "dlc_support_avatar_list")]
	public uint[] DlcSupportAvatarLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}
}
