using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncMatrixFloorNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2328
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__FloorId;

	[ProtoMember(1, Name = "floor_id")]
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

	[ProtoMember(2, Name = "floor_brief_info")]
	public MatrixFloorBriefInfo FloorBriefInfo { get; set; }

	[ProtoMember(3, Name = "floor_map_info")]
	public MatrixFloorMapInfo FloorMapInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFloorId()
	{
		return __pbn__FloorId.HasValue;
	}

	public void ResetFloorId()
	{
		__pbn__FloorId = null;
	}
}
