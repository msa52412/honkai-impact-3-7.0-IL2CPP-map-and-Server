using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PvzBattleBeginRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6553
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "QAVATAR_ERROR")]
		QavatarError,
		[ProtoEnum(Name = "INVALID_TILE")]
		InvalidTile,
		[ProtoEnum(Name = "RPG_SITE_CANNOT_ENTER")]
		RpgSiteCannotEnter,
		[ProtoEnum(Name = "LACK_TICKET")]
		LackTicket,
		[ProtoEnum(Name = "TILE_SITE_MISMATCH")]
		TileSiteMismatch
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__TowerId;

	private uint? __pbn__FloorId;

	private uint? __pbn__GameType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
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

	[ProtoMember(3, Name = "tower_id")]
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

	[ProtoMember(4, Name = "floor_id")]
	public uint FloorId
	{
		get
		{
			return __pbn__FloorId.GetValueOrDefault();
		}
		set
		{
			__pbn__FloorId = value;
		}
	}

	[ProtoMember(5, Name = "qavatar_list")]
	public List<PvzQAvatar> QavatarLists { get; } = new List<PvzQAvatar>();


	[ProtoMember(6, Name = "game_type")]
	public uint GameType
	{
		get
		{
			return __pbn__GameType.GetValueOrDefault();
		}
		set
		{
			__pbn__GameType = value;
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

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}

	public bool ShouldSerializeFloorId()
	{
		return __pbn__FloorId.HasValue;
	}

	public void ResetFloorId()
	{
		__pbn__FloorId = null;
	}

	public bool ShouldSerializeGameType()
	{
		return __pbn__GameType.HasValue;
	}

	public void ResetGameType()
	{
		__pbn__GameType = null;
	}
}
