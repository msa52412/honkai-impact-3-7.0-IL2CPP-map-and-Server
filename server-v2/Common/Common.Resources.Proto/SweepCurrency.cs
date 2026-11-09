using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SweepCurrency : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PointId;

	private uint? __pbn__AddNum;

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

	[ProtoMember(2, Name = "add_num")]
	public uint AddNum
	{
		get
		{
			return __pbn__AddNum.GetValueOrDefault();
		}
		set
		{
			__pbn__AddNum = value;
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

	public bool ShouldSerializeAddNum()
	{
		return __pbn__AddNum.HasValue;
	}

	public void ResetAddNum()
	{
		__pbn__AddNum = null;
	}
}
