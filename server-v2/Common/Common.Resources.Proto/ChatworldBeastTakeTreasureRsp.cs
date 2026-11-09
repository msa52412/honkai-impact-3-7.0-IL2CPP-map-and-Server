using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldBeastTakeTreasureRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2472
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "HAS_TAKE")]
		HasTake,
		[ProtoEnum(Name = "DAILY_LIMIT")]
		DailyLimit,
		[ProtoEnum(Name = "NOT_EXIST")]
		NotExist,
		[ProtoEnum(Name = "NO_RIGHT")]
		NoRight,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BeastId;

	private uint? __pbn__TreasureRankId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	[ProtoMember(3, Name = "beast_id")]
	public uint BeastId
	{
		get
		{
			return __pbn__BeastId.GetValueOrDefault();
		}
		set
		{
			__pbn__BeastId = value;
		}
	}

	[ProtoMember(4, Name = "treasure_rank_id")]
	public uint TreasureRankId
	{
		get
		{
			return __pbn__TreasureRankId.GetValueOrDefault();
		}
		set
		{
			__pbn__TreasureRankId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBeastId()
	{
		return __pbn__BeastId.HasValue;
	}

	public void ResetBeastId()
	{
		__pbn__BeastId = null;
	}

	public bool ShouldSerializeTreasureRankId()
	{
		return __pbn__TreasureRankId.HasValue;
	}

	public void ResetTreasureRankId()
	{
		__pbn__TreasureRankId = null;
	}
}
