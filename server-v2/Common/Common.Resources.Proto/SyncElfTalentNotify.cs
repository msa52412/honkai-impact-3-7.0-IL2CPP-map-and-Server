using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncElfTalentNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2104
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(4, Name = "elf_talent_list")]
	public List<ElfTalent> ElfTalentLists { get; } = new List<ElfTalent>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
