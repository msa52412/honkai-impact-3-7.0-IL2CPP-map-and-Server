using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleUseSkillRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6586
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SKILL_INVALID")]
		SkillInvalid,
		[ProtoEnum(Name = "SKILL_IN_CD")]
		SkillInCd,
		[ProtoEnum(Name = "SKILL_ALL_USED")]
		SkillAllUsed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SkillId;

	private uint? __pbn__RemainTimes;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "skill_id")]
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

	[ProtoMember(3, Name = "remain_times")]
	public uint RemainTimes
	{
		get
		{
			return __pbn__RemainTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__RemainTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSkillId()
	{
		return __pbn__SkillId.HasValue;
	}

	public void ResetSkillId()
	{
		__pbn__SkillId = null;
	}

	public bool ShouldSerializeRemainTimes()
	{
		return __pbn__RemainTimes.HasValue;
	}

	public void ResetRemainTimes()
	{
		__pbn__RemainTimes = null;
	}
}
