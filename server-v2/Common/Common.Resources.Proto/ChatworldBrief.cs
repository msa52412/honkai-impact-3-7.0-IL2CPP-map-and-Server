using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldBrief : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatworldId;

	private uint? __pbn__PlayerNum;

	private bool? __pbn__IsHasUntakenTreasure;

	private uint? __pbn__BeastNum;

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

	[ProtoMember(2, Name = "player_num")]
	public uint PlayerNum
	{
		get
		{
			return __pbn__PlayerNum.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerNum = value;
		}
	}

	[ProtoMember(3, Name = "is_has_untaken_treasure")]
	public bool IsHasUntakenTreasure
	{
		get
		{
			return __pbn__IsHasUntakenTreasure.GetValueOrDefault();
		}
		set
		{
			__pbn__IsHasUntakenTreasure = value;
		}
	}

	[ProtoMember(4, Name = "beast_num")]
	public uint BeastNum
	{
		get
		{
			return __pbn__BeastNum.GetValueOrDefault();
		}
		set
		{
			__pbn__BeastNum = value;
		}
	}

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

	public bool ShouldSerializePlayerNum()
	{
		return __pbn__PlayerNum.HasValue;
	}

	public void ResetPlayerNum()
	{
		__pbn__PlayerNum = null;
	}

	public bool ShouldSerializeIsHasUntakenTreasure()
	{
		return __pbn__IsHasUntakenTreasure.HasValue;
	}

	public void ResetIsHasUntakenTreasure()
	{
		__pbn__IsHasUntakenTreasure = null;
	}

	public bool ShouldSerializeBeastNum()
	{
		return __pbn__BeastNum.HasValue;
	}

	public void ResetBeastNum()
	{
		__pbn__BeastNum = null;
	}
}
