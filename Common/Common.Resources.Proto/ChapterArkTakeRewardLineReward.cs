using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterArkTakeRewardLineRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7958
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "chapter_id")]
	public uint ChapterId { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
[ProtoContract]
public class ChapterArkTakeRewardLineRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7959
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_REWARD")]
		NoReward
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chapter_id")]
	public uint ChapterId { get; set; }

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(4, Name = "reward_line_taken_progress")]
	public uint RewardLineTakenProgress { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
