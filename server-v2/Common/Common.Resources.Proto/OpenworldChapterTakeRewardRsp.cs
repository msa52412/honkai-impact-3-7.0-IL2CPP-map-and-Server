using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterTakeRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4572
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

	private uint? __pbn__MapId;

	private uint? __pbn__MaxRewardTakeNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(4, Name = "max_reward_take_num")]
	public uint MaxRewardTakeNum
	{
		get
		{
			return __pbn__MaxRewardTakeNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxRewardTakeNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeMaxRewardTakeNum()
	{
		return __pbn__MaxRewardTakeNum.HasValue;
	}

	public void ResetMaxRewardTakeNum()
	{
		__pbn__MaxRewardTakeNum = null;
	}
}
