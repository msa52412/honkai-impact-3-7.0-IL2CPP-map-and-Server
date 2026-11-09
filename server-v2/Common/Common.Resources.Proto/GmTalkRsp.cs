using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GmTalkRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 23
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

	private string __pbn__Msg;

	private string __pbn__Retmsg;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "msg")]
	[DefaultValue("")]
	public string Msg
	{
		get
		{
			return __pbn__Msg ?? "";
		}
		set
		{
			__pbn__Msg = value;
		}
	}

	[ProtoMember(3, Name = "retmsg")]
	[DefaultValue("")]
	public string Retmsg
	{
		get
		{
			return __pbn__Retmsg ?? "";
		}
		set
		{
			__pbn__Retmsg = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMsg()
	{
		return __pbn__Msg != null;
	}

	public void ResetMsg()
	{
		__pbn__Msg = null;
	}

	public bool ShouldSerializeRetmsg()
	{
		return __pbn__Retmsg != null;
	}

	public void ResetRetmsg()
	{
		__pbn__Retmsg = null;
	}
}
