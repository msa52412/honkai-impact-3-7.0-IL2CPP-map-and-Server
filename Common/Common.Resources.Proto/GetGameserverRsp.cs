using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetGameserverRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SERVER_STOP")]
		ServerStop,
		[ProtoEnum(Name = "VERSION_ERROR")]
		VersionError,
		[ProtoEnum(Name = "FORCE_UPDATE")]
		ForceUpdate
	}

	private IExtension __pbn__extensionData;

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
