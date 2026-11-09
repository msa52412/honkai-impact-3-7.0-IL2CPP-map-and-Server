using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DevLevelChangeNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1619
	}

	private IExtension __pbn__extensionData;

	private string __pbn__DevLevelMsg;

	[ProtoMember(1, Name = "dev_level_msg")]
	[DefaultValue("")]
	public string DevLevelMsg
	{
		get
		{
			return __pbn__DevLevelMsg ?? "";
		}
		set
		{
			__pbn__DevLevelMsg = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDevLevelMsg()
	{
		return __pbn__DevLevelMsg != null;
	}

	public void ResetDevLevelMsg()
	{
		__pbn__DevLevelMsg = null;
	}
}
