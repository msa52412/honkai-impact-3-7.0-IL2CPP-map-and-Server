using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetVirtualGachaStatusReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3510
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupId;

	[ProtoMember(1, Name = "group_id")]
	public uint GroupId
	{
		get
		{
			return __pbn__GroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGroupId()
	{
		return __pbn__GroupId.HasValue;
	}

	public void ResetGroupId()
	{
		__pbn__GroupId = null;
	}
}
