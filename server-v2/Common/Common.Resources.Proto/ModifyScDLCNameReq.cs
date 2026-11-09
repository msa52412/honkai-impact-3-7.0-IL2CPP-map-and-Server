using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ModifyScDLCNameReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6326
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Name;

	[ProtoMember(1, Name = "name")]
	[DefaultValue("")]
	public string Name
	{
		get
		{
			return __pbn__Name ?? "";
		}
		set
		{
			__pbn__Name = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeName()
	{
		return __pbn__Name != null;
	}

	public void ResetName()
	{
		__pbn__Name = null;
	}
}
