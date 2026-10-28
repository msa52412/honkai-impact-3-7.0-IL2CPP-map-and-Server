using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatchThreeActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__CurScheduleId;

	private uint? __pbn__MaxTowerFloor;

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
	public List<MatchThreeRole> RoleLists { get; } = new List<MatchThreeRole>();


	[ProtoMember(3, Name = "gem_list")]
	public List<MatchThreeGem> GemLists { get; } = new List<MatchThreeGem>();


	[ProtoMember(4, Name = "has_got_daily_material_list")]
	public List<MatchThreeHasGotDailyMaterial> HasGotDailyMaterialLists { get; } = new List<MatchThreeHasGotDailyMaterial>();


	[ProtoMember(5, Name = "item_list")]
	public uint[] ItemLists { get; set; }

	[ProtoMember(6, Name = "cur_schedule_id")]
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

	[ProtoMember(7, Name = "passed_room_id_list")]
	public uint[] PassedRoomIdLists { get; set; }

	[ProtoMember(8, Name = "max_tower_floor")]
	public uint MaxTowerFloor
	{
		get
		{
			return __pbn__MaxTowerFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxTowerFloor = value;
		}
	}

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

	public bool ShouldSerializeMaxTowerFloor()
	{
		return __pbn__MaxTowerFloor.HasValue;
	}

	public void ResetMaxTowerFloor()
	{
		__pbn__MaxTowerFloor = null;
	}
}
