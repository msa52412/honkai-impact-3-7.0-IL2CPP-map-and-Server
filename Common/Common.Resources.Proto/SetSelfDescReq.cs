using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetSelfDescReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 78
	}

	private IExtension __pbn__extensionData;

	private string __pbn__SelfDesc;

	[ProtoMember(1, Name = "self_desc")]
	[DefaultValue("")]
	public string SelfDesc
	{
		get
		{
			return __pbn__SelfDesc ?? "";
		}
		set
		{
			__pbn__SelfDesc = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSelfDesc()
	{
		return __pbn__SelfDesc != null;
	}

	public void ResetSelfDesc()
	{
		__pbn__SelfDesc = null;
	}
}
