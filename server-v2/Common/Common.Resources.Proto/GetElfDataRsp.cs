using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetElfDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2101
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsTakeCompensation;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "elf_list")]
	public List<Elf> ElfLists { get; } = new List<Elf>();


	[ProtoMember(3, Name = "elf_fragment_list")]
	public List<ElfFragment> ElfFragmentLists { get; } = new List<ElfFragment>();


	[ProtoMember(4, Name = "elf_talent_list")]
	public List<ElfTalent> ElfTalentLists { get; } = new List<ElfTalent>();


	[ProtoMember(5, Name = "is_take_compensation")]
	public bool IsTakeCompensation
	{
		get
		{
			return __pbn__IsTakeCompensation.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeCompensation = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsTakeCompensation()
	{
		return __pbn__IsTakeCompensation.HasValue;
	}

	public void ResetIsTakeCompensation()
	{
		__pbn__IsTakeCompensation = null;
	}
}
