using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class JiguangUpdateTagReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1590
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "add_tag_list")]
	public List<JiguangClientTag> AddTagLists { get; } = new List<JiguangClientTag>();


	[ProtoMember(2, Name = "del_tag_list")]
	public List<JiguangClientTag> DelTagLists { get; } = new List<JiguangClientTag>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
