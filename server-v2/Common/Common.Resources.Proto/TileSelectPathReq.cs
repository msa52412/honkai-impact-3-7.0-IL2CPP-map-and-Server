using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TileSelectPathReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4915
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TowerId;

	private uint? __pbn__NextPathCellId;

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

	[ProtoMember(2, Name = "next_path_cell_id")]
	public uint NextPathCellId
	{
		get
		{
			return __pbn__NextPathCellId.GetValueOrDefault();
		}
		set
		{
			__pbn__NextPathCellId = value;
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

	public bool ShouldSerializeNextPathCellId()
	{
		return __pbn__NextPathCellId.HasValue;
	}

	public void ResetNextPathCellId()
	{
		__pbn__NextPathCellId = null;
	}
}
