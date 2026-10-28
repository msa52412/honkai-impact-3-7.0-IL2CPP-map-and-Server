using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetPlayerTagNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 802
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Uid;

	[ProtoMember(1, Name = "uid")]
	[DefaultValue("")]
	public string Uid
	{
		get
		{
			return __pbn__Uid ?? "";
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "tag_list")]
	public List<string> TagLists { get; } = new List<string>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid != null;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}
}
