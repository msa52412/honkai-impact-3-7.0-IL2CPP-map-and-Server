using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeArmadaReunionRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2838
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada,
		[ProtoEnum(Name = "HAVE_TAKEN")]
		HaveTaken,
		[ProtoEnum(Name = "QUIT_SHORT")]
		QuitShort,
		[ProtoEnum(Name = "IN_CD_TIME")]
		InCdTime
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RewardId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_id")]
	public uint RewardId
	{
		get
		{
			return __pbn__RewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardId = value;
		}
	}

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(4, Name = "reward_list")]
	public List<ChatworldDishReward> RewardLists { get; } = new List<ChatworldDishReward>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRewardId()
	{
		return __pbn__RewardId.HasValue;
	}

	public void ResetRewardId()
	{
		__pbn__RewardId = null;
	}
}
