using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DreamOpenDreamReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5752
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__DreamId;

	private uint? __pbn__SelectDreamRewardId;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "dream_id")]
	public uint DreamId
	{
		get
		{
			return __pbn__DreamId.GetValueOrDefault();
		}
		set
		{
			__pbn__DreamId = value;
		}
	}

	[ProtoMember(3, Name = "select_dream_reward_id")]
	public uint SelectDreamRewardId
	{
		get
		{
			return __pbn__SelectDreamRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__SelectDreamRewardId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeDreamId()
	{
		return __pbn__DreamId.HasValue;
	}

	public void ResetDreamId()
	{
		__pbn__DreamId = null;
	}

	public bool ShouldSerializeSelectDreamRewardId()
	{
		return __pbn__SelectDreamRewardId.HasValue;
	}

	public void ResetSelectDreamRewardId()
	{
		__pbn__SelectDreamRewardId = null;
	}
}
