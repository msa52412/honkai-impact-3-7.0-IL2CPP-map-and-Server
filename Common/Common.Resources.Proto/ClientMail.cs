using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ClientMail : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__Title;

	private string __pbn__Content;

	private string __pbn__Sender;

	private uint? __pbn__Time;

	private bool? __pbn__IsAttachmentGot;

	private uint? __pbn__ExpireTime;

	private uint? __pbn__TemplateId;

	private bool? __pbn__IsRead;

	private uint? __pbn__MailStyle;

	private string __pbn__ImgPath;

	private bool? __pbn__IsFavorite;

	[ProtoMember(1, Name = "key")]
	public ClientMailKey Key { get; set; }

	[ProtoMember(2, Name = "title")]
	[DefaultValue("")]
	public string Title
	{
		get
		{
			return __pbn__Title ?? "";
		}
		set
		{
			__pbn__Title = value;
		}
	}

	[ProtoMember(3, Name = "content")]
	[DefaultValue("")]
	public string Content
	{
		get
		{
			return __pbn__Content ?? "";
		}
		set
		{
			__pbn__Content = value;
		}
	}

	[ProtoMember(4, Name = "sender")]
	[DefaultValue("")]
	public string Sender
	{
		get
		{
			return __pbn__Sender ?? "";
		}
		set
		{
			__pbn__Sender = value;
		}
	}

	[ProtoMember(5, Name = "time")]
	public uint Time
	{
		get
		{
			return __pbn__Time.GetValueOrDefault();
		}
		set
		{
			__pbn__Time = value;
		}
	}

	[ProtoMember(6, Name = "attachment")]
	public MailAttachment Attachment { get; set; }

	[ProtoMember(7, Name = "is_attachment_got")]
	public bool IsAttachmentGot
	{
		get
		{
			return __pbn__IsAttachmentGot.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAttachmentGot = value;
		}
	}

	[ProtoMember(8, Name = "expire_time")]
	public uint ExpireTime
	{
		get
		{
			return __pbn__ExpireTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpireTime = value;
		}
	}

	[ProtoMember(9, Name = "content_param_list")]
	public List<string> ContentParamLists { get; } = new List<string>();


	[ProtoMember(10, Name = "template_id")]
	public uint TemplateId
	{
		get
		{
			return __pbn__TemplateId.GetValueOrDefault();
		}
		set
		{
			__pbn__TemplateId = value;
		}
	}

	[ProtoMember(11, Name = "is_read")]
	public bool IsRead
	{
		get
		{
			return __pbn__IsRead.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRead = value;
		}
	}

	[ProtoMember(12, Name = "mail_style")]
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

	[ProtoMember(13, Name = "img_path")]
	[DefaultValue("")]
	public string ImgPath
	{
		get
		{
			return __pbn__ImgPath ?? "";
		}
		set
		{
			__pbn__ImgPath = value;
		}
	}

	[ProtoMember(14, Name = "is_favorite")]
	public bool IsFavorite
	{
		get
		{
			return __pbn__IsFavorite.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFavorite = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTitle()
	{
		return __pbn__Title != null;
	}

	public void ResetTitle()
	{
		__pbn__Title = null;
	}

	public bool ShouldSerializeContent()
	{
		return __pbn__Content != null;
	}

	public void ResetContent()
	{
		__pbn__Content = null;
	}

	public bool ShouldSerializeSender()
	{
		return __pbn__Sender != null;
	}

	public void ResetSender()
	{
		__pbn__Sender = null;
	}

	public bool ShouldSerializeTime()
	{
		return __pbn__Time.HasValue;
	}

	public void ResetTime()
	{
		__pbn__Time = null;
	}

	public bool ShouldSerializeIsAttachmentGot()
	{
		return __pbn__IsAttachmentGot.HasValue;
	}

	public void ResetIsAttachmentGot()
	{
		__pbn__IsAttachmentGot = null;
	}

	public bool ShouldSerializeExpireTime()
	{
		return __pbn__ExpireTime.HasValue;
	}

	public void ResetExpireTime()
	{
		__pbn__ExpireTime = null;
	}

	public bool ShouldSerializeTemplateId()
	{
		return __pbn__TemplateId.HasValue;
	}

	public void ResetTemplateId()
	{
		__pbn__TemplateId = null;
	}

	public bool ShouldSerializeIsRead()
	{
		return __pbn__IsRead.HasValue;
	}

	public void ResetIsRead()
	{
		__pbn__IsRead = null;
	}

	public bool ShouldSerializeMailStyle()
	{
		return __pbn__MailStyle.HasValue;
	}

	public void ResetMailStyle()
	{
		__pbn__MailStyle = null;
	}

	public bool ShouldSerializeImgPath()
	{
		return __pbn__ImgPath != null;
	}

	public void ResetImgPath()
	{
		__pbn__ImgPath = null;
	}

	public bool ShouldSerializeIsFavorite()
	{
		return __pbn__IsFavorite.HasValue;
	}

	public void ResetIsFavorite()
	{
		__pbn__IsFavorite = null;
	}
}
