using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class NicknameModifyReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 20
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Nickname;

	[ProtoMember(1, Name = "nickname")]
	[DefaultValue("")]
	public string Nickname
	{
		get
		{
			return __pbn__Nickname ?? "";
		}
		set
		{
			__pbn__Nickname = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}
}
