using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PictureActivityShareReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4270
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private bool? __pbn__IsTakeReward;

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

	[ProtoMember(2, Name = "is_take_reward")]
	public bool IsTakeReward
	{
		get
		{
			return __pbn__IsTakeReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeReward = value;
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

	public bool ShouldSerializeIsTakeReward()
	{
		return __pbn__IsTakeReward.HasValue;
	}

	public void ResetIsTakeReward()
	{
		__pbn__IsTakeReward = null;
	}
}
