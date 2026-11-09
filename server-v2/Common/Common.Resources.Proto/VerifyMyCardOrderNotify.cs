using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VerifyMyCardOrderNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6741
	}

	private IExtension __pbn__extensionData;

	private string __pbn__AuthCode;

	private string __pbn__ProductName;

	[ProtoMember(1, Name = "auth_code")]
	[DefaultValue("")]
	public string AuthCode
	{
		get
		{
			return __pbn__AuthCode ?? "";
		}
		set
		{
			__pbn__AuthCode = value;
		}
	}

	[ProtoMember(2, Name = "product_name")]
	[DefaultValue("")]
	public string ProductName
	{
		get
		{
			return __pbn__ProductName ?? "";
		}
		set
		{
			__pbn__ProductName = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAuthCode()
	{
		return __pbn__AuthCode != null;
	}

	public void ResetAuthCode()
	{
		__pbn__AuthCode = null;
	}

	public bool ShouldSerializeProductName()
	{
		return __pbn__ProductName != null;
	}

	public void ResetProductName()
	{
		__pbn__ProductName = null;
	}
}
