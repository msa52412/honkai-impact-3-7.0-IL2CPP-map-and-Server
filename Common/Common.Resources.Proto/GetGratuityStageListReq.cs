using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetGratuityStageListReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4116
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "stage_unique_id_list")]
	public uint[] StageUniqueIdLists { get; set; }

	[ProtoMember(2, Name = "stage_group_list")]
	public uint[] StageGroupLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
