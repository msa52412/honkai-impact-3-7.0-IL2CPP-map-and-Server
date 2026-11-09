using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetVipRewardDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6718
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

	private uint? __pbn__TotalPayHcoin;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "total_pay_hcoin")]
	public uint TotalPayHcoin
	{
		get
		{
			return __pbn__TotalPayHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalPayHcoin = value;
		}
	}

	[ProtoMember(3, Name = "vip_reward_list")]
	public List<VipReward> VipRewardLists { get; } = new List<VipReward>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTotalPayHcoin()
	{
		return __pbn__TotalPayHcoin.HasValue;
	}

	public void ResetTotalPayHcoin()
	{
		__pbn__TotalPayHcoin = null;
	}
}
