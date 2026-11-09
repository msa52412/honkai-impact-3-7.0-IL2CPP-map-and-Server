using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReplayEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5402
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Key;

	[ProtoMember(1, Name = "key")]
	[DefaultValue("")]
	public string Key
	{
		get
		{
			return __pbn__Key ?? "";
		}
		set
		{
			__pbn__Key = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeKey()
	{
		return __pbn__Key != null;
	}

	public void ResetKey()
	{
		__pbn__Key = null;
	}
}
