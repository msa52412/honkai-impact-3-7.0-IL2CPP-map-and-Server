using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetAvatarDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 24
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
