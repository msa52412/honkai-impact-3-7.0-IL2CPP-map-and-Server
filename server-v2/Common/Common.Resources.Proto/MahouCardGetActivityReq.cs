using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardGetActivityReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7900
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
