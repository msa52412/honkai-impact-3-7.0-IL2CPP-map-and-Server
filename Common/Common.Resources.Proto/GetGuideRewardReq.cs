using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetGuideRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 185
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GuideId;

	[ProtoMember(1, Name = "guide_id")]
	public uint GuideId
	{
		get
		{
			return __pbn__GuideId.GetValueOrDefault();
		}
		set
		{
			__pbn__GuideId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGuideId()
	{
		return __pbn__GuideId.HasValue;
	}

	public void ResetGuideId()
	{
		__pbn__GuideId = null;
	}
}
