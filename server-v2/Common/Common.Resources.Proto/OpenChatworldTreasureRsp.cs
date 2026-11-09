using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenChatworldTreasureRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2418
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "OPEN_NUM_LIMIT")]
		OpenNumLimit,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "HAS_OPEN")]
		HasOpen,
		[ProtoEnum(Name = "HAS_TAKEN")]
		HasTaken,
		[ProtoEnum(Name = "TREASURE_EXPIRE")]
		TreasureExpire,
		[ProtoEnum(Name = "NOT_IN_CHATWORLD")]
		NotInChatworld
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatworldId;

	private uint? __pbn__TreasureId;

	private bool? __pbn__IsTakeReward;

	private uint? __pbn__CurTreasureScheduleId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chatworld_id")]
	public uint ChatworldId
	{
		get
		{
			return __pbn__ChatworldId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatworldId = value;
		}
	}

	[ProtoMember(3, Name = "treasure_id")]
	public uint TreasureId
	{
		get
		{
			return __pbn__TreasureId.GetValueOrDefault();
		}
		set
		{
			__pbn__TreasureId = value;
		}
	}

	[ProtoMember(4, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	[ProtoMember(5, Name = "is_take_reward")]
	public bool IsTakeReward
	{
		get
		{
			return __pbn__IsTakeReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeReward = value;
		}
	}

	[ProtoMember(6, Name = "cur_treasure_schedule_id")]
	public uint CurTreasureScheduleId
	{
		get
		{
			return __pbn__CurTreasureScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurTreasureScheduleId = value;
		}
	}

	[ProtoMember(7, Name = "open_uid_list")]
	public uint[] OpenUidLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatworldId()
	{
		return __pbn__ChatworldId.HasValue;
	}

	public void ResetChatworldId()
	{
		__pbn__ChatworldId = null;
	}

	public bool ShouldSerializeTreasureId()
	{
		return __pbn__TreasureId.HasValue;
	}

	public void ResetTreasureId()
	{
		__pbn__TreasureId = null;
	}

	public bool ShouldSerializeIsTakeReward()
	{
		return __pbn__IsTakeReward.HasValue;
	}

	public void ResetIsTakeReward()
	{
		__pbn__IsTakeReward = null;
	}

	public bool ShouldSerializeCurTreasureScheduleId()
	{
		return __pbn__CurTreasureScheduleId.HasValue;
	}

	public void ResetCurTreasureScheduleId()
	{
		__pbn__CurTreasureScheduleId = null;
	}
}
