using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleEndBattleNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5630
	}

	private IExtension __pbn__extensionData;

	private QAvatarBattleEndReason? __pbn__EndReason;

	private bool? __pbn__IsWin;

	private bool? __pbn__IsMvp;

	[ProtoMember(1, Name = "end_reason")]
	[DefaultValue(QAvatarBattleEndReason.QavatarBattleEndReasonNone)]
	public QAvatarBattleEndReason EndReason
	{
		get
		{
			return __pbn__EndReason.GetValueOrDefault();
		}
		set
		{
			__pbn__EndReason = value;
		}
	}

	[ProtoMember(2, Name = "is_win")]
	public bool IsWin
	{
		get
		{
			return __pbn__IsWin.GetValueOrDefault();
		}
		set
		{
			__pbn__IsWin = value;
		}
	}

	[ProtoMember(3, Name = "is_mvp")]
	public bool IsMvp
	{
		get
		{
			return __pbn__IsMvp.GetValueOrDefault();
		}
		set
		{
			__pbn__IsMvp = value;
		}
	}

	[ProtoMember(4, Name = "fight_info_list")]
	public List<QAvatarBattleFightInfo> FightInfoLists { get; } = new List<QAvatarBattleFightInfo>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEndReason()
	{
		return __pbn__EndReason.HasValue;
	}

	public void ResetEndReason()
	{
		__pbn__EndReason = null;
	}

	public bool ShouldSerializeIsWin()
	{
		return __pbn__IsWin.HasValue;
	}

	public void ResetIsWin()
	{
		__pbn__IsWin = null;
	}

	public bool ShouldSerializeIsMvp()
	{
		return __pbn__IsMvp.HasValue;
	}

	public void ResetIsMvp()
	{
		__pbn__IsMvp = null;
	}
}
