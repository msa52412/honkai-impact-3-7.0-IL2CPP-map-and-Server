using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TowerRaidLockAvatarNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4191
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "avatar_list")]
	public List<TowerRaidAvatar> AvatarLists { get; } = new List<TowerRaidAvatar>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
