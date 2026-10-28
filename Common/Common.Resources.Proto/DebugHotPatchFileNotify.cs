using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DebugHotPatchFileNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1600
	}

	private IExtension __pbn__extensionData;

	private string __pbn__FileUrl;

	[ProtoMember(1, Name = "file_url")]
	[DefaultValue("")]
	public string FileUrl
	{
		get
		{
			return __pbn__FileUrl ?? "";
		}
		set
		{
			__pbn__FileUrl = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFileUrl()
	{
		return __pbn__FileUrl != null;
	}

	public void ResetFileUrl()
	{
		__pbn__FileUrl = null;
	}
}
