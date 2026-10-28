using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetExtraStoryAchieveRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 237
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AchieveId;

	[ProtoMember(1, Name = "achieve_id")]
	public uint AchieveId
	{
		get
		{
			return __pbn__AchieveId.GetValueOrDefault();
		}
		set
		{
			__pbn__AchieveId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAchieveId()
	{
		return __pbn__AchieveId.HasValue;
	}

	public void ResetAchieveId()
	{
		__pbn__AchieveId = null;
	}
}
