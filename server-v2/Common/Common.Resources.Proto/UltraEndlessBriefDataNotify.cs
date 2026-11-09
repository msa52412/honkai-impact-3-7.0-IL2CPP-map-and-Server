using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UltraEndlessBriefDataNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5210
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "main_data")]
	public UltraEndlessMainData MainData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
