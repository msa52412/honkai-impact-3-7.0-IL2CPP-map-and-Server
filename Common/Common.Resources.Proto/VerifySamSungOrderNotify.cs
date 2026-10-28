using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VerifySamSungOrderNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1581
	}

	private IExtension __pbn__extensionData;

	private string __pbn__PurchaseId;

	[ProtoMember(1, Name = "purchase_id")]
	[DefaultValue("")]
	public string PurchaseId
	{
		get
		{
			return __pbn__PurchaseId ?? "";
		}
		set
		{
			__pbn__PurchaseId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePurchaseId()
	{
		return __pbn__PurchaseId != null;
	}

	public void ResetPurchaseId()
	{
		__pbn__PurchaseId = null;
	}
}
