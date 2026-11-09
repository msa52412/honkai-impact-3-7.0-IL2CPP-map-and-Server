using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldSkillDamageJudgeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2421
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_CHATWORLD")]
		NotInChatworld,
		[ProtoEnum(Name = "HIT_NOT_IN_CHATWORLD")]
		HitNotInChatworld,
		[ProtoEnum(Name = "NO_ATTACK")]
		NoAttack,
		[ProtoEnum(Name = "EXCEED_RANGE")]
		ExceedRange,
		[ProtoEnum(Name = "CAN_NOT_HIT")]
		CanNotHit,
		[ProtoEnum(Name = "INVALID_PATH")]
		InvalidPath,
		[ProtoEnum(Name = "DAMAGE_FREE")]
		DamageFree,
		[ProtoEnum(Name = "NOT_ENEMY")]
		NotEnemy,
		[ProtoEnum(Name = "IS_DEAD")]
		IsDead
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Score;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "damage_msg")]
	public ChatworldSkillDamageMsg DamageMsg { get; set; }

	[ProtoMember(3, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}
}
