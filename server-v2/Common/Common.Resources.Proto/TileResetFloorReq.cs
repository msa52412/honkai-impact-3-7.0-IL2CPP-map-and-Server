using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TileResetFloorReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4912
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TowerId;

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

	[ProtoMember(2, Name = "floor_id_list")]
	public uint[] FloorIdLists { get; set; }

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
}
