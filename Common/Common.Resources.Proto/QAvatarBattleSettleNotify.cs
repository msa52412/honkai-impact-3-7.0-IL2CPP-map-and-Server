using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleSettleNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5551
	}

	private IExtension __pbn__extensionData;

	private QAvatarBattleEndReason? __pbn__EndReason;

	private uint? __pbn__TaleId;

	private uint? __pbn__SiteId;

	private uint? __pbn__LobbyId;

	private uint? __pbn__BattleStartTime;

	private uint? __pbn__PreviousScore;

	private uint? __pbn__CurScore;

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

	[ProtoMember(2, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
		}
	}

	[ProtoMember(3, Name = "site_id")]
	public uint SiteId
	{
		get
		{
			return __pbn__SiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__SiteId = value;
		}
	}

	[ProtoMember(4, Name = "lobby_id")]
	public uint LobbyId
	{
		get
		{
			return __pbn__LobbyId.GetValueOrDefault();
		}
		set
		{
			__pbn__LobbyId = value;
		}
	}

	[ProtoMember(5, Name = "battle_start_time")]
	public uint BattleStartTime
	{
		get
		{
			return __pbn__BattleStartTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleStartTime = value;
		}
	}

	[ProtoMember(6, Name = "previous_score")]
	public uint PreviousScore
	{
		get
		{
			return __pbn__PreviousScore.GetValueOrDefault();
		}
		set
		{
			__pbn__PreviousScore = value;
		}
	}

	[ProtoMember(8, Name = "cur_score")]
	public uint CurScore
	{
		get
		{
			return __pbn__CurScore.GetValueOrDefault();
		}
		set
		{
			__pbn__CurScore = value;
		}
	}

	[ProtoMember(9, Name = "fight_info_list")]
	public List<QAvatarBattleFightInfo> FightInfoLists { get; } = new List<QAvatarBattleFightInfo>();


	[ProtoMember(10, Name = "get_material_list")]
	public List<Material> GetMaterialLists { get; } = new List<Material>();


	[ProtoMember(11, Name = "member_list")]
	public List<QAvatarBattleMember> MemberLists { get; } = new List<QAvatarBattleMember>();


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

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}

	public bool ShouldSerializeSiteId()
	{
		return __pbn__SiteId.HasValue;
	}

	public void ResetSiteId()
	{
		__pbn__SiteId = null;
	}

	public bool ShouldSerializeLobbyId()
	{
		return __pbn__LobbyId.HasValue;
	}

	public void ResetLobbyId()
	{
		__pbn__LobbyId = null;
	}

	public bool ShouldSerializeBattleStartTime()
	{
		return __pbn__BattleStartTime.HasValue;
	}

	public void ResetBattleStartTime()
	{
		__pbn__BattleStartTime = null;
	}

	public bool ShouldSerializePreviousScore()
	{
		return __pbn__PreviousScore.HasValue;
	}

	public void ResetPreviousScore()
	{
		__pbn__PreviousScore = null;
	}

	public bool ShouldSerializeCurScore()
	{
		return __pbn__CurScore.HasValue;
	}

	public void ResetCurScore()
	{
		__pbn__CurScore = null;
	}
}
