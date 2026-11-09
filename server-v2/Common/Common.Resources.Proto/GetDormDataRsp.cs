using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetDormDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 602
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private DormDataType? __pbn__DataType;

	private uint? __pbn__LevelUpHouseId;

	private uint? __pbn__LevelUpEndTime;

	private uint? __pbn__LevelUpEndHouseId;

	private uint? __pbn__ShowHouse;

	private uint? __pbn__ShowRoom;

	private uint? __pbn__VisitAvatar;

	private bool? __pbn__IsAllowVisit;

	private uint? __pbn__LastTakeDormDailyStaminaTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "data_type")]
	[DefaultValue(DormDataType.DormDataAll)]
	public DormDataType DataType
	{
		get
		{
			return __pbn__DataType.GetValueOrDefault();
		}
		set
		{
			__pbn__DataType = value;
		}
	}

	[ProtoMember(3, Name = "house_list")]
	public List<DormHouse> HouseLists { get; } = new List<DormHouse>();


	[ProtoMember(4, Name = "finish_talk_plot_id_list")]
	public uint[] FinishTalkPlotIdLists { get; set; }

	[ProtoMember(5, Name = "depot_furniture_list")]
	public List<DepotFurniture> DepotFurnitureLists { get; } = new List<DepotFurniture>();


	[ProtoMember(6, Name = "level_up_house_id")]
	public uint LevelUpHouseId
	{
		get
		{
			return __pbn__LevelUpHouseId.GetValueOrDefault();
		}
		set
		{
			__pbn__LevelUpHouseId = value;
		}
	}

	[ProtoMember(7, Name = "level_up_end_time")]
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

	[ProtoMember(8, Name = "level_up_end_house_id")]
	public uint LevelUpEndHouseId
	{
		get
		{
			return __pbn__LevelUpEndHouseId.GetValueOrDefault();
		}
		set
		{
			__pbn__LevelUpEndHouseId = value;
		}
	}

	[ProtoMember(9, Name = "facility_data")]
	public DormFacilityData FacilityData { get; set; }

	[ProtoMember(10, Name = "event_list")]
	public List<DormEvent> EventLists { get; } = new List<DormEvent>();


	[ProtoMember(11, Name = "show_house")]
	public uint ShowHouse
	{
		get
		{
			return __pbn__ShowHouse.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowHouse = value;
		}
	}

	[ProtoMember(12, Name = "show_room")]
	public uint ShowRoom
	{
		get
		{
			return __pbn__ShowRoom.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowRoom = value;
		}
	}

	[ProtoMember(13, Name = "visit_avatar")]
	public uint VisitAvatar
	{
		get
		{
			return __pbn__VisitAvatar.GetValueOrDefault();
		}
		set
		{
			__pbn__VisitAvatar = value;
		}
	}

	[ProtoMember(14, Name = "is_allow_visit")]
	public bool IsAllowVisit
	{
		get
		{
			return __pbn__IsAllowVisit.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAllowVisit = value;
		}
	}

	[ProtoMember(15, Name = "last_take_dorm_daily_stamina_time")]
	public uint LastTakeDormDailyStaminaTime
	{
		get
		{
			return __pbn__LastTakeDormDailyStaminaTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastTakeDormDailyStaminaTime = value;
		}
	}

	[ProtoMember(16, Name = "unlock_avatar_list")]
	public uint[] UnlockAvatarLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDataType()
	{
		return __pbn__DataType.HasValue;
	}

	public void ResetDataType()
	{
		__pbn__DataType = null;
	}

	public bool ShouldSerializeLevelUpHouseId()
	{
		return __pbn__LevelUpHouseId.HasValue;
	}

	public void ResetLevelUpHouseId()
	{
		__pbn__LevelUpHouseId = null;
	}

	public bool ShouldSerializeLevelUpEndTime()
	{
		return __pbn__LevelUpEndTime.HasValue;
	}

	public void ResetLevelUpEndTime()
	{
		__pbn__LevelUpEndTime = null;
	}

	public bool ShouldSerializeLevelUpEndHouseId()
	{
		return __pbn__LevelUpEndHouseId.HasValue;
	}

	public void ResetLevelUpEndHouseId()
	{
		__pbn__LevelUpEndHouseId = null;
	}

	public bool ShouldSerializeShowHouse()
	{
		return __pbn__ShowHouse.HasValue;
	}

	public void ResetShowHouse()
	{
		__pbn__ShowHouse = null;
	}

	public bool ShouldSerializeShowRoom()
	{
		return __pbn__ShowRoom.HasValue;
	}

	public void ResetShowRoom()
	{
		__pbn__ShowRoom = null;
	}

	public bool ShouldSerializeVisitAvatar()
	{
		return __pbn__VisitAvatar.HasValue;
	}

	public void ResetVisitAvatar()
	{
		__pbn__VisitAvatar = null;
	}

	public bool ShouldSerializeIsAllowVisit()
	{
		return __pbn__IsAllowVisit.HasValue;
	}

	public void ResetIsAllowVisit()
	{
		__pbn__IsAllowVisit = null;
	}

	public bool ShouldSerializeLastTakeDormDailyStaminaTime()
	{
		return __pbn__LastTakeDormDailyStaminaTime.HasValue;
	}

	public void ResetLastTakeDormDailyStaminaTime()
	{
		__pbn__LastTakeDormDailyStaminaTime = null;
	}
}
