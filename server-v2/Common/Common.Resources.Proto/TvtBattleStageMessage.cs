using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtBattleStageMessage : IExtensible
{
	[ProtoContract]
	public class AvatarHpMesssage : IExtensible
	{
		private IExtension __pbn__extensionData;

		[ProtoMember(1, Name = "avatar_hp_list")]
		public uint[] AvatarHpLists { get; set; }

		[ProtoMember(2, Name = "avatar_id_list")]
		public uint[] AvatarIdLists { get; set; }

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}
	}

	[ProtoContract]
	public class BossDamageMessage : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__ClientAddDamage;

		private uint? __pbn__ClientTotalDamage;

		private uint? __pbn__ClientSetLeftHp;

		private uint? __pbn__ServerSetLhsBossHp;

		private uint? __pbn__ServerSetRhsBossHp;

		private uint? __pbn__LhsFinalKillPlayerUid;

		private uint? __pbn__RhsFinalKillPlayerUid;

		[ProtoMember(1, Name = "client_add_damage")]
		public uint ClientAddDamage
		{
			get
			{
				return __pbn__ClientAddDamage.GetValueOrDefault();
			}
			set
			{
				__pbn__ClientAddDamage = value;
			}
		}

		[ProtoMember(2, Name = "client_total_damage")]
		public uint ClientTotalDamage
		{
			get
			{
				return __pbn__ClientTotalDamage.GetValueOrDefault();
			}
			set
			{
				__pbn__ClientTotalDamage = value;
			}
		}

		[ProtoMember(3, Name = "client_set_left_hp")]
		public uint ClientSetLeftHp
		{
			get
			{
				return __pbn__ClientSetLeftHp.GetValueOrDefault();
			}
			set
			{
				__pbn__ClientSetLeftHp = value;
			}
		}

		[ProtoMember(4, Name = "server_set_lhs_boss_hp")]
		public uint ServerSetLhsBossHp
		{
			get
			{
				return __pbn__ServerSetLhsBossHp.GetValueOrDefault();
			}
			set
			{
				__pbn__ServerSetLhsBossHp = value;
			}
		}

		[ProtoMember(5, Name = "server_set_rhs_boss_hp")]
		public uint ServerSetRhsBossHp
		{
			get
			{
				return __pbn__ServerSetRhsBossHp.GetValueOrDefault();
			}
			set
			{
				__pbn__ServerSetRhsBossHp = value;
			}
		}

		[ProtoMember(6, Name = "lhs_final_kill_player_uid")]
		public uint LhsFinalKillPlayerUid
		{
			get
			{
				return __pbn__LhsFinalKillPlayerUid.GetValueOrDefault();
			}
			set
			{
				__pbn__LhsFinalKillPlayerUid = value;
			}
		}

		[ProtoMember(7, Name = "rhs_final_kill_player_uid")]
		public uint RhsFinalKillPlayerUid
		{
			get
			{
				return __pbn__RhsFinalKillPlayerUid.GetValueOrDefault();
			}
			set
			{
				__pbn__RhsFinalKillPlayerUid = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeClientAddDamage()
		{
			return __pbn__ClientAddDamage.HasValue;
		}

		public void ResetClientAddDamage()
		{
			__pbn__ClientAddDamage = null;
		}

		public bool ShouldSerializeClientTotalDamage()
		{
			return __pbn__ClientTotalDamage.HasValue;
		}

		public void ResetClientTotalDamage()
		{
			__pbn__ClientTotalDamage = null;
		}

		public bool ShouldSerializeClientSetLeftHp()
		{
			return __pbn__ClientSetLeftHp.HasValue;
		}

		public void ResetClientSetLeftHp()
		{
			__pbn__ClientSetLeftHp = null;
		}

		public bool ShouldSerializeServerSetLhsBossHp()
		{
			return __pbn__ServerSetLhsBossHp.HasValue;
		}

		public void ResetServerSetLhsBossHp()
		{
			__pbn__ServerSetLhsBossHp = null;
		}

		public bool ShouldSerializeServerSetRhsBossHp()
		{
			return __pbn__ServerSetRhsBossHp.HasValue;
		}

		public void ResetServerSetRhsBossHp()
		{
			__pbn__ServerSetRhsBossHp = null;
		}

		public bool ShouldSerializeLhsFinalKillPlayerUid()
		{
			return __pbn__LhsFinalKillPlayerUid.HasValue;
		}

		public void ResetLhsFinalKillPlayerUid()
		{
			__pbn__LhsFinalKillPlayerUid = null;
		}

		public bool ShouldSerializeRhsFinalKillPlayerUid()
		{
			return __pbn__RhsFinalKillPlayerUid.HasValue;
		}

		public void ResetRhsFinalKillPlayerUid()
		{
			__pbn__RhsFinalKillPlayerUid = null;
		}
	}

	[ProtoContract]
	public class ClientDefineMessage : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__Type;

		[ProtoMember(1, Name = "type")]
		public uint Type
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

		[ProtoMember(2, Name = "para_uint_list")]
		public uint[] ParaUintLists { get; set; }

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
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

	[ProtoContract]
	public class ItemMessage : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__ItemId;

		private uint? __pbn__Level;

		[ProtoMember(1, Name = "item_id")]
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

		[ProtoMember(2, Name = "target_uid_list")]
		public uint[] TargetUidLists { get; set; }

		[ProtoMember(3, Name = "level")]
		public uint Level
		{
			get
			{
				return __pbn__Level.GetValueOrDefault();
			}
			set
			{
				__pbn__Level = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeItemId()
		{
			return __pbn__ItemId.HasValue;
		}

		public void ResetItemId()
		{
			__pbn__ItemId = null;
		}

		public bool ShouldSerializeLevel()
		{
			return __pbn__Level.HasValue;
		}

		public void ResetLevel()
		{
			__pbn__Level = null;
		}
	}

	[ProtoContract]
	public class PlayerMessage : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__PlayerValue;

		private uint? __pbn__Progress;

		[ProtoMember(1, Name = "player_value")]
		public uint PlayerValue
		{
			get
			{
				return __pbn__PlayerValue.GetValueOrDefault();
			}
			set
			{
				__pbn__PlayerValue = value;
			}
		}

		[ProtoMember(2, Name = "progress")]
		public uint Progress
		{
			get
			{
				return __pbn__Progress.GetValueOrDefault();
			}
			set
			{
				__pbn__Progress = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializePlayerValue()
		{
			return __pbn__PlayerValue.HasValue;
		}

		public void ResetPlayerValue()
		{
			__pbn__PlayerValue = null;
		}

		public bool ShouldSerializeProgress()
		{
			return __pbn__Progress.HasValue;
		}

		public void ResetProgress()
		{
			__pbn__Progress = null;
		}
	}

	[ProtoContract]
	public class RoundScoreMessage : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__ClientAddScore;

		private uint? __pbn__LeftTotalScore;

		private uint? __pbn__RightTotalScore;

		private uint? __pbn__LeftWinTimes;

		private uint? __pbn__RightWinTimes;

		private uint? __pbn__Round;

		[ProtoMember(1, Name = "client_add_score")]
		public uint ClientAddScore
		{
			get
			{
				return __pbn__ClientAddScore.GetValueOrDefault();
			}
			set
			{
				__pbn__ClientAddScore = value;
			}
		}

		[ProtoMember(2, Name = "left_total_score")]
		public uint LeftTotalScore
		{
			get
			{
				return __pbn__LeftTotalScore.GetValueOrDefault();
			}
			set
			{
				__pbn__LeftTotalScore = value;
			}
		}

		[ProtoMember(3, Name = "right_total_score")]
		public uint RightTotalScore
		{
			get
			{
				return __pbn__RightTotalScore.GetValueOrDefault();
			}
			set
			{
				__pbn__RightTotalScore = value;
			}
		}

		[ProtoMember(4, Name = "left_win_times")]
		public uint LeftWinTimes
		{
			get
			{
				return __pbn__LeftWinTimes.GetValueOrDefault();
			}
			set
			{
				__pbn__LeftWinTimes = value;
			}
		}

		[ProtoMember(5, Name = "right_win_times")]
		public uint RightWinTimes
		{
			get
			{
				return __pbn__RightWinTimes.GetValueOrDefault();
			}
			set
			{
				__pbn__RightWinTimes = value;
			}
		}

		[ProtoMember(6, Name = "round")]
		public uint Round
		{
			get
			{
				return __pbn__Round.GetValueOrDefault();
			}
			set
			{
				__pbn__Round = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeClientAddScore()
		{
			return __pbn__ClientAddScore.HasValue;
		}

		public void ResetClientAddScore()
		{
			__pbn__ClientAddScore = null;
		}

		public bool ShouldSerializeLeftTotalScore()
		{
			return __pbn__LeftTotalScore.HasValue;
		}

		public void ResetLeftTotalScore()
		{
			__pbn__LeftTotalScore = null;
		}

		public bool ShouldSerializeRightTotalScore()
		{
			return __pbn__RightTotalScore.HasValue;
		}

		public void ResetRightTotalScore()
		{
			__pbn__RightTotalScore = null;
		}

		public bool ShouldSerializeLeftWinTimes()
		{
			return __pbn__LeftWinTimes.HasValue;
		}

		public void ResetLeftWinTimes()
		{
			__pbn__LeftWinTimes = null;
		}

		public bool ShouldSerializeRightWinTimes()
		{
			return __pbn__RightWinTimes.HasValue;
		}

		public void ResetRightWinTimes()
		{
			__pbn__RightWinTimes = null;
		}

		public bool ShouldSerializeRound()
		{
			return __pbn__Round.HasValue;
		}

		public void ResetRound()
		{
			__pbn__Round = null;
		}
	}

	[ProtoContract]
	public class StageScoinMessage : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__ClientAddScoin;

		private uint? __pbn__ClientTotalScoin;

		[ProtoMember(1, Name = "client_add_scoin")]
		public uint ClientAddScoin
		{
			get
			{
				return __pbn__ClientAddScoin.GetValueOrDefault();
			}
			set
			{
				__pbn__ClientAddScoin = value;
			}
		}

		[ProtoMember(2, Name = "client_total_scoin")]
		public uint ClientTotalScoin
		{
			get
			{
				return __pbn__ClientTotalScoin.GetValueOrDefault();
			}
			set
			{
				__pbn__ClientTotalScoin = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeClientAddScoin()
		{
			return __pbn__ClientAddScoin.HasValue;
		}

		public void ResetClientAddScoin()
		{
			__pbn__ClientAddScoin = null;
		}

		public bool ShouldSerializeClientTotalScoin()
		{
			return __pbn__ClientTotalScoin.HasValue;
		}

		public void ResetClientTotalScoin()
		{
			__pbn__ClientTotalScoin = null;
		}
	}

	[ProtoContract]
	public enum MessageType
	{
		[ProtoEnum(Name = "MESSAGE_ITEM")]
		MessageItem = 1,
		[ProtoEnum(Name = "MESSAGE_AVATAR_HP")]
		MessageAvatarHp,
		[ProtoEnum(Name = "MESSAGE_PLAYER_WAVE")]
		MessagePlayerWave,
		[ProtoEnum(Name = "MESSAGE_BOSS_DAMAGE")]
		MessageBossDamage,
		[ProtoEnum(Name = "MESSAGE_STAGE_SCOIN")]
		MessageStageScoin,
		[ProtoEnum(Name = "MESSAGE_CLIENT_DEFINE")]
		MessageClientDefine,
		[ProtoEnum(Name = "MESSAGE_ROUND_SCORE")]
		MessageRoundScore
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MessageId;

	private uint? __pbn__FromUid;

	[ProtoMember(1, Name = "message_id")]
	public uint MessageId
	{
		get
		{
			return __pbn__MessageId.GetValueOrDefault();
		}
		set
		{
			__pbn__MessageId = value;
		}
	}

	[ProtoMember(2, Name = "from_uid")]
	public uint FromUid
	{
		get
		{
			return __pbn__FromUid.GetValueOrDefault();
		}
		set
		{
			__pbn__FromUid = value;
		}
	}

	[ProtoMember(11, Name = "item_msg")]
	public ItemMessage ItemMsg { get; set; }

	[ProtoMember(12, Name = "avatar_hp_msg")]
	public AvatarHpMesssage AvatarHpMsg { get; set; }

	[ProtoMember(13, Name = "player_msg")]
	public PlayerMessage PlayerMsg { get; set; }

	[ProtoMember(14, Name = "boss_damage_msg")]
	public BossDamageMessage BossDamageMsg { get; set; }

	[ProtoMember(15, Name = "stage_scoin_msg")]
	public StageScoinMessage StageScoinMsg { get; set; }

	[ProtoMember(16, Name = "client_define_msg")]
	public ClientDefineMessage ClientDefineMsg { get; set; }

	[ProtoMember(17, Name = "round_score_msg")]
	public RoundScoreMessage RoundScoreMsg { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMessageId()
	{
		return __pbn__MessageId.HasValue;
	}

	public void ResetMessageId()
	{
		__pbn__MessageId = null;
	}

	public bool ShouldSerializeFromUid()
	{
		return __pbn__FromUid.HasValue;
	}

	public void ResetFromUid()
	{
		__pbn__FromUid = null;
	}
}
