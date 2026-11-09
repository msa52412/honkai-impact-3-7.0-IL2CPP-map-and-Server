using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeExtraStoryLineStoryFinishRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 836
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LineId;

	[ProtoMember(1, Name = "line_id")]
	public uint LineId
	{
		get
		{
			return __pbn__LineId.GetValueOrDefault();
		}
		set
		{
			__pbn__LineId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLineId()
	{
		return __pbn__LineId.HasValue;
	}

	public void ResetLineId()
	{
		__pbn__LineId = null;
	}
}
[ProtoContract]
public class TakeExtraStoryLineStoryFinishRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 837
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_FINISH")]
		NotFinish,
		[ProtoEnum(Name = "HAS_TAKEN")]
		HasTaken
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LineId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "line_id")]
	public uint LineId
	{
		get
		{
			return __pbn__LineId.GetValueOrDefault();
		}
		set
		{
			__pbn__LineId = value;
		}
	}

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLineId()
	{
		return __pbn__LineId.HasValue;
	}

	public void ResetLineId()
	{
		__pbn__LineId = null;
	}
}
