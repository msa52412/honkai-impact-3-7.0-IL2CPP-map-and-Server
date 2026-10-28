using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LoginWishTakeLoginRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1609
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__LoginDay;

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

	[ProtoMember(2, Name = "login_day")]
	public uint LoginDay
	{
		get
		{
			return __pbn__LoginDay.GetValueOrDefault();
		}
		set
		{
			__pbn__LoginDay = value;
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

	public bool ShouldSerializeLoginDay()
	{
		return __pbn__LoginDay.HasValue;
	}

	public void ResetLoginDay()
	{
		__pbn__LoginDay = null;
	}
}
