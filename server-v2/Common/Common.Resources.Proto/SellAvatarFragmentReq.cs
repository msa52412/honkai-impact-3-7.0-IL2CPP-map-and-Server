using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SellAvatarFragmentReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 102
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "fragment_list")]
	public List<AvatarFragment> FragmentLists { get; } = new List<AvatarFragment>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
