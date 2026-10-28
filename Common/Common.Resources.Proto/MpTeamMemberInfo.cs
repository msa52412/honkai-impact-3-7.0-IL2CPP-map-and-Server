using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpTeamMemberInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "detail")]
	public PlayerDetailData Detail { get; set; }

	[ProtoMember(3, Name = "card")]
	public PlayerCardData Card { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
