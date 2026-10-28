using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaMatrixGridPos : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__RowIndex;

	private uint? __pbn__ColIndex;

	[ProtoMember(1, Name = "row_index")]
	public uint RowIndex
	{
		get
		{
			return __pbn__RowIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__RowIndex = value;
		}
	}

	[ProtoMember(2, Name = "col_index")]
	public uint ColIndex
	{
		get
		{
			return __pbn__ColIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__ColIndex = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRowIndex()
	{
		return __pbn__RowIndex.HasValue;
	}

	public void ResetRowIndex()
	{
		__pbn__RowIndex = null;
	}

	public bool ShouldSerializeColIndex()
	{
		return __pbn__ColIndex.HasValue;
	}

	public void ResetColIndex()
	{
		__pbn__ColIndex = null;
	}
}
