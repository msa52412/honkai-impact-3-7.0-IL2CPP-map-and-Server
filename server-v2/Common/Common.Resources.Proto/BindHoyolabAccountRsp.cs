using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BindHoyolabAccountRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1659
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ALREADY_BIND")]
		AlreadyBind,
		[ProtoEnum(Name = "BIND_BY_OTHER")]
		BindByOther
	}

	private IExtension __pbn__extensionData;

	private string __pbn__HoyolabAccountUid;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "hoyolab_account_uid")]
	[DefaultValue("")]
	public string HoyolabAccountUid
	{
		get
		{
			return __pbn__HoyolabAccountUid ?? "";
		}
		set
		{
			__pbn__HoyolabAccountUid = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHoyolabAccountUid()
	{
		return __pbn__HoyolabAccountUid != null;
	}

	public void ResetHoyolabAccountUid()
	{
		__pbn__HoyolabAccountUid = null;
	}
}
