using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InviteFriendRewardData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Level;

	private uint? __pbn__CurNum;

	private uint? __pbn__MaxNum;

	[ProtoMember(1, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(2, Name = "cur_num")]
	public uint CurNum
	{
		get
		{
			return __pbn__CurNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CurNum = value;
		}
	}

	[ProtoMember(3, Name = "max_num")]
	public uint MaxNum
	{
		get
		{
			return __pbn__MaxNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxNum = value;
		}
	}

	[ProtoMember(4, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeCurNum()
	{
		return __pbn__CurNum.HasValue;
	}

	public void ResetCurNum()
	{
		__pbn__CurNum = null;
	}

	public bool ShouldSerializeMaxNum()
	{
		return __pbn__MaxNum.HasValue;
	}

	public void ResetMaxNum()
	{
		__pbn__MaxNum = null;
	}
}
