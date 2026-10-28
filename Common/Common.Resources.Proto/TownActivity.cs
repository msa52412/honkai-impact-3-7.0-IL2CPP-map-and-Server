using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TownId;

	private uint? __pbn__TowerId;

	private uint? __pbn__GameRound;

	private uint? __pbn__NextBrickRound;

	private bool? __pbn__IsGameEnd;

	private string __pbn__TransactionStr;

	[ProtoMember(1, Name = "town_id")]
	public uint TownId
	{
		get
		{
			return __pbn__TownId.GetValueOrDefault();
		}
		set
		{
			__pbn__TownId = value;
		}
	}

	[ProtoMember(2, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	[ProtoMember(3, Name = "game_round")]
	public uint GameRound
	{
		get
		{
			return __pbn__GameRound.GetValueOrDefault();
		}
		set
		{
			__pbn__GameRound = value;
		}
	}

	[ProtoMember(4, Name = "avatar_list")]
	public List<TownAvatar> AvatarLists { get; } = new List<TownAvatar>();


	[ProtoMember(5, Name = "brick_id_list")]
	public uint[] BrickIdLists { get; set; }

	[ProtoMember(6, Name = "next_brick_round")]
	public uint NextBrickRound
	{
		get
		{
			return __pbn__NextBrickRound.GetValueOrDefault();
		}
		set
		{
			__pbn__NextBrickRound = value;
		}
	}

	[ProtoMember(7, Name = "building_list")]
	public List<TownBuilding> BuildingLists { get; } = new List<TownBuilding>();


	[ProtoMember(8, Name = "valid_building_id_list")]
	public uint[] ValidBuildingIdLists { get; set; }

	[ProtoMember(9, Name = "monster_list")]
	public List<TownMonster> MonsterLists { get; } = new List<TownMonster>();


	[ProtoMember(10, Name = "event_list")]
	public List<TownEvent> EventLists { get; } = new List<TownEvent>();


	[ProtoMember(11, Name = "boss")]
	public TownBoss Boss { get; set; }

	[ProtoMember(12, Name = "buff_list")]
	public List<TownBuff> BuffLists { get; } = new List<TownBuff>();


	[ProtoMember(13, Name = "trigger_list")]
	public List<TownPassByTrigger> TriggerLists { get; } = new List<TownPassByTrigger>();


	[ProtoMember(14, Name = "challenge_list")]
	public List<TownChallenge> ChallengeLists { get; } = new List<TownChallenge>();


	[ProtoMember(15, Name = "finished_tower_id_list")]
	public uint[] FinishedTowerIdLists { get; set; }

	[ProtoMember(16, Name = "is_game_end")]
	public bool IsGameEnd
	{
		get
		{
			return __pbn__IsGameEnd.GetValueOrDefault();
		}
		set
		{
			__pbn__IsGameEnd = value;
		}
	}

	[ProtoMember(17, Name = "path_list")]
	public List<TownPath> PathLists { get; } = new List<TownPath>();


	[ProtoMember(18, Name = "transaction_str")]
	[DefaultValue("")]
	public string TransactionStr
	{
		get
		{
			return __pbn__TransactionStr ?? "";
		}
		set
		{
			__pbn__TransactionStr = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTownId()
	{
		return __pbn__TownId.HasValue;
	}

	public void ResetTownId()
	{
		__pbn__TownId = null;
	}

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}

	public bool ShouldSerializeGameRound()
	{
		return __pbn__GameRound.HasValue;
	}

	public void ResetGameRound()
	{
		__pbn__GameRound = null;
	}

	public bool ShouldSerializeNextBrickRound()
	{
		return __pbn__NextBrickRound.HasValue;
	}

	public void ResetNextBrickRound()
	{
		__pbn__NextBrickRound = null;
	}

	public bool ShouldSerializeIsGameEnd()
	{
		return __pbn__IsGameEnd.HasValue;
	}

	public void ResetIsGameEnd()
	{
		__pbn__IsGameEnd = null;
	}

	public bool ShouldSerializeTransactionStr()
	{
		return __pbn__TransactionStr != null;
	}

	public void ResetTransactionStr()
	{
		__pbn__TransactionStr = null;
	}
}
