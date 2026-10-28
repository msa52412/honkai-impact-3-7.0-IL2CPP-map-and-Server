using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeConsignedRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 524
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada,
		[ProtoEnum(Name = "REWARD_TAKEN")]
		RewardTaken
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ReliableId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(3, Name = "reliable_id")]
	public uint ReliableId
	{
		get
		{
			return __pbn__ReliableId.GetValueOrDefault();
		}
		set
		{
			__pbn__ReliableId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReliableId()
	{
		return __pbn__ReliableId.HasValue;
	}

	public void ResetReliableId()
	{
		__pbn__ReliableId = null;
	}
}
