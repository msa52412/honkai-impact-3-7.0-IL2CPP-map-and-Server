using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtBattleBeginNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3352
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleId;

	private uint? __pbn__BattleStageId;

	private string __pbn__TransactionStr;

	private bool? __pbn__IsRobotMatch;

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

	[ProtoMember(2, Name = "battle_stage_id")]
	public uint BattleStageId
	{
		get
		{
			return __pbn__BattleStageId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleStageId = value;
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

	[ProtoMember(4, Name = "battle_info")]
	public TvtBattleInfo BattleInfo { get; set; }

	[ProtoMember(5, Name = "lhs_member_list")]
	public List<TvtMember> LhsMemberLists { get; } = new List<TvtMember>();


	[ProtoMember(6, Name = "rhs_member_list")]
	public List<TvtMember> RhsMemberLists { get; } = new List<TvtMember>();


	[ProtoMember(7, Name = "is_robot_match")]
	public bool IsRobotMatch
	{
		get
		{
			return __pbn__IsRobotMatch.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRobotMatch = value;
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

	public bool ShouldSerializeBattleStageId()
	{
		return __pbn__BattleStageId.HasValue;
	}

	public void ResetBattleStageId()
	{
		__pbn__BattleStageId = null;
	}

	public bool ShouldSerializeTransactionStr()
	{
		return __pbn__TransactionStr != null;
	}

	public void ResetTransactionStr()
	{
		__pbn__TransactionStr = null;
	}

	public bool ShouldSerializeIsRobotMatch()
	{
		return __pbn__IsRobotMatch.HasValue;
	}

	public void ResetIsRobotMatch()
	{
		__pbn__IsRobotMatch = null;
	}
}
