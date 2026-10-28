using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FlopActivityFlopCardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4176
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
		[ProtoEnum(Name = "HAS_FLOP")]
		HasFlop,
		[ProtoEnum(Name = "HAS_UNLOCK")]
		HasUnlock,
		[ProtoEnum(Name = "NO_TIMES")]
		NoTimes,
		[ProtoEnum(Name = "ITEMS_LACK")]
		ItemsLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RewardIndex;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_data")]
	public FlopActivityData ActivityData { get; set; }

	[ProtoMember(3, Name = "reward_index")]
	public uint RewardIndex
	{
		get
		{
			return __pbn__RewardIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardIndex = value;
		}
	}

	[ProtoMember(4, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRewardIndex()
	{
		return __pbn__RewardIndex.HasValue;
	}

	public void ResetRewardIndex()
	{
		__pbn__RewardIndex = null;
	}
}
