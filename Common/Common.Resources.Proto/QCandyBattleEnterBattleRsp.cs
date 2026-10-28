using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleEnterBattleRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6571
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "BATTLE_NOT_EXIST")]
		BattleNotExist
	}

	private IExtension __pbn__extensionData;

	private QCandyBattleType? __pbn__BattleType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "battle_type")]
	[DefaultValue(QCandyBattleType.QcandyBattleTypeNone)]
	public QCandyBattleType BattleType
	{
		get
		{
			return __pbn__BattleType.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleType = value;
		}
	}

	[ProtoMember(3, Name = "member_list")]
	public List<QCandyBattleMember> MemberLists { get; } = new List<QCandyBattleMember>();


	[ProtoMember(5, Name = "prop_data_list")]
	public List<QCandyBattlePropData> PropDataLists { get; } = new List<QCandyBattlePropData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleType()
	{
		return __pbn__BattleType.HasValue;
	}

	public void ResetBattleType()
	{
		__pbn__BattleType = null;
	}
}
