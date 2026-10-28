using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldEndlessBattleBeginReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3438
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleId;

	private uint? __pbn__GroupLevel;

	private uint? __pbn__MonsterGroupId;

	private uint? __pbn__LayerNumber;

	private string __pbn__TransactionStr;

	private EndlessType? __pbn__Type;

	[ProtoMember(1, Name = "battle_id")]
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

	[ProtoMember(2, Name = "group_level")]
	public uint GroupLevel
	{
		get
		{
			return __pbn__GroupLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupLevel = value;
		}
	}

	[ProtoMember(3, Name = "monster_group_id")]
	public uint MonsterGroupId
	{
		get
		{
			return __pbn__MonsterGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__MonsterGroupId = value;
		}
	}

	[ProtoMember(4, Name = "layer_number")]
	public uint LayerNumber
	{
		get
		{
			return __pbn__LayerNumber.GetValueOrDefault();
		}
		set
		{
			__pbn__LayerNumber = value;
		}
	}

	[ProtoMember(5, Name = "transaction_str")]
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

	[ProtoMember(6, Name = "type")]
	[DefaultValue(EndlessType.EndlessTypeNone)]
	public EndlessType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}

	public bool ShouldSerializeGroupLevel()
	{
		return __pbn__GroupLevel.HasValue;
	}

	public void ResetGroupLevel()
	{
		__pbn__GroupLevel = null;
	}

	public bool ShouldSerializeMonsterGroupId()
	{
		return __pbn__MonsterGroupId.HasValue;
	}

	public void ResetMonsterGroupId()
	{
		__pbn__MonsterGroupId = null;
	}

	public bool ShouldSerializeLayerNumber()
	{
		return __pbn__LayerNumber.HasValue;
	}

	public void ResetLayerNumber()
	{
		__pbn__LayerNumber = null;
	}

	public bool ShouldSerializeTransactionStr()
	{
		return __pbn__TransactionStr != null;
	}

	public void ResetTransactionStr()
	{
		__pbn__TransactionStr = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}
}
