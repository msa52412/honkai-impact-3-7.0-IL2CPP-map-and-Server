using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UseOpenworldEndlessItemRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3423
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ITEM_LACK")]
		ItemLack,
		[ProtoEnum(Name = "INVASION_AVATAR_ERROR")]
		InvasionAvatarError,
		[ProtoEnum(Name = "TARGET_ERROR")]
		TargetError,
		[ProtoEnum(Name = "TARGET_GROUP_ERROR")]
		TargetGroupError,
		[ProtoEnum(Name = "OUT_OF_RANGE")]
		OutOfRange,
		[ProtoEnum(Name = "INVASION_FULL")]
		InvasionFull,
		[ProtoEnum(Name = "INVASION_ID_ERROR")]
		InvasionIdError,
		[ProtoEnum(Name = "ENDLESS_STATUS_ERROR")]
		EndlessStatusError,
		[ProtoEnum(Name = "NOT_JOIN")]
		NotJoin,
		[ProtoEnum(Name = "TRAP_FULL")]
		TrapFull
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__NewInvasionId;

	private uint? __pbn__ItemId;

	private uint? __pbn__BattleId;

	private uint? __pbn__OldInvaisonId;

	private uint? __pbn__TargetId;

	private uint? __pbn__InvasionBuffNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "new_invasion_id")]
	public uint NewInvasionId
	{
		get
		{
			return __pbn__NewInvasionId.GetValueOrDefault();
		}
		set
		{
			__pbn__NewInvasionId = value;
		}
	}

	[ProtoMember(3, Name = "item_id")]
	public uint ItemId
	{
		get
		{
			return __pbn__ItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemId = value;
		}
	}

	[ProtoMember(4, Name = "battle_id")]
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

	[ProtoMember(5, Name = "effect_item_list")]
	public List<OpenworldEndlessItem> EffectItemLists { get; } = new List<OpenworldEndlessItem>();


	[ProtoMember(6, Name = "old_invaison_id")]
	public uint OldInvaisonId
	{
		get
		{
			return __pbn__OldInvaisonId.GetValueOrDefault();
		}
		set
		{
			__pbn__OldInvaisonId = value;
		}
	}

	[ProtoMember(7, Name = "target_id")]
	public uint TargetId
	{
		get
		{
			return __pbn__TargetId.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetId = value;
		}
	}

	[ProtoMember(8, Name = "invasion_buff_num")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNewInvasionId()
	{
		return __pbn__NewInvasionId.HasValue;
	}

	public void ResetNewInvasionId()
	{
		__pbn__NewInvasionId = null;
	}

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}

	public bool ShouldSerializeOldInvaisonId()
	{
		return __pbn__OldInvaisonId.HasValue;
	}

	public void ResetOldInvaisonId()
	{
		__pbn__OldInvaisonId = null;
	}

	public bool ShouldSerializeTargetId()
	{
		return __pbn__TargetId.HasValue;
	}

	public void ResetTargetId()
	{
		__pbn__TargetId = null;
	}

	public bool ShouldSerializeInvasionBuffNum()
	{
		return __pbn__InvasionBuffNum.HasValue;
	}

	public void ResetInvasionBuffNum()
	{
		__pbn__InvasionBuffNum = null;
	}
}
