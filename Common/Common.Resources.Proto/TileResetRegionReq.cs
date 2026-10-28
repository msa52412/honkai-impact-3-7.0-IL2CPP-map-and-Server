using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TileResetRegionReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4917
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TowerId;

	private uint? __pbn__FloorId;

	private uint? __pbn__ResetTag;

	[ProtoMember(1, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	[ProtoMember(2, Name = "floor_id")]
	public uint FloorId
	{
		get
		{
			return __pbn__FloorId.GetValueOrDefault();
		}
		set
		{
			__pbn__FloorId = value;
		}
	}

	[ProtoMember(3, Name = "reset_tag")]
	public uint ResetTag
	{
		get
		{
			return __pbn__ResetTag.GetValueOrDefault();
		}
		set
		{
			__pbn__ResetTag = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}

	public bool ShouldSerializeFloorId()
	{
		return __pbn__FloorId.HasValue;
	}

	public void ResetFloorId()
	{
		__pbn__FloorId = null;
	}

	public bool ShouldSerializeResetTag()
	{
		return __pbn__ResetTag.HasValue;
	}

	public void ResetResetTag()
	{
		__pbn__ResetTag = null;
	}
}
