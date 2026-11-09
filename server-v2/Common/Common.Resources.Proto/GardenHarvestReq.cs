using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GardenHarvestReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4289
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "site_list")]
	public uint[] SiteLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
