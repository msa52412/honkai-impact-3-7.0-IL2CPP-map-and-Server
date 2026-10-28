using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtCardTransformMaterialReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3317
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "material_id_list")]
	public uint[] MaterialIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
