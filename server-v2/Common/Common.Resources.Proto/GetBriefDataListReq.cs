using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetBriefDataListReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1374
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "uid_list")]
	public uint[] UidLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
