using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMailAttachmentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 87
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsShowAttachment;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "succ_mail_key_list")]
	public List<MailKey> SuccMailKeyLists { get; } = new List<MailKey>();


	[ProtoMember(3, Name = "is_show_attachment")]
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

	[ProtoMember(4, Name = "mail_attachment")]
	public MailAttachment MailAttachment { get; set; }

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
