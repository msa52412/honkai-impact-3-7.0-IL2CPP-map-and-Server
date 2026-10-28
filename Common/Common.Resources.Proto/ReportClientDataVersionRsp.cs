using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReportClientDataVersionRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 399
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ServerVersion;

	[ProtoMember(1, Name = "server_version")]
	public uint ServerVersion
	{
		get
		{
			return __pbn__ServerVersion.GetValueOrDefault();
		}
		set
		{
			__pbn__ServerVersion = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeServerVersion()
	{
		return __pbn__ServerVersion.HasValue;
	}

	public void ResetServerVersion()
	{
		__pbn__ServerVersion = null;
	}
}
