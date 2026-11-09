using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaBulletinNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2679
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Bulletin;

	private string __pbn__Contact;

	[ProtoMember(1, Name = "bulletin")]
	[DefaultValue("")]
	public string Bulletin
	{
		get
		{
			return __pbn__Bulletin ?? "";
		}
		set
		{
			__pbn__Bulletin = value;
		}
	}

	[ProtoMember(2, Name = "contact")]
	[DefaultValue("")]
	public string Contact
	{
		get
		{
			return __pbn__Contact ?? "";
		}
		set
		{
			__pbn__Contact = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBulletin()
	{
		return __pbn__Bulletin != null;
	}

	public void ResetBulletin()
	{
		__pbn__Bulletin = null;
	}

	public bool ShouldSerializeContact()
	{
		return __pbn__Contact != null;
	}

	public void ResetContact()
	{
		__pbn__Contact = null;
	}
}
