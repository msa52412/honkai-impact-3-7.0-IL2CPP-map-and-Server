using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncDLCAvatarNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3556
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "avatar_list")]
	public List<DLCAvatar> AvatarLists { get; } = new List<DLCAvatar>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
