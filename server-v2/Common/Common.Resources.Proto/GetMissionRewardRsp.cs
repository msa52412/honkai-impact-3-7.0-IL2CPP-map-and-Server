using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMissionRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 115
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MISSION_ID_ERROR")]
		MissionIdError,
		[ProtoEnum(Name = "MISSION_STATUS_ERROR")]
		MissionStatusError,
		[ProtoEnum(Name = "EQUIPMENT_FULL")]
		EquipmentFull,
		[ProtoEnum(Name = "EQUIPMENT_MATERIAL_LIMIT")]
		EquipmentMaterialLimit,
		[ProtoEnum(Name = "EQUIPMENT_FRAGMENT_LIMIT")]
		EquipmentFragmentLimit,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed,
		[ProtoEnum(Name = "EQUIPMENT_ELF_FRAGMENT_LIMIT")]
		EquipmentElfFragmentLimit,
		[ProtoEnum(Name = "TIME_LIMIT")]
		TimeLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RewardUnlockTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(3, Name = "mission_id_list")]
	public uint[] MissionIdLists { get; set; }

	[ProtoMember(4, Name = "reward_unlock_time")]
	public uint RewardUnlockTime
	{
		get
		{
			return __pbn__RewardUnlockTime.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardUnlockTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRewardUnlockTime()
	{
		return __pbn__RewardUnlockTime.HasValue;
	}

	public void ResetRewardUnlockTime()
	{
		__pbn__RewardUnlockTime = null;
	}
}
