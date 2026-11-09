using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BbqActivityCookReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4252
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__BonusLevel;

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

	[ProtoMember(2, Name = "bonus_level")]
	public uint BonusLevel
	{
		get
		{
			return __pbn__BonusLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__BonusLevel = value;
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

	public bool ShouldSerializeBonusLevel()
	{
		return __pbn__BonusLevel.HasValue;
	}

	public void ResetBonusLevel()
	{
		__pbn__BonusLevel = null;
	}
}
