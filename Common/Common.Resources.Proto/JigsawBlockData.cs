using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class JigsawBlockData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BlockId;

	private uint? __pbn__PieceId;

	[ProtoMember(1, Name = "block_id")]
	public uint BlockId
	{
		get
		{
			return __pbn__BlockId.GetValueOrDefault();
		}
		set
		{
			__pbn__BlockId = value;
		}
	}

	[ProtoMember(2, Name = "piece_id")]
	public uint PieceId
	{
		get
		{
			return __pbn__PieceId.GetValueOrDefault();
		}
		set
		{
			__pbn__PieceId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBlockId()
	{
		return __pbn__BlockId.HasValue;
	}

	public void ResetBlockId()
	{
		__pbn__BlockId = null;
	}

	public bool ShouldSerializePieceId()
	{
		return __pbn__PieceId.HasValue;
	}

	public void ResetPieceId()
	{
		__pbn__PieceId = null;
	}
}
