using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncOpenworldActivityNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4535
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "activity_data")]
	public OpenworldActivity ActivityData { get; set; }

	[ProtoMember(2, Name = "display_info")]
	public SyncOpenworldActivityDisplayInfo DisplayInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
