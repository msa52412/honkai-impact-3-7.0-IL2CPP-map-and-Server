using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixGrid : IExtensible
{
	private IExtension __pbn__extensionData;

	private MatrixGridType? __pbn__Type;

	private uint? __pbn__GridId;

	private uint? __pbn__EventId;

	[ProtoMember(1, Name = "pos")]
	public MatrixGridPos Pos { get; set; }

	[ProtoMember(2, Name = "type")]
	[DefaultValue(MatrixGridType.MatrixGridNone)]
	public MatrixGridType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(3, Name = "grid_id")]
	public uint GridId
	{
		get
		{
			return __pbn__GridId.GetValueOrDefault();
		}
		set
		{
			__pbn__GridId = value;
		}
	}

	[ProtoMember(4, Name = "event_id")]
	public uint EventId
	{
		get
		{
			return __pbn__EventId.GetValueOrDefault();
		}
		set
		{
			__pbn__EventId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeGridId()
	{
		return __pbn__GridId.HasValue;
	}

	public void ResetGridId()
	{
		__pbn__GridId = null;
	}

	public bool ShouldSerializeEventId()
	{
		return __pbn__EventId.HasValue;
	}

	public void ResetEventId()
	{
		__pbn__EventId = null;
	}
}
