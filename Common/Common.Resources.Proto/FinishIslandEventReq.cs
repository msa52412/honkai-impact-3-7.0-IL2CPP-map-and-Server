using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishIslandEventReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 243
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EventId;

	private uint? __pbn__NodeNum;

	private bool? __pbn__IsGetReward;

	[ProtoMember(1, Name = "event_id")]
	public uint EventId
	{
		get
		{
			return __pbn__EventId.GetValueOrDefault();
		}
		set
		{
			__pbn__EventId = value;
		}
	}

	[ProtoMember(2, Name = "node_num")]
	public uint NodeNum
	{
		get
		{
			return __pbn__NodeNum.GetValueOrDefault();
		}
		set
		{
			__pbn__NodeNum = value;
		}
	}

	[ProtoMember(3, Name = "is_get_reward")]
	public bool IsGetReward
	{
		get
		{
			return __pbn__IsGetReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsGetReward = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEventId()
	{
		return __pbn__EventId.HasValue;
	}

	public void ResetEventId()
	{
		__pbn__EventId = null;
	}

	public bool ShouldSerializeNodeNum()
	{
		return __pbn__NodeNum.HasValue;
	}

	public void ResetNodeNum()
	{
		__pbn__NodeNum = null;
	}

	public bool ShouldSerializeIsGetReward()
	{
		return __pbn__IsGetReward.HasValue;
	}

	public void ResetIsGetReward()
	{
		__pbn__IsGetReward = null;
	}
}
