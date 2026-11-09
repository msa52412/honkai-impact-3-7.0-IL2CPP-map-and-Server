using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMailAttachmentReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 86
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsShowAttachment;

	[ProtoMember(1, Name = "mail_key_list")]
	public List<MailKey> MailKeyLists { get; } = new List<MailKey>();


	[ProtoMember(2, Name = "is_show_attachment")]
	public bool IsShowAttachment
	{
		get
		{
			return __pbn__IsShowAttachment.GetValueOrDefault();
		}
		set
		{
			__pbn__IsShowAttachment = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsShowAttachment()
	{
		return __pbn__IsShowAttachment.HasValue;
	}

	public void ResetIsShowAttachment()
	{
		__pbn__IsShowAttachment = null;
	}
}
