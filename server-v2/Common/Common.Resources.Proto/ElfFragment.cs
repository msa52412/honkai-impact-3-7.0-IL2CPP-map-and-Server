using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ElfFragment : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ElfId;

	private uint? __pbn__FragmentNum;

	[ProtoMember(1, Name = "elf_id")]
	public uint ElfId
	{
		get
		{
			return __pbn__ElfId.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfId = value;
		}
	}

	[ProtoMember(2, Name = "fragment_num")]
	public uint FragmentNum
	{
		get
		{
			return __pbn__FragmentNum.GetValueOrDefault();
		}
		set
		{
			__pbn__FragmentNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeElfId()
	{
		return __pbn__ElfId.HasValue;
	}

	public void ResetElfId()
	{
		__pbn__ElfId = null;
	}

	public bool ShouldSerializeFragmentNum()
	{
		return __pbn__FragmentNum.HasValue;
	}

	public void ResetFragmentNum()
	{
		__pbn__FragmentNum = null;
	}
}
