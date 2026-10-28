using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpStageGetVoteInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1037
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_TEAM")]
		NotInTeam,
		[ProtoEnum(Name = "NOT_IN_VOTING")]
		NotInVoting
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__VoteEndTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "vote_info_list")]
	public List<MpStageVoteInfo> VoteInfoLists { get; } = new List<MpStageVoteInfo>();


	[ProtoMember(3, Name = "vote_end_time")]
	public uint VoteEndTime
	{
		get
		{
			return __pbn__VoteEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__VoteEndTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeVoteEndTime()
	{
		return __pbn__VoteEndTime.HasValue;
	}

	public void ResetVoteEndTime()
	{
		__pbn__VoteEndTime = null;
	}
}
