using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetWikiDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1194
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "has_take_rating_reward_list")]
	public uint[] HasTakeRatingRewardLists { get; set; }

	[ProtoMember(3, Name = "has_take_activity_suit_reward_list")]
	public uint[] HasTakeActivitySuitRewardLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
