using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReplayFrameNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5408
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "replay_frame_list")]
	public List<ReplayFrame> ReplayFrameLists { get; } = new List<ReplayFrame>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
