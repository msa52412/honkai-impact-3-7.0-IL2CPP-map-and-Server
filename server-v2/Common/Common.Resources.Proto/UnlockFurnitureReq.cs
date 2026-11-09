using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UnlockFurnitureReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 680
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__FurnitureId;

	[ProtoMember(1, Name = "furniture_id")]
	public uint FurnitureId
	{
		get
		{
			return __pbn__FurnitureId.GetValueOrDefault();
		}
		set
		{
			__pbn__FurnitureId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFurnitureId()
	{
		return __pbn__FurnitureId.HasValue;
	}

	public void ResetFurnitureId()
	{
		__pbn__FurnitureId = null;
	}
}
