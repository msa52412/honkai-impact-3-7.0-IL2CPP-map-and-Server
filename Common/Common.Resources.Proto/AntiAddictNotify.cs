using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AntiAddictNotify : IExtensible
{
	[ProtoContract]
	public enum AntiAddictMsgType
	{
		[ProtoEnum(Name = "ANTI_ADDICT_MSG_ERROR")]
		AntiAddictMsgError,
		[ProtoEnum(Name = "ANTI_ADDICT_MSG_ANTI_ADDICT")]
		AntiAddictMsgAntiAddict,
		[ProtoEnum(Name = "ANTI_ADDICT_MSG_SHOULD_BIND_ACCOUNT")]
		AntiAddictMsgShouldBindAccount
	}

	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5004
	}

	private IExtension __pbn__extensionData;

	private AntiAddictMsgType? __pbn__MsgType;

	private string __pbn__Msg;

	private string __pbn__Level;

	[ProtoMember(1, Name = "msg_type")]
	[DefaultValue(AntiAddictMsgType.AntiAddictMsgError)]
	public AntiAddictMsgType MsgType
	{
		get
		{
			return __pbn__MsgType.GetValueOrDefault();
		}
		set
		{
			__pbn__MsgType = value;
		}
	}

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

	[ProtoMember(3, Name = "level")]
	[DefaultValue("")]
	public string Level
	{
		get
		{
			return __pbn__Level ?? "";
		}
		set
		{
			__pbn__Level = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMsgType()
	{
		return __pbn__MsgType.HasValue;
	}

	public void ResetMsgType()
	{
		__pbn__MsgType = null;
	}

	public bool ShouldSerializeMsg()
	{
		return __pbn__Msg != null;
	}

	public void ResetMsg()
	{
		__pbn__Msg = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level != null;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}
}
