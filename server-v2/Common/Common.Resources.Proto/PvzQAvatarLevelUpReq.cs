using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PvzQAvatarLevelUpReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6556
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__QavatarId;

	private uint? __pbn__TargetLevel;

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

	[ProtoMember(2, Name = "qavatar_id")]
	public uint QavatarId
	{
		get
		{
			return __pbn__QavatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__QavatarId = value;
		}
	}

	[ProtoMember(3, Name = "target_level")]
	public uint TargetLevel
	{
		get
		{
			return __pbn__TargetLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetLevel = value;
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

	public bool ShouldSerializeQavatarId()
	{
		return __pbn__QavatarId.HasValue;
	}

	public void ResetQavatarId()
	{
		__pbn__QavatarId = null;
	}

	public bool ShouldSerializeTargetLevel()
	{
		return __pbn__TargetLevel.HasValue;
	}

	public void ResetTargetLevel()
	{
		__pbn__TargetLevel = null;
	}
}
