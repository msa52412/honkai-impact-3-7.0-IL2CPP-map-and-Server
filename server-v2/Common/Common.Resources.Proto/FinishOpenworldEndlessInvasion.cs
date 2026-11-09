using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishOpenworldEndlessInvasionMonsterReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3430
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__BattleId;

	private string __pbn__TransactionStr;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "battle_id")]
	public uint BattleId
	{
		get
		{
			return __pbn__BattleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleId = value;
		}
	}

	[ProtoMember(3, Name = "transaction_str")]
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

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
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
[ProtoContract]
public class FinishOpenworldEndlessInvasionMonsterRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3431
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ENDLESS_STATUS_ERROR")]
		EndlessStatusError,
		[ProtoEnum(Name = "NOT_JOIN")]
		NotJoin,
		[ProtoEnum(Name = "NO_INVASION")]
		NoInvasion
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SubScore;

	private uint? __pbn__Id;

	private uint? __pbn__Score;

	private uint? __pbn__BattleId;

	private string __pbn__TransactionStr;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "sub_score")]
	public uint SubScore
	{
		get
		{
			return __pbn__SubScore.GetValueOrDefault();
		}
		set
		{
			__pbn__SubScore = value;
		}
	}

	[ProtoMember(3, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(4, Name = "clear_item_list")]
	public List<OpenworldEndlessItem> ClearItemLists { get; } = new List<OpenworldEndlessItem>();


	[ProtoMember(5, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(6, Name = "battle_id")]
	public uint BattleId
	{
		get
		{
			return __pbn__BattleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleId = value;
		}
	}

	[ProtoMember(7, Name = "transaction_str")]
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

	public bool ShouldSerializeSubScore()
	{
		return __pbn__SubScore.HasValue;
	}

	public void ResetSubScore()
	{
		__pbn__SubScore = null;
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
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
