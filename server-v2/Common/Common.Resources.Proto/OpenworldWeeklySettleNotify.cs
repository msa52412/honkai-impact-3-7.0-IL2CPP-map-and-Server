using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldWeeklySettleNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4451
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__QuestLevel;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(2, Name = "quest_level")]
	public uint QuestLevel
	{
		get
		{
			return __pbn__QuestLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__QuestLevel = value;
		}
	}

	[ProtoMember(3, Name = "reward")]
	public RewardData Reward { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeQuestLevel()
	{
		return __pbn__QuestLevel.HasValue;
	}

	public void ResetQuestLevel()
	{
		__pbn__QuestLevel = null;
	}
}
