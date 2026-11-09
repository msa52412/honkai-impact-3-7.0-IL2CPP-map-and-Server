using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldFishJoinSpotRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2430
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SPOT_FULL")]
		SpotFull,
		[ProtoEnum(Name = "CURRENCY_LIMIT")]
		CurrencyLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__FishSpotId;

	private uint? __pbn__SeatIdx;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "fish_spot_id")]
	public uint FishSpotId
	{
		get
		{
			return __pbn__FishSpotId.GetValueOrDefault();
		}
		set
		{
			__pbn__FishSpotId = value;
		}
	}

	[ProtoMember(3, Name = "seat_idx")]
	public uint SeatIdx
	{
		get
		{
			return __pbn__SeatIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__SeatIdx = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFishSpotId()
	{
		return __pbn__FishSpotId.HasValue;
	}

	public void ResetFishSpotId()
	{
		__pbn__FishSpotId = null;
	}

	public bool ShouldSerializeSeatIdx()
	{
		return __pbn__SeatIdx.HasValue;
	}

	public void ResetSeatIdx()
	{
		__pbn__SeatIdx = null;
	}
}
