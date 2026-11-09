using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleEndSkillRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5608
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SkillType;

	private uint? __pbn__SkillId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "skill_type")]
	public uint SkillType
	{
		get
		{
			return __pbn__SkillType.GetValueOrDefault();
		}
		set
		{
			__pbn__SkillType = value;
		}
	}

	[ProtoMember(3, Name = "skill_id")]
	public uint SkillId
	{
		get
		{
			return __pbn__SkillId.GetValueOrDefault();
		}
		set
		{
			__pbn__SkillId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSkillType()
	{
		return __pbn__SkillType.HasValue;
	}

	public void ResetSkillType()
	{
		__pbn__SkillType = null;
	}

	public bool ShouldSerializeSkillId()
	{
		return __pbn__SkillId.HasValue;
	}

	public void ResetSkillId()
	{
		__pbn__SkillId = null;
	}
}
