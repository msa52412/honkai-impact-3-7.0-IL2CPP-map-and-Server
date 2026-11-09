using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetGratuityActivityReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4100
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "stage_unique_id_list")]
	public uint[] StageUniqueIdLists { get; set; }

	[ProtoMember(2, Name = "invite_stage_unique_id_list")]
	public uint[] InviteStageUniqueIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
