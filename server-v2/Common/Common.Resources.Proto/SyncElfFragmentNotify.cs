using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncElfFragmentNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2103
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(3, Name = "elf_fragment_list")]
	public List<ElfFragment> ElfFragmentLists { get; } = new List<ElfFragment>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
