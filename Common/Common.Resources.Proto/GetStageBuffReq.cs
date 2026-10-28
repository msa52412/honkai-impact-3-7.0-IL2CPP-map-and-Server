using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetStageBuffReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1528
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "stage_id_list")]
	public uint[] StageIdLists { get; set; }

	[ProtoMember(2, Name = "activity_buff_id_list")]
	public uint[] ActivityBuffIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
