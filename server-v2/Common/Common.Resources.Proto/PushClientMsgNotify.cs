using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PushClientMsgNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 801
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Content;

	[ProtoMember(1, Name = "platform_list")]
	public List<string> PlatformLists { get; } = new List<string>();


	[ProtoMember(2, Name = "uid_list")]
	public List<string> UidLists { get; } = new List<string>();


	[ProtoMember(3, Name = "tag_list")]
	public List<string> TagLists { get; } = new List<string>();


	[ProtoMember(4, Name = "tag_and_list")]
	public List<string> TagAndLists { get; } = new List<string>();


	[ProtoMember(5, Name = "tag_not_list")]
	public List<string> TagNotLists { get; } = new List<string>();


	[ProtoMember(6, Name = "content")]
	[DefaultValue("")]
	public string Content
	{
		get
		{
			return __pbn__Content ?? "";
		}
		set
		{
			__pbn__Content = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeContent()
	{
		return __pbn__Content != null;
	}

	public void ResetContent()
	{
		__pbn__Content = null;
	}
}
