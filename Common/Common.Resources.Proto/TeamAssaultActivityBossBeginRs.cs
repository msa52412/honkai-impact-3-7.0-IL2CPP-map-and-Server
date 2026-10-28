using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TeamAssaultActivityBossBeginRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4025
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "ORDER_ERROR")]
		OrderError,
		[ProtoEnum(Name = "AVATAR_ERROR")]
		AvatarError,
		[ProtoEnum(Name = "AVATAR_RESTRICT")]
		AvatarRestrict
	}

	private IExtension __pbn__extensionData;

	private string __pbn__ClientData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "client_data")]
	[DefaultValue("")]
	public string ClientData
	{
		get
		{
			return __pbn__ClientData ?? "";
		}
		set
		{
			__pbn__ClientData = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeClientData()
	{
		return __pbn__ClientData != null;
	}

	public void ResetClientData()
	{
		__pbn__ClientData = null;
	}
}
