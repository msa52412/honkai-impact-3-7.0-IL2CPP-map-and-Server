using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMpDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 341
	}

	[ProtoContract]
	public enum OpType
	{
		[ProtoEnum(Name = "INIT_DATA")]
		InitData,
		[ProtoEnum(Name = "ADD_DATA")]
		AddData,
		[ProtoEnum(Name = "UPDATE_DATA")]
		UpdateData
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

	private MpDataType? __pbn__DataType;

	private OpType? __pbn__op_type;

	private uint? __pbn__MpLevel;

	private uint? __pbn__MpExp;

	private uint? __pbn__TeamAvatarId;

	private uint? __pbn__PunishEndTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "data_type")]
	[DefaultValue(MpDataType.MpDataAll)]
	public MpDataType DataType
	{
		get
		{
			return __pbn__DataType.GetValueOrDefault();
		}
		set
		{
			__pbn__DataType = value;
		}
	}

	[ProtoMember(3)]
	[DefaultValue(OpType.InitData)]
	public OpType op_type
	{
		get
		{
			return __pbn__op_type.GetValueOrDefault();
		}
		set
		{
			__pbn__op_type = value;
		}
	}

	[ProtoMember(4, Name = "mp_level")]
	public uint MpLevel
	{
		get
		{
			return __pbn__MpLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MpLevel = value;
		}
	}

	[ProtoMember(5, Name = "mp_exp")]
	public uint MpExp
	{
		get
		{
			return __pbn__MpExp.GetValueOrDefault();
		}
		set
		{
			__pbn__MpExp = value;
		}
	}

	[ProtoMember(6, Name = "skill_point_list")]
	public List<MpSkillPoint> SkillPointLists { get; } = new List<MpSkillPoint>();


	[ProtoMember(7, Name = "skill_list")]
	public List<MpSkill> SkillLists { get; } = new List<MpSkill>();


	[ProtoMember(8, Name = "team_avatar_id")]
	public uint TeamAvatarId
	{
		get
		{
			return __pbn__TeamAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__TeamAvatarId = value;
		}
	}

	[ProtoMember(9, Name = "punish_end_time")]
	public uint PunishEndTime
	{
		get
		{
			return __pbn__PunishEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__PunishEndTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDataType()
	{
		return __pbn__DataType.HasValue;
	}

	public void ResetDataType()
	{
		__pbn__DataType = null;
	}

	public bool ShouldSerializeop_type()
	{
		return __pbn__op_type.HasValue;
	}

	public void Resetop_type()
	{
		__pbn__op_type = null;
	}

	public bool ShouldSerializeMpLevel()
	{
		return __pbn__MpLevel.HasValue;
	}

	public void ResetMpLevel()
	{
		__pbn__MpLevel = null;
	}

	public bool ShouldSerializeMpExp()
	{
		return __pbn__MpExp.HasValue;
	}

	public void ResetMpExp()
	{
		__pbn__MpExp = null;
	}

	public bool ShouldSerializeTeamAvatarId()
	{
		return __pbn__TeamAvatarId.HasValue;
	}

	public void ResetTeamAvatarId()
	{
		__pbn__TeamAvatarId = null;
	}

	public bool ShouldSerializePunishEndTime()
	{
		return __pbn__PunishEndTime.HasValue;
	}

	public void ResetPunishEndTime()
	{
		__pbn__PunishEndTime = null;
	}
}
