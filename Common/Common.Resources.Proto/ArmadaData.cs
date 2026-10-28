using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaData : IExtensible
{
	private IExtension __pbn__extensionData;

	private ArmadaPosition? __pbn__Position;

	private ArmadaCabinType? __pbn__LevelUpCabinType;

	private uint? __pbn__LevelUpCabinId;

	private uint? __pbn__LevelUpEndTime;

	private uint? __pbn__DissolveTime;

	private bool? __pbn__IsArmadaChangeNameFree;

	[ProtoMember(1, Name = "brief")]
	public ArmadaBriefData Brief { get; set; }

	[ProtoMember(2, Name = "member_list")]
	public List<ArmadaMember> MemberLists { get; } = new List<ArmadaMember>();


	[ProtoMember(3, Name = "position")]
	[DefaultValue(ArmadaPosition.ArmadaPositionNone)]
	public ArmadaPosition Position
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

	[ProtoMember(4, Name = "grid_list")]
	public List<ArmadaGrid> GridLists { get; } = new List<ArmadaGrid>();


	[ProtoMember(5, Name = "contact_list")]
	public List<ArmadaContact> ContactLists { get; } = new List<ArmadaContact>();


	[ProtoMember(6, Name = "hangar_list")]
	public List<ArmadaHangar> HangarLists { get; } = new List<ArmadaHangar>();


	[ProtoMember(7, Name = "workshop_list")]
	public List<ArmadaWorkshop> WorkshopLists { get; } = new List<ArmadaWorkshop>();


	[ProtoMember(8, Name = "exchequer_list")]
	public List<ArmadaExchequer> ExchequerLists { get; } = new List<ArmadaExchequer>();


	[ProtoMember(9, Name = "bossroom_list")]
	public List<ArmadaBossroom> BossroomLists { get; } = new List<ArmadaBossroom>();


	[ProtoMember(10, Name = "level_up_cabin_type")]
	[DefaultValue(ArmadaCabinType.ArmadaCabinNone)]
	public ArmadaCabinType LevelUpCabinType
	{
		get
		{
			return __pbn__LevelUpCabinType.GetValueOrDefault();
		}
		set
		{
			__pbn__LevelUpCabinType = value;
		}
	}

	[ProtoMember(11, Name = "level_up_cabin_id")]
	public uint LevelUpCabinId
	{
		get
		{
			return __pbn__LevelUpCabinId.GetValueOrDefault();
		}
		set
		{
			__pbn__LevelUpCabinId = value;
		}
	}

	[ProtoMember(12, Name = "level_up_end_time")]
	public uint LevelUpEndTime
	{
		get
		{
			return __pbn__LevelUpEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LevelUpEndTime = value;
		}
	}

	[ProtoMember(13, Name = "dissolve_time")]
	public uint DissolveTime
	{
		get
		{
			return __pbn__DissolveTime.GetValueOrDefault();
		}
		set
		{
			__pbn__DissolveTime = value;
		}
	}

	[ProtoMember(14, Name = "armada_player_data")]
	public ArmadaPlayerData ArmadaPlayerData { get; set; }

	[ProtoMember(15, Name = "impeach_data")]
	public ArmadaImpeachData ImpeachData { get; set; }

	[ProtoMember(16, Name = "is_armada_change_name_free")]
	public bool IsArmadaChangeNameFree
	{
		get
		{
			return __pbn__IsArmadaChangeNameFree.GetValueOrDefault();
		}
		set
		{
			__pbn__IsArmadaChangeNameFree = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePosition()
	{
		return __pbn__Position.HasValue;
	}

	public void ResetPosition()
	{
		__pbn__Position = null;
	}

	public bool ShouldSerializeLevelUpCabinType()
	{
		return __pbn__LevelUpCabinType.HasValue;
	}

	public void ResetLevelUpCabinType()
	{
		__pbn__LevelUpCabinType = null;
	}

	public bool ShouldSerializeLevelUpCabinId()
	{
		return __pbn__LevelUpCabinId.HasValue;
	}

	public void ResetLevelUpCabinId()
	{
		__pbn__LevelUpCabinId = null;
	}

	public bool ShouldSerializeLevelUpEndTime()
	{
		return __pbn__LevelUpEndTime.HasValue;
	}

	public void ResetLevelUpEndTime()
	{
		__pbn__LevelUpEndTime = null;
	}

	public bool ShouldSerializeDissolveTime()
	{
		return __pbn__DissolveTime.HasValue;
	}

	public void ResetDissolveTime()
	{
		__pbn__DissolveTime = null;
	}

	public bool ShouldSerializeIsArmadaChangeNameFree()
	{
		return __pbn__IsArmadaChangeNameFree.HasValue;
	}

	public void ResetIsArmadaChangeNameFree()
	{
		__pbn__IsArmadaChangeNameFree = null;
	}
}
