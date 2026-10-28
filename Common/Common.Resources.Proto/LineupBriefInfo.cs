using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LineupBriefInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "avatar_info_list")]
	public List<AvatarBriefInfo> AvatarInfoLists { get; } = new List<AvatarBriefInfo>();


	[ProtoMember(2, Name = "elf_info_list")]
	public List<ElfBriefInfo> ElfInfoLists { get; } = new List<ElfBriefInfo>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
