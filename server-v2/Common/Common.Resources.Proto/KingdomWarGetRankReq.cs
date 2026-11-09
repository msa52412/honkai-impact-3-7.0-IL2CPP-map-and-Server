using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KingdomWarGetRankReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3058
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__KingdomId;

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
}
