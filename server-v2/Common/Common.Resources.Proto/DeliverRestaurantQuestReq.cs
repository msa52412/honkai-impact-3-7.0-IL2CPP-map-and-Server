using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DeliverRestaurantQuestReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5870
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__QuestId;

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

	[ProtoMember(2, Name = "quest_id")]
	public uint QuestId
	{
		get
		{
			return __pbn__QuestId.GetValueOrDefault();
		}
		set
		{
			__pbn__QuestId = value;
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

	public bool ShouldSerializeQuestId()
	{
		return __pbn__QuestId.HasValue;
	}

	public void ResetQuestId()
	{
		__pbn__QuestId = null;
	}
}
