using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VirtualTrainRoleCustomLevelUpReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3518
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupId;

	private uint? __pbn__VirtualRoleId;

	private uint? __pbn__CustomId;

	[ProtoMember(1, Name = "group_id")]
	public uint GroupId
	{
		get
		{
			return __pbn__GroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupId = value;
		}
	}

	[ProtoMember(2, Name = "virtual_role_id")]
	public uint VirtualRoleId
	{
		get
		{
			return __pbn__VirtualRoleId.GetValueOrDefault();
		}
		set
		{
			__pbn__VirtualRoleId = value;
		}
	}

	[ProtoMember(3, Name = "custom_id")]
	public uint CustomId
	{
		get
		{
			return __pbn__CustomId.GetValueOrDefault();
		}
		set
		{
			__pbn__CustomId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGroupId()
	{
		return __pbn__GroupId.HasValue;
	}

	public void ResetGroupId()
	{
		__pbn__GroupId = null;
	}

	public bool ShouldSerializeVirtualRoleId()
	{
		return __pbn__VirtualRoleId.HasValue;
	}

	public void ResetVirtualRoleId()
	{
		__pbn__VirtualRoleId = null;
	}

	public bool ShouldSerializeCustomId()
	{
		return __pbn__CustomId.HasValue;
	}

	public void ResetCustomId()
	{
		__pbn__CustomId = null;
	}
}
[ProtoContract]
public class VirtualTrainRoleCustomLevelUpRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3519
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MATERIAL_NOT_ENOUGH")]
		MaterialNotEnough,
		[ProtoEnum(Name = "LEVEL_MAX")]
		LevelMax,
		[ProtoEnum(Name = "ROLE_NOT_EXIST")]
		RoleNotExist,
		[ProtoEnum(Name = "STAGE_NOT_FINISH")]
		StageNotFinish,
		[ProtoEnum(Name = "PRE_CUSTOM_LEVEL_LACK")]
		PreCustomLevelLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupId;

	private uint? __pbn__VirtualRoleId;

	private uint? __pbn__CustomId;

	private uint? __pbn__Level;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "group_id")]
	public uint GroupId
	{
		get
		{
			return __pbn__GroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupId = value;
		}
	}

	[ProtoMember(3, Name = "virtual_role_id")]
	public uint VirtualRoleId
	{
		get
		{
			return __pbn__VirtualRoleId.GetValueOrDefault();
		}
		set
		{
			__pbn__VirtualRoleId = value;
		}
	}

	[ProtoMember(4, Name = "custom_id")]
	public uint CustomId
	{
		get
		{
			return __pbn__CustomId.GetValueOrDefault();
		}
		set
		{
			__pbn__CustomId = value;
		}
	}

	[ProtoMember(5, Name = "level")]
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

	public bool ShouldSerializeGroupId()
	{
		return __pbn__GroupId.HasValue;
	}

	public void ResetGroupId()
	{
		__pbn__GroupId = null;
	}

	public bool ShouldSerializeVirtualRoleId()
	{
		return __pbn__VirtualRoleId.HasValue;
	}

	public void ResetVirtualRoleId()
	{
		__pbn__VirtualRoleId = null;
	}

	public bool ShouldSerializeCustomId()
	{
		return __pbn__CustomId.HasValue;
	}

	public void ResetCustomId()
	{
		__pbn__CustomId = null;
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
