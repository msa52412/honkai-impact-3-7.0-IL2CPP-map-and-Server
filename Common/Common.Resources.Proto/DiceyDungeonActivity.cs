using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DiceyDungeonActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__CurScheduleId;

	private uint? __pbn__RefreshOrnamentRandom;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "role_list")]
	public List<DiceyDungeonRole> RoleLists { get; } = new List<DiceyDungeonRole>();


	[ProtoMember(3, Name = "weapon_list")]
	public List<DiceyDungeonWeapon> WeaponLists { get; } = new List<DiceyDungeonWeapon>();


	[ProtoMember(4, Name = "story_dicey_dungeon_info")]
	public DiceyDungeonInfo StoryDiceyDungeonInfo { get; set; }

	[ProtoMember(5, Name = "schedule_dicey_dungeon_info")]
	public DiceyDungeonInfo ScheduleDiceyDungeonInfo { get; set; }

	[ProtoMember(6, Name = "endless_dicey_dungeon_info")]
	public DiceyDungeonInfo EndlessDiceyDungeonInfo { get; set; }

	[ProtoMember(7, Name = "passed_endless_dungeon_room_id_list")]
	public uint[] PassedEndlessDungeonRoomIdLists { get; set; }

	[ProtoMember(8, Name = "cur_schedule_id")]
	public uint CurScheduleId
	{
		get
		{
			return __pbn__CurScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurScheduleId = value;
		}
	}

	[ProtoMember(9, Name = "refresh_ornament_random")]
	public uint RefreshOrnamentRandom
	{
		get
		{
			return __pbn__RefreshOrnamentRandom.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshOrnamentRandom = value;
		}
	}

	[ProtoMember(11, Name = "has_got_daily_material_list")]
	public List<DiceyDungeonHasGotDailyMaterial> HasGotDailyMaterialLists { get; } = new List<DiceyDungeonHasGotDailyMaterial>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeCurScheduleId()
	{
		return __pbn__CurScheduleId.HasValue;
	}

	public void ResetCurScheduleId()
	{
		__pbn__CurScheduleId = null;
	}

	public bool ShouldSerializeRefreshOrnamentRandom()
	{
		return __pbn__RefreshOrnamentRandom.HasValue;
	}

	public void ResetRefreshOrnamentRandom()
	{
		__pbn__RefreshOrnamentRandom = null;
	}
}
