using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AiCyberSetBlessReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6656
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Bless;

	[ProtoMember(1, Name = "bless")]
	[DefaultValue("")]
	public string Bless
	{
		get
		{
			return __pbn__Bless ?? "";
		}
		set
		{
			__pbn__Bless = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBless()
	{
		return __pbn__Bless != null;
	}

	public void ResetBless()
	{
		__pbn__Bless = null;
	}
}
