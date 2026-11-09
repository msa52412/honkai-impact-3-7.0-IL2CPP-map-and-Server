using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetConsignedRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 520
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MyCredit;

	private uint? __pbn__TotalCredit;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "required_reward_list")]
	public List<RewardData> RequiredRewardLists { get; } = new List<RewardData>();


	[ProtoMember(3, Name = "optional_reward_list")]
	public List<RewardData> OptionalRewardLists { get; } = new List<RewardData>();


	[ProtoMember(4, Name = "my_credit")]
	public uint MyCredit
	{
		get
		{
			return __pbn__MyCredit.GetValueOrDefault();
		}
		set
		{
			__pbn__MyCredit = value;
		}
	}

	[ProtoMember(5, Name = "total_credit")]
	public uint TotalCredit
	{
		get
		{
			return __pbn__TotalCredit.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalCredit = value;
		}
	}

	[ProtoMember(6, Name = "my_optional_reward_list")]
	public uint[] MyOptionalRewardLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMyCredit()
	{
		return __pbn__MyCredit.HasValue;
	}

	public void ResetMyCredit()
	{
		__pbn__MyCredit = null;
	}

	public bool ShouldSerializeTotalCredit()
	{
		return __pbn__TotalCredit.HasValue;
	}

	public void ResetTotalCredit()
	{
		__pbn__TotalCredit = null;
	}
}
