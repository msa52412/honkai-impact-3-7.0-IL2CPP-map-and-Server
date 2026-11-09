using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ClientCheckNetworkEnvReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1626
	}

	private IExtension __pbn__extensionData;

	private string __pbn__TokenStr;

	[ProtoMember(1, Name = "token_str")]
	[DefaultValue("")]
	public string TokenStr
	{
		get
		{
			return __pbn__TokenStr ?? "";
		}
		set
		{
			__pbn__TokenStr = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTokenStr()
	{
		return __pbn__TokenStr != null;
	}

	public void ResetTokenStr()
	{
		__pbn__TokenStr = null;
	}
}
