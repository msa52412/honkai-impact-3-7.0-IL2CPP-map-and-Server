using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetExtraStoryAchieveRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 238
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACHIEVE_NOT_EXIST")]
		AchieveNotExist,
		[ProtoEnum(Name = "ACHIEVE_NOT_FINISH")]
		AchieveNotFinish,
		[ProtoEnum(Name = "ACHIEVE_HAS_TAKEN")]
		AchieveHasTaken
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AchieveId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "achieve_id")]
	public uint AchieveId
	{
		get
		{
			return __pbn__AchieveId.GetValueOrDefault();
		}
		set
		{
			__pbn__AchieveId = value;
		}
	}

	[ProtoMember(3, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAchieveId()
	{
		return __pbn__AchieveId.HasValue;
	}

	public void ResetAchieveId()
	{
		__pbn__AchieveId = null;
	}
}
