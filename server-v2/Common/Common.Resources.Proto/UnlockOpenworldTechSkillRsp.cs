using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UnlockOpenworldTechSkillRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4503
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "TECH_LOCK")]
		TechLock,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "STORY_LOCK")]
		StoryLock,
		[ProtoEnum(Name = "PRE_SKILL_LOCK")]
		PreSkillLock,
		[ProtoEnum(Name = "SKILL_POINT_LACK")]
		SkillPointLack,
		[ProtoEnum(Name = "ALREADA_UNLOCK")]
		AlreadaUnlock
	}

	private IExtension __pbn__extensionData;

	private int? __pbn__Param;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "param")]
	public int Param
	{
		get
		{
			return __pbn__Param.GetValueOrDefault();
		}
		set
		{
			__pbn__Param = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeParam()
	{
		return __pbn__Param.HasValue;
	}

	public void ResetParam()
	{
		__pbn__Param = null;
	}
}
