using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DelClientMailReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3806
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsOneClickDelete;

	[ProtoMember(1, Name = "mail_key")]
	public ClientMailKey MailKey { get; set; }

	[ProtoMember(2, Name = "is_one_click_delete")]
	public bool IsOneClickDelete
	{
		get
		{
			return __pbn__IsOneClickDelete.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOneClickDelete = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsOneClickDelete()
	{
		return __pbn__IsOneClickDelete.HasValue;
	}

	public void ResetIsOneClickDelete()
	{
		__pbn__IsOneClickDelete = null;
	}
}
