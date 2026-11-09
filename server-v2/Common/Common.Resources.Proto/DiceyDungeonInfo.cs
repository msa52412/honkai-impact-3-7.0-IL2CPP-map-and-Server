using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DiceyDungeonInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__DungeonId;

	private uint? __pbn__Floor;

	private uint? __pbn__DungeonRoomId;

	private uint? __pbn__Health;

	private uint? __pbn__LeftRefreshOrnamentTimes;

	private uint? __pbn__DiceyBattleScore;

	private uint? __pbn__TotalDiceyBattleScore;

	[ProtoMember(1, Name = "dungeon_id")]
	public uint DungeonId
	{
		get
		{
			return __pbn__DungeonId.GetValueOrDefault();
		}
		set
		{
			__pbn__DungeonId = value;
		}
	}

	[ProtoMember(2, Name = "floor")]
	public uint Floor
	{
		get
		{
			return __pbn__Floor.GetValueOrDefault();
		}
		set
		{
			__pbn__Floor = value;
		}
	}

	[ProtoMember(3, Name = "dungeon_room_id")]
	public uint DungeonRoomId
	{
		get
		{
			return __pbn__DungeonRoomId.GetValueOrDefault();
		}
		set
		{
			__pbn__DungeonRoomId = value;
		}
	}

	[ProtoMember(4, Name = "role")]
	public DiceyDungeonRole Role { get; set; }

	[ProtoMember(5, Name = "health")]
	public uint Health
	{
		get
		{
			return __pbn__Health.GetValueOrDefault();
		}
		set
		{
			__pbn__Health = value;
		}
	}

	[ProtoMember(6, Name = "weapon")]
	public DiceyDungeonWeapon Weapon { get; set; }

	[ProtoMember(7, Name = "passed_room_id_list")]
	public uint[] PassedRoomIdLists { get; set; }

	[ProtoMember(8, Name = "ornament_list")]
	public List<DiceyDungeonOrnament> OrnamentLists { get; } = new List<DiceyDungeonOrnament>();


	[ProtoMember(9, Name = "wait_select_ornament_id_list")]
	public uint[] WaitSelectOrnamentIdLists { get; set; }

	[ProtoMember(10, Name = "random_dicey_num_list")]
	public uint[] RandomDiceyNumLists { get; set; }

	[ProtoMember(11, Name = "left_refresh_ornament_times")]
	public uint LeftRefreshOrnamentTimes
	{
		get
		{
			return __pbn__LeftRefreshOrnamentTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftRefreshOrnamentTimes = value;
		}
	}

	[ProtoMember(12, Name = "dicey_battle_score")]
	public uint DiceyBattleScore
	{
		get
		{
			return __pbn__DiceyBattleScore.GetValueOrDefault();
		}
		set
		{
			__pbn__DiceyBattleScore = value;
		}
	}

	[ProtoMember(13, Name = "total_dicey_battle_score")]
	public uint TotalDiceyBattleScore
	{
		get
		{
			return __pbn__TotalDiceyBattleScore.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalDiceyBattleScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDungeonId()
	{
		return __pbn__DungeonId.HasValue;
	}

	public void ResetDungeonId()
	{
		__pbn__DungeonId = null;
	}

	public bool ShouldSerializeFloor()
	{
		return __pbn__Floor.HasValue;
	}

	public void ResetFloor()
	{
		__pbn__Floor = null;
	}

	public bool ShouldSerializeDungeonRoomId()
	{
		return __pbn__DungeonRoomId.HasValue;
	}

	public void ResetDungeonRoomId()
	{
		__pbn__DungeonRoomId = null;
	}

	public bool ShouldSerializeHealth()
	{
		return __pbn__Health.HasValue;
	}

	public void ResetHealth()
	{
		__pbn__Health = null;
	}

	public bool ShouldSerializeLeftRefreshOrnamentTimes()
	{
		return __pbn__LeftRefreshOrnamentTimes.HasValue;
	}

	public void ResetLeftRefreshOrnamentTimes()
	{
		__pbn__LeftRefreshOrnamentTimes = null;
	}

	public bool ShouldSerializeDiceyBattleScore()
	{
		return __pbn__DiceyBattleScore.HasValue;
	}

	public void ResetDiceyBattleScore()
	{
		__pbn__DiceyBattleScore = null;
	}

	public bool ShouldSerializeTotalDiceyBattleScore()
	{
		return __pbn__TotalDiceyBattleScore.HasValue;
	}

	public void ResetTotalDiceyBattleScore()
	{
		__pbn__TotalDiceyBattleScore = null;
	}
}
