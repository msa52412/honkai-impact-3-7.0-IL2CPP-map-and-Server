using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ConsignedRewardLog : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__Credit;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(3, Name = "credit")]
	public uint Credit
	{
		get
		{
			return __pbn__Credit.GetValueOrDefault();
		}
		set
		{
			__pbn__Credit = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeCredit()
	{
		return __pbn__Credit.HasValue;
	}

	public void ResetCredit()
	{
		__pbn__Credit = null;
	}
}
