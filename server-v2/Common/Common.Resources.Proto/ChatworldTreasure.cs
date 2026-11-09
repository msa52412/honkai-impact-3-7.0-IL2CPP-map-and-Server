using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldTreasure : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatworldId;

	private uint? __pbn__TreasureId;

	private bool? __pbn__IsRewardTaken;

	[ProtoMember(1, Name = "chatworld_id")]
	public uint ChatworldId
	{
		get
		{
			return __pbn__ChatworldId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatworldId = value;
		}
	}

	[ProtoMember(2, Name = "treasure_id")]
	public uint TreasureId
	{
		get
		{
			return __pbn__TreasureId.GetValueOrDefault();
		}
		set
		{
			__pbn__TreasureId = value;
		}
	}

	[ProtoMember(3, Name = "position")]
	public ChatworldPosition Position { get; set; }

	[ProtoMember(4, Name = "is_reward_taken")]
	public bool IsRewardTaken
	{
		get
		{
			return __pbn__IsRewardTaken.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRewardTaken = value;
		}
	}

	[ProtoMember(5, Name = "open_uid_list")]
	public uint[] OpenUidLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatworldId()
	{
		return __pbn__ChatworldId.HasValue;
	}

	public void ResetChatworldId()
	{
		__pbn__ChatworldId = null;
	}

	public bool ShouldSerializeTreasureId()
	{
		return __pbn__TreasureId.HasValue;
	}

	public void ResetTreasureId()
	{
		__pbn__TreasureId = null;
	}

	public bool ShouldSerializeIsRewardTaken()
	{
		return __pbn__IsRewardTaken.HasValue;
	}

	public void ResetIsRewardTaken()
	{
		__pbn__IsRewardTaken = null;
	}
}
