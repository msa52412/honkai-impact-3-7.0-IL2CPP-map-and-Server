using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaSendMailReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2689
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Content;

	private string __pbn__Sender;

	[ProtoMember(1, Name = "content")]
	[DefaultValue("")]
	public string Content
	{
		get
		{
			return __pbn__Content ?? "";
		}
		set
		{
			__pbn__Content = value;
		}
	}

	[ProtoMember(2, Name = "sender")]
	[DefaultValue("")]
	public string Sender
	{
		get
		{
			return __pbn__Sender ?? "";
		}
		set
		{
			__pbn__Sender = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeContent()
	{
		return __pbn__Content != null;
	}

	public void ResetContent()
	{
		__pbn__Content = null;
	}

	public bool ShouldSerializeSender()
	{
		return __pbn__Sender != null;
	}

	public void ResetSender()
	{
		__pbn__Sender = null;
	}
}
