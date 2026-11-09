using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KingdomWarKingdom : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__KingdomId;

	private uint? __pbn__PlayerNum;

	[ProtoMember(1, Name = "kingdom_id")]
	public uint KingdomId
	{
		get
		{
			return __pbn__KingdomId.GetValueOrDefault();
		}
		set
		{
			__pbn__KingdomId = value;
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeKingdomId()
	{
		return __pbn__KingdomId.HasValue;
	}

	public void ResetKingdomId()
	{
		__pbn__KingdomId = null;
	}

	public bool ShouldSerializePlayerNum()
	{
		return __pbn__PlayerNum.HasValue;
	}

	public void ResetPlayerNum()
	{
		__pbn__PlayerNum = null;
	}
}
