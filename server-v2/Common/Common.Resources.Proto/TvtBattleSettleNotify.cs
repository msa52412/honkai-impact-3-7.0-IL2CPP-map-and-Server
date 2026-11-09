using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtBattleSettleNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3354
	}

	private IExtension __pbn__extensionData;

	private string __pbn__TransactionStr;

	private uint? __pbn__BattleId;

	[ProtoMember(1, Name = "transaction_str")]
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

	[ProtoMember(3, Name = "battle_result")]
	public TvtBattleResult BattleResult { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTransactionStr()
	{
		return __pbn__TransactionStr != null;
	}

	public void ResetTransactionStr()
	{
		__pbn__TransactionStr = null;
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}
}
