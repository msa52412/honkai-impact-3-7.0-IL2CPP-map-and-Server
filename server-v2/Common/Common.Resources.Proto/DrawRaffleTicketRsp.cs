using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DrawRaffleTicketRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3897
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 0,
		[ProtoEnum(Name = "FAIL")]
		Fail = 1,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen = 3,
		[ProtoEnum(Name = "CANNOT_DRAW")]
		CannotDraw = 4,
		[ProtoEnum(Name = "TIMES_LIMIT")]
		TimesLimit = 5,
		[ProtoEnum(Name = "MATERIAL_NOT_ENOUGH")]
		MaterialNotEnough = 6,
		[ProtoEnum(Name = "TOO_FREQUENT")]
		TooFrequent = 7
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RaffleId;

	private uint? __pbn__Ticket;

	private uint? __pbn__DrawIdx;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "raffle_id")]
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

	[ProtoMember(3, Name = "ticket")]
	public uint Ticket
	{
		get
		{
			return __pbn__Ticket.GetValueOrDefault();
		}
		set
		{
			__pbn__Ticket = value;
		}
	}

	[ProtoMember(4, Name = "draw_idx")]
	public uint DrawIdx
	{
		get
		{
			return __pbn__DrawIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__DrawIdx = value;
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

	public bool ShouldSerializeTicket()
	{
		return __pbn__Ticket.HasValue;
	}

	public void ResetTicket()
	{
		__pbn__Ticket = null;
	}

	public bool ShouldSerializeDrawIdx()
	{
		return __pbn__DrawIdx.HasValue;
	}

	public void ResetDrawIdx()
	{
		__pbn__DrawIdx = null;
	}
}
