using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetStageDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 41
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "stage_id_list")]
	public uint[] StageIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
