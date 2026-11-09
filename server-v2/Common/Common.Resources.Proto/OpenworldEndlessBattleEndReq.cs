using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldEndlessBattleEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3440
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleId;

	private uint? __pbn__GroupLevel;

	private uint? __pbn__InvasionId;

	private uint? __pbn__InvasionBuffNum;

	private uint? __pbn__CostTime;

	private StageEndStatus? __pbn__EndStatus;

	private uint? __pbn__MonsterGroupId;

	private uint? __pbn__EnvironmentId;

	private uint? __pbn__LayerNumber;

	private string __pbn__TransactionStr;

	private string __pbn__MonsterStr;

	private EndlessType? __pbn__Type;

	private string __pbn__QuantumString;

	private string __pbn__WaveString;

	private string __pbn__AoeString;

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

	[ProtoMember(3, Name = "invasion_id")]
	public uint InvasionId
	{
		get
		{
			return __pbn__InvasionId.GetValueOrDefault();
		}
		set
		{
			__pbn__InvasionId = value;
		}
	}

	[ProtoMember(4, Name = "invasion_buff_num")]
	public uint InvasionBuffNum
	{
		get
		{
			return __pbn__InvasionBuffNum.GetValueOrDefault();
		}
		set
		{
			__pbn__InvasionBuffNum = value;
		}
	}

	[ProtoMember(5, Name = "cost_time")]
	public uint CostTime
	{
		get
		{
			return __pbn__CostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CostTime = value;
		}
	}

	[ProtoMember(6, Name = "end_status")]
	[DefaultValue(StageEndStatus.StageWin)]
	public StageEndStatus EndStatus
	{
		get
		{
			return __pbn__EndStatus ?? StageEndStatus.StageWin;
		}
		set
		{
			__pbn__EndStatus = value;
		}
	}

	[ProtoMember(7, Name = "avatar_list")]
	public List<StageInnerAvatarData> AvatarLists { get; } = new List<StageInnerAvatarData>();


	[ProtoMember(8, Name = "monster_group_id")]
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

	[ProtoMember(9, Name = "environment_id")]
	public uint EnvironmentId
	{
		get
		{
			return __pbn__EnvironmentId.GetValueOrDefault();
		}
		set
		{
			__pbn__EnvironmentId = value;
		}
	}

	[ProtoMember(10, Name = "layer_number")]
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

	[ProtoMember(11, Name = "transaction_str")]
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

	[ProtoMember(12, Name = "monster_str")]
	[DefaultValue("")]
	public string MonsterStr
	{
		get
		{
			return __pbn__MonsterStr ?? "";
		}
		set
		{
			__pbn__MonsterStr = value;
		}
	}

	[ProtoMember(13, Name = "elf_list")]
	public List<StageInnerElfData> ElfLists { get; } = new List<StageInnerElfData>();


	[ProtoMember(14, Name = "type")]
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

	[ProtoMember(15, Name = "quantum_string")]
	[DefaultValue("")]
	public string QuantumString
	{
		get
		{
			return __pbn__QuantumString ?? "";
		}
		set
		{
			__pbn__QuantumString = value;
		}
	}

	[ProtoMember(16, Name = "wave_string")]
	[DefaultValue("")]
	public string WaveString
	{
		get
		{
			return __pbn__WaveString ?? "";
		}
		set
		{
			__pbn__WaveString = value;
		}
	}

	[ProtoMember(17, Name = "aoe_string")]
	[DefaultValue("")]
	public string AoeString
	{
		get
		{
			return __pbn__AoeString ?? "";
		}
		set
		{
			__pbn__AoeString = value;
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

	public bool ShouldSerializeInvasionId()
	{
		return __pbn__InvasionId.HasValue;
	}

	public void ResetInvasionId()
	{
		__pbn__InvasionId = null;
	}

	public bool ShouldSerializeInvasionBuffNum()
	{
		return __pbn__InvasionBuffNum.HasValue;
	}

	public void ResetInvasionBuffNum()
	{
		__pbn__InvasionBuffNum = null;
	}

	public bool ShouldSerializeCostTime()
	{
		return __pbn__CostTime.HasValue;
	}

	public void ResetCostTime()
	{
		__pbn__CostTime = null;
	}

	public bool ShouldSerializeEndStatus()
	{
		return __pbn__EndStatus.HasValue;
	}

	public void ResetEndStatus()
	{
		__pbn__EndStatus = null;
	}

	public bool ShouldSerializeMonsterGroupId()
	{
		return __pbn__MonsterGroupId.HasValue;
	}

	public void ResetMonsterGroupId()
	{
		__pbn__MonsterGroupId = null;
	}

	public bool ShouldSerializeEnvironmentId()
	{
		return __pbn__EnvironmentId.HasValue;
	}

	public void ResetEnvironmentId()
	{
		__pbn__EnvironmentId = null;
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

	public bool ShouldSerializeMonsterStr()
	{
		return __pbn__MonsterStr != null;
	}

	public void ResetMonsterStr()
	{
		__pbn__MonsterStr = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeQuantumString()
	{
		return __pbn__QuantumString != null;
	}

	public void ResetQuantumString()
	{
		__pbn__QuantumString = null;
	}

	public bool ShouldSerializeWaveString()
	{
		return __pbn__WaveString != null;
	}

	public void ResetWaveString()
	{
		__pbn__WaveString = null;
	}

	public bool ShouldSerializeAoeString()
	{
		return __pbn__AoeString != null;
	}

	public void ResetAoeString()
	{
		__pbn__AoeString = null;
	}
}
