using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtUpdateLineupInfoNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3362
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	[ProtoMember(2, Name = "elf_id_list")]
	public uint[] ElfIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
