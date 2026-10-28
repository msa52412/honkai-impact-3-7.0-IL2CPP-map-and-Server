using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BingoActivityFlopCardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4160
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "ITEMS_LACK")]
		ItemsLack,
		[ProtoEnum(Name = "HAS_FINISED")]
		HasFinised,
		[ProtoEnum(Name = "HAS_FLOPED")]
		HasFloped
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__BingoId;

	private uint? __pbn__CardId;

	private bool? __pbn__IsAllFinished;

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

	[ProtoMember(3, Name = "bingo_id")]
	public uint BingoId
	{
		get
		{
			return __pbn__BingoId.GetValueOrDefault();
		}
		set
		{
			__pbn__BingoId = value;
		}
	}

	[ProtoMember(4, Name = "card_id")]
	public uint CardId
	{
		get
		{
			return __pbn__CardId.GetValueOrDefault();
		}
		set
		{
			__pbn__CardId = value;
		}
	}

	[ProtoMember(5, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(6, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	[ProtoMember(7, Name = "is_all_finished")]
	public bool IsAllFinished
	{
		get
		{
			return __pbn__IsAllFinished.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAllFinished = value;
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

	public bool ShouldSerializeBingoId()
	{
		return __pbn__BingoId.HasValue;
	}

	public void ResetBingoId()
	{
		__pbn__BingoId = null;
	}

	public bool ShouldSerializeCardId()
	{
		return __pbn__CardId.HasValue;
	}

	public void ResetCardId()
	{
		__pbn__CardId = null;
	}

	public bool ShouldSerializeIsAllFinished()
	{
		return __pbn__IsAllFinished.HasValue;
	}

	public void ResetIsAllFinished()
	{
		__pbn__IsAllFinished = null;
	}
}
