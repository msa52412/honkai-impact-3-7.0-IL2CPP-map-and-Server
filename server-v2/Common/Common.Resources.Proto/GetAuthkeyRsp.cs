using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetAuthkeyRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5011
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SIGN_TYPE_ERROR")]
		SignTypeError,
		[ProtoEnum(Name = "AUTHKEY_VER_ERROR")]
		AuthkeyVerError
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Authkey;

	private string __pbn__AuthAppid;

	private uint? __pbn__SignType;

	private uint? __pbn__AuthkeyVer;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "authkey")]
	[DefaultValue("")]
	public string Authkey
	{
		get
		{
			return __pbn__Authkey ?? "";
		}
		set
		{
			__pbn__Authkey = value;
		}
	}

	[ProtoMember(3, Name = "auth_appid")]
	[DefaultValue("")]
	public string AuthAppid
	{
		get
		{
			return __pbn__AuthAppid ?? "";
		}
		set
		{
			__pbn__AuthAppid = value;
		}
	}

	[ProtoMember(4, Name = "sign_type")]
	public uint SignType
	{
		get
		{
			return __pbn__SignType.GetValueOrDefault();
		}
		set
		{
			__pbn__SignType = value;
		}
	}

	[ProtoMember(5, Name = "authkey_ver")]
	public uint AuthkeyVer
	{
		get
		{
			return __pbn__AuthkeyVer.GetValueOrDefault();
		}
		set
		{
			__pbn__AuthkeyVer = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAuthkey()
	{
		return __pbn__Authkey != null;
	}

	public void ResetAuthkey()
	{
		__pbn__Authkey = null;
	}

	public bool ShouldSerializeAuthAppid()
	{
		return __pbn__AuthAppid != null;
	}

	public void ResetAuthAppid()
	{
		__pbn__AuthAppid = null;
	}

	public bool ShouldSerializeSignType()
	{
		return __pbn__SignType.HasValue;
	}

	public void ResetSignType()
	{
		__pbn__SignType = null;
	}

	public bool ShouldSerializeAuthkeyVer()
	{
		return __pbn__AuthkeyVer.HasValue;
	}

	public void ResetAuthkeyVer()
	{
		__pbn__AuthkeyVer = null;
	}
}
