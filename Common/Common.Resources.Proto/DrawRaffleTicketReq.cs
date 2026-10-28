using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DrawRaffleTicketReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3896
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RaffleId;

	[ProtoMember(1, Name = "raffle_id")]
	public uint RaffleId
	{
		get
		{
			return __pbn__RaffleId.GetValueOrDefault();
		}
		set
		{
			__pbn__RaffleId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRaffleId()
	{
		return __pbn__RaffleId.HasValue;
	}

	public void ResetRaffleId()
	{
		__pbn__RaffleId = null;
	}
}
