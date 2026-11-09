using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LevelUpDLCAvatarTalentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3553
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "AVATAR_NOT_EXIST")]
		AvatarNotExist,
		[ProtoEnum(Name = "TARGET_LEVEL_ERROR")]
		TargetLevelError,
		[ProtoEnum(Name = "PRE_TALENT_LACK")]
		PreTalentLack,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "DLC_LEVEL_LACK")]
		DlcLevelLack,
		[ProtoEnum(Name = "PRE_AVATAR_LOCK")]
		PreAvatarLock,
		[ProtoEnum(Name = "WAIT_SELECT_AFFIX_SET_NOT_EMPTY")]
		WaitSelectAffixSetNotEmpty
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TalentId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "talent_id")]
	public uint TalentId
	{
		get
		{
			return __pbn__TalentId.GetValueOrDefault();
		}
		set
		{
			__pbn__TalentId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTalentId()
	{
		return __pbn__TalentId.HasValue;
	}

	public void ResetTalentId()
	{
		__pbn__TalentId = null;
	}
}
