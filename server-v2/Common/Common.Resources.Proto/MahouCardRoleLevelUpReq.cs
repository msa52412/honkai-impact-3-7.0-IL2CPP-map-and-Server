using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardRoleLevelUpReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7902
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId { get; set; }

	[ProtoMember(2, Name = "role_id")]
	public uint RoleId { get; set; }

	[ProtoMember(3, Name = "target_level")]
	public uint TargetLevel { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
