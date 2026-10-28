using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownAvatar : IExtensible
{
	[ProtoContract]
	public enum Status
	{
		[ProtoEnum(Name = "NORMAL")]
		Normal,
		[ProtoEnum(Name = "HEALING")]
		Healing,
		[ProtoEnum(Name = "DEAD")]
		Dead
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__TileUniqueId;

	private uint? __pbn__LeftStep;

	private uint? __pbn__TilePosition;

	private uint? __pbn__LeftHpRatio;

	private Status? __pbn__status;

	private uint? __pbn__StatusBeginRound;

	private uint? __pbn__CheckInBuildingUniqueId;

	private uint? __pbn__SpeedUpHealTimes;

	[ProtoMember(1, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(2, Name = "tile_unique_id")]
	public uint TileUniqueId
	{
		get
		{
			return __pbn__TileUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__TileUniqueId = value;
		}
	}

	[ProtoMember(3, Name = "left_step")]
	public uint LeftStep
	{
		get
		{
			return __pbn__LeftStep.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftStep = value;
		}
	}

	[ProtoMember(4, Name = "tile_position")]
	public uint TilePosition
	{
		get
		{
			return __pbn__TilePosition.GetValueOrDefault();
		}
		set
		{
			__pbn__TilePosition = value;
		}
	}

	[ProtoMember(5, Name = "left_hp_ratio")]
	public uint LeftHpRatio
	{
		get
		{
			return __pbn__LeftHpRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftHpRatio = value;
		}
	}

	[ProtoMember(6)]
	[DefaultValue(Status.Normal)]
	public Status status
	{
		get
		{
			return __pbn__status.GetValueOrDefault();
		}
		set
		{
			__pbn__status = value;
		}
	}

	[ProtoMember(7, Name = "status_begin_round")]
	public uint StatusBeginRound
	{
		get
		{
			return __pbn__StatusBeginRound.GetValueOrDefault();
		}
		set
		{
			__pbn__StatusBeginRound = value;
		}
	}

	[ProtoMember(8, Name = "check_in_building_unique_id")]
	public uint CheckInBuildingUniqueId
	{
		get
		{
			return __pbn__CheckInBuildingUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__CheckInBuildingUniqueId = value;
		}
	}

	[ProtoMember(9, Name = "buff_list")]
	public List<TownBuff> BuffLists { get; } = new List<TownBuff>();


	[ProtoMember(10, Name = "speed_up_heal_times")]
	public uint SpeedUpHealTimes
	{
		get
		{
			return __pbn__SpeedUpHealTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__SpeedUpHealTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeTileUniqueId()
	{
		return __pbn__TileUniqueId.HasValue;
	}

	public void ResetTileUniqueId()
	{
		__pbn__TileUniqueId = null;
	}

	public bool ShouldSerializeLeftStep()
	{
		return __pbn__LeftStep.HasValue;
	}

	public void ResetLeftStep()
	{
		__pbn__LeftStep = null;
	}

	public bool ShouldSerializeTilePosition()
	{
		return __pbn__TilePosition.HasValue;
	}

	public void ResetTilePosition()
	{
		__pbn__TilePosition = null;
	}

	public bool ShouldSerializeLeftHpRatio()
	{
		return __pbn__LeftHpRatio.HasValue;
	}

	public void ResetLeftHpRatio()
	{
		__pbn__LeftHpRatio = null;
	}

	public bool ShouldSerializestatus()
	{
		return __pbn__status.HasValue;
	}

	public void Resetstatus()
	{
		__pbn__status = null;
	}

	public bool ShouldSerializeStatusBeginRound()
	{
		return __pbn__StatusBeginRound.HasValue;
	}

	public void ResetStatusBeginRound()
	{
		__pbn__StatusBeginRound = null;
	}

	public bool ShouldSerializeCheckInBuildingUniqueId()
	{
		return __pbn__CheckInBuildingUniqueId.HasValue;
	}

	public void ResetCheckInBuildingUniqueId()
	{
		__pbn__CheckInBuildingUniqueId = null;
	}

	public bool ShouldSerializeSpeedUpHealTimes()
	{
		return __pbn__SpeedUpHealTimes.HasValue;
	}

	public void ResetSpeedUpHealTimes()
	{
		__pbn__SpeedUpHealTimes = null;
	}
}
