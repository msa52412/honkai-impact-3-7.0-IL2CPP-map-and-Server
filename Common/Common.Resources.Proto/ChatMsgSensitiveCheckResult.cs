using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatMsgSensitiveCheckResult : IExtensible
{
	private IExtension __pbn__extensionData;

	private int? __pbn__ShieldType;

	private int? __pbn__NumberCheck;

	private string __pbn__RewriteText;

	[ProtoMember(1, Name = "shield_type")]
	public int ShieldType
	{
		get
		{
			return __pbn__ShieldType.GetValueOrDefault();
		}
		set
		{
			__pbn__ShieldType = value;
		}
	}

	[ProtoMember(2, Name = "number_check")]
	public int NumberCheck
	{
		get
		{
			return __pbn__NumberCheck.GetValueOrDefault();
		}
		set
		{
			__pbn__NumberCheck = value;
		}
	}

	[ProtoMember(3, Name = "rewrite_text")]
	[DefaultValue("")]
	public string RewriteText
	{
		get
		{
			return __pbn__RewriteText ?? "";
		}
		set
		{
			__pbn__RewriteText = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeShieldType()
	{
		return __pbn__ShieldType.HasValue;
	}

	public void ResetShieldType()
	{
		__pbn__ShieldType = null;
	}

	public bool ShouldSerializeNumberCheck()
	{
		return __pbn__NumberCheck.HasValue;
	}

	public void ResetNumberCheck()
	{
		__pbn__NumberCheck = null;
	}

	public bool ShouldSerializeRewriteText()
	{
		return __pbn__RewriteText != null;
	}

	public void ResetRewriteText()
	{
		__pbn__RewriteText = null;
	}
}
