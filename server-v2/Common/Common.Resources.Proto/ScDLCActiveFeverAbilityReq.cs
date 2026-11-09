using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScDLCActiveFeverAbilityReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6345
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "ability_group_id_list")]
	public uint[] AbilityGroupIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
