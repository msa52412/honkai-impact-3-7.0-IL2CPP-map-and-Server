using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldFishJoinSpotReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2429
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__FishSpotId;

	private uint? __pbn__SeatIdx;

	[ProtoMember(1, Name = "fish_spot_id")]
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

	[ProtoMember(2, Name = "seat_idx")]
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

	[ProtoMember(3, Name = "fish_info")]
	public ChatworldFishInfo FishInfo { get; set; }

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
