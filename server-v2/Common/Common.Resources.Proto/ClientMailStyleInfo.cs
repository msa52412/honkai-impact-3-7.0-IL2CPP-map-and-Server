using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ClientMailStyleInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MailStyle;

	private uint? __pbn__UntakenNum;

	[ProtoMember(1, Name = "mail_style")]
	public uint MailStyle
	{
		get
		{
			return __pbn__MailStyle.GetValueOrDefault();
		}
		set
		{
			__pbn__MailStyle = value;
		}
	}

	[ProtoMember(2, Name = "untaken_num")]
	public uint UntakenNum
	{
		get
		{
			return __pbn__UntakenNum.GetValueOrDefault();
		}
		set
		{
			__pbn__UntakenNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMailStyle()
	{
		return __pbn__MailStyle.HasValue;
	}

	public void ResetMailStyle()
	{
		__pbn__MailStyle = null;
	}

	public bool ShouldSerializeUntakenNum()
	{
		return __pbn__UntakenNum.HasValue;
	}

	public void ResetUntakenNum()
	{
		__pbn__UntakenNum = null;
	}
}
