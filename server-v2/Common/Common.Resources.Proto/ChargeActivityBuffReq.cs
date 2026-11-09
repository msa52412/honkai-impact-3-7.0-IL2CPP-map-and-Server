using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChargeActivityBuffReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1605
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityBuffId;

	[ProtoMember(1, Name = "activity_buff_id")]
	public uint ActivityBuffId
	{
		get
		{
			return __pbn__ActivityBuffId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityBuffId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityBuffId()
	{
		return __pbn__ActivityBuffId.HasValue;
	}

	public void ResetActivityBuffId()
	{
		__pbn__ActivityBuffId = null;
	}
}
