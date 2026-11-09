using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixFloorBriefInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__FloorId;

	private MatrixFloorStatus? __pbn__FloorStatus;

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

	[ProtoMember(2, Name = "floor_status")]
	[DefaultValue(MatrixFloorStatus.MatrixFloorStatusInvalid)]
	public MatrixFloorStatus FloorStatus
	{
		get
		{
			return __pbn__FloorStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__FloorStatus = value;
		}
	}

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

	public bool ShouldSerializeFloorStatus()
	{
		return __pbn__FloorStatus.HasValue;
	}

	public void ResetFloorStatus()
	{
		__pbn__FloorStatus = null;
	}
}
