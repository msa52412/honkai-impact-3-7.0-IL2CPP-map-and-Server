using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMedalDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 449
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "medal_id_list")]
	public uint[] MedalIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
