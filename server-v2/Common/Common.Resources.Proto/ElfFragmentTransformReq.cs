using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ElfFragmentTransformReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2121
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ElfFragmentId;

	private uint? __pbn__ElfFragmentNum;

	[ProtoMember(1, Name = "elf_fragment_id")]
	public uint ElfFragmentId
	{
		get
		{
			return __pbn__ElfFragmentId.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfFragmentId = value;
		}
	}

	[ProtoMember(2, Name = "elf_fragment_num")]
	public uint ElfFragmentNum
	{
		get
		{
			return __pbn__ElfFragmentNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfFragmentNum = value;
		}
	}

	[ProtoMember(3, Name = "fragment_list")]
	public List<GenericItemNum> FragmentLists { get; } = new List<GenericItemNum>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeElfFragmentId()
	{
		return __pbn__ElfFragmentId.HasValue;
	}

	public void ResetElfFragmentId()
	{
		__pbn__ElfFragmentId = null;
	}

	public bool ShouldSerializeElfFragmentNum()
	{
		return __pbn__ElfFragmentNum.HasValue;
	}

	public void ResetElfFragmentNum()
	{
		__pbn__ElfFragmentNum = null;
	}
}
