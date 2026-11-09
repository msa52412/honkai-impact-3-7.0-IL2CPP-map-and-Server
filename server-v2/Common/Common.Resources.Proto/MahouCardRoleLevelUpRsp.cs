using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardRoleLevelUpRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7903
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
		[ProtoEnum(Name = "ROLE_ERROR")]
		RoleError,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "TARGET_LEVEL_ERROR")]
		TargetLevelError,
		[ProtoEnum(Name = "REQUIRED_PASS_SITE")]
		RequiredPassSite
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId { get; set; }

	[ProtoMember(3, Name = "role_id")]
	public uint RoleId { get; set; }

	[ProtoMember(4, Name = "target_level")]
	public uint TargetLevel { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
