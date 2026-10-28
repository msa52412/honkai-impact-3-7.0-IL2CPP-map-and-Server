using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncThemeDataNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4246
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "theme")]
	public ThemeData Theme { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
