using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpStageMemberVoteNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1038
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "vote_info_list")]
	public List<MpStageVoteInfo> VoteInfoLists { get; } = new List<MpStageVoteInfo>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
