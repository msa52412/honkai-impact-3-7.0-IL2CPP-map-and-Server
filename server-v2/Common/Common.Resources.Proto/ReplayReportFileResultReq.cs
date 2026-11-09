using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReplayReportFileResultReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5412
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Token;

	[ProtoMember(1, Name = "token")]
	[DefaultValue("")]
	public string Token
	{
		get
		{
			return __pbn__Token ?? "";
		}
		set
		{
			__pbn__Token = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeToken()
	{
		return __pbn__Token != null;
	}

	public void ResetToken()
	{
		__pbn__Token = null;
	}
}
