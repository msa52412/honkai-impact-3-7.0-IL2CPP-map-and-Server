using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgBroadcastNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6068
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "broadcast_list")]
	public List<SlgBattleBroadcast> BroadcastLists { get; } = new List<SlgBattleBroadcast>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
