using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetKingdomWarReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3050
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "join_data")]
	public KingdomWarJoinData JoinData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
