using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReportClientDataVersionReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 398
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Version;

	[ProtoMember(1, Name = "version")]
	public uint Version
	{
		get
		{
			return __pbn__Version.GetValueOrDefault();
		}
		set
		{
			__pbn__Version = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeVersion()
	{
		return __pbn__Version.HasValue;
	}

	public void ResetVersion()
	{
		__pbn__Version = null;
	}
}
