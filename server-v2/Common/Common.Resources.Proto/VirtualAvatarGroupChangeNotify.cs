using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VirtualAvatarGroupChangeNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3512
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "virtual_avatar_group")]
	public VirtualAvatarGroup VirtualAvatarGroup { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
