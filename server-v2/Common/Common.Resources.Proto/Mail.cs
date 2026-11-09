using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Mail : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private MailType? __pbn__Type;

	private string __pbn__Title;

	private string __pbn__Content;

	private string __pbn__Sender;

	private uint? __pbn__Time;

	private bool? __pbn__IsAttachmentGot;

	private uint? __pbn__ExpireTime;

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

	[ProtoMember(3, Name = "title")]
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

	[ProtoMember(4, Name = "content")]
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

	[ProtoMember(5, Name = "sender")]
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

	[ProtoMember(6, Name = "time")]
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

	[ProtoMember(7, Name = "attachment")]
	public MailAttachment Attachment { get; set; }

	[ProtoMember(8, Name = "is_attachment_got")]
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

	[ProtoMember(9, Name = "expire_time")]
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

	[ProtoMember(10, Name = "content_param_list")]
	public List<string> ContentParamLists { get; } = new List<string>();


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
}
