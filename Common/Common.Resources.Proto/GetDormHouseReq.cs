using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetDormHouseReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 603
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "house_id_list")]
	public uint[] HouseIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
