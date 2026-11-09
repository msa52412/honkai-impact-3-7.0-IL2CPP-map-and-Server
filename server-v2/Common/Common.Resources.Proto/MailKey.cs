using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MailKey : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private MailType? __pbn__Type;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "type")]
	[DefaultValue(MailType.MailInner)]
	public MailType Type
	{
		get
		{
			return __pbn__Type ?? MailType.MailInner;
		}
		set
		{
			__pbn__Type = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}
}
