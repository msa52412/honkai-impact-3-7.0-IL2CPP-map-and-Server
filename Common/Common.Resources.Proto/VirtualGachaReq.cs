using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VirtualGachaReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3508
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BoxId;

	[ProtoMember(1, Name = "box_id")]
	public uint BoxId
	{
		get
		{
			return __pbn__BoxId.GetValueOrDefault();
		}
		set
		{
			__pbn__BoxId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBoxId()
	{
		return __pbn__BoxId.HasValue;
	}

	public void ResetBoxId()
	{
		__pbn__BoxId = null;
	}
}
