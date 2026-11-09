using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MirageReportStageBeginRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7005
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "STAGE_ERROR")]
		StageError,
		[ProtoEnum(Name = "BATTLE_AVATAR_ERROR")]
		BattleAvatarError,
		[ProtoEnum(Name = "SUPPORT_AVATAR_ERROR")]
		SupportAvatarError,
		[ProtoEnum(Name = "CORE_SKILL_ERROR")]
		CoreSkillError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__BattleAvatarId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(3, Name = "battle_avatar_id")]
	public uint BattleAvatarId
	{
		get
		{
			return __pbn__BattleAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleAvatarId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeBattleAvatarId()
	{
		return __pbn__BattleAvatarId.HasValue;
	}

	public void ResetBattleAvatarId()
	{
		__pbn__BattleAvatarId = null;
	}
}
