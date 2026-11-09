using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalWarIsolatePoint : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PointId;

	private uint? __pbn__PointStatus;

	[ProtoMember(1, Name = "point_id")]
	public uint PointId
	{
		get
		{
			return __pbn__PointId.GetValueOrDefault();
		}
		set
		{
			__pbn__PointId = value;
		}
	}

	[ProtoMember(2, Name = "point_status")]
	public uint PointStatus
	{
		get
		{
			return __pbn__PointStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__PointStatus = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePointId()
	{
		return __pbn__PointId.HasValue;
	}

	public void ResetPointId()
	{
		__pbn__PointId = null;
	}

	public bool ShouldSerializePointStatus()
	{
		return __pbn__PointStatus.HasValue;
	}

	public void ResetPointStatus()
	{
		__pbn__PointStatus = null;
	}
}
