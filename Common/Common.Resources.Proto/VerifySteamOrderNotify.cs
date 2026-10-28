using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VerifySteamOrderNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1650
	}

	private IExtension __pbn__extensionData;

	private string __pbn__OrderId;

	[ProtoMember(1, Name = "order_id")]
	[DefaultValue("")]
	public string OrderId
	{
		get
		{
			return __pbn__OrderId ?? "";
		}
		set
		{
			__pbn__OrderId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeOrderId()
	{
		return __pbn__OrderId != null;
	}

	public void ResetOrderId()
	{
		__pbn__OrderId = null;
	}
}
