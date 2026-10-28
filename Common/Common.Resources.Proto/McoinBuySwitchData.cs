using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class McoinBuySwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AccountType;

	[ProtoMember(1, Name = "account_type")]
	public uint AccountType
	{
		get
		{
			return __pbn__AccountType.GetValueOrDefault();
		}
		set
		{
			__pbn__AccountType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAccountType()
	{
		return __pbn__AccountType.HasValue;
	}

	public void ResetAccountType()
	{
		__pbn__AccountType = null;
	}
}
