using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetPhonePendantDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1197
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "phone_pendant_id_list")]
	public uint[] PhonePendantIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
