using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeClientMailAttachmentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3803
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "REPEATED_REQ")]
		RepeatedReq,
		[ProtoEnum(Name = "PART_FAIL")]
		PartFail,
		[ProtoEnum(Name = "FEATURE_CLOSE")]
		FeatureClose
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsShowAttachment;

	private bool? __pbn__IsTakeAll;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "succ_mail_key_list")]
	public List<ClientMailKey> SuccMailKeyLists { get; } = new List<ClientMailKey>();


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

	[ProtoMember(5, Name = "is_take_all")]
	public bool IsTakeAll
	{
		get
		{
			return __pbn__IsTakeAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeAll = value;
		}
	}

	[ProtoMember(6, Name = "fail_mail_list")]
	public List<ClientMailAttachmentItem> FailMailLists { get; } = new List<ClientMailAttachmentItem>();


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

	public bool ShouldSerializeIsTakeAll()
	{
		return __pbn__IsTakeAll.HasValue;
	}

	public void ResetIsTakeAll()
	{
		__pbn__IsTakeAll = null;
	}
}
