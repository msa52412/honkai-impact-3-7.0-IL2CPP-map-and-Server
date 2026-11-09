using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeRewardLineActivityRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5893
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "CLOSED")]
		Closed,
		[ProtoEnum(Name = "NO_REWARD")]
		NoReward,
		[ProtoEnum(Name = "ACTIVITY_NOT_EXIST")]
		ActivityNotExist
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_data_list")]
	public List<RewardData> RewardDataLists { get; } = new List<RewardData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
