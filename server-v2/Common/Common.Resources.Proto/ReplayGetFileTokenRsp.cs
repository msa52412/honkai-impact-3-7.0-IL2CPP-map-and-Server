using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReplayGetFileTokenRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5411
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LIMIT")]
		Limit,
		[ProtoEnum(Name = "INVALID_PARAM")]
		InvalidParam,
		[ProtoEnum(Name = "HAS_UPLOAD")]
		HasUpload,
		[ProtoEnum(Name = "HAS_DELETE")]
		HasDelete,
		[ProtoEnum(Name = "DAILY_LIMIT")]
		DailyLimit
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Token;

	private string __pbn__TokenUrl;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "token")]
	[DefaultValue("")]
	public string Token
	{
		get
		{
			return __pbn__Token ?? "";
		}
		set
		{
			__pbn__Token = value;
		}
	}

	[ProtoMember(3, Name = "token_url")]
	[DefaultValue("")]
	public string TokenUrl
	{
		get
		{
			return __pbn__TokenUrl ?? "";
		}
		set
		{
			__pbn__TokenUrl = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeToken()
	{
		return __pbn__Token != null;
	}

	public void ResetToken()
	{
		__pbn__Token = null;
	}

	public bool ShouldSerializeTokenUrl()
	{
		return __pbn__TokenUrl != null;
	}

	public void ResetTokenUrl()
	{
		__pbn__TokenUrl = null;
	}
}
