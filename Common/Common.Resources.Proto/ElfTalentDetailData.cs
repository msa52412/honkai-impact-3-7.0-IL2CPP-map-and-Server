using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ElfTalentDetailData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ElfTalentId;

	private uint? __pbn__TalentLevel;

	[ProtoMember(1, Name = "elf_talent_id")]
	public uint ElfTalentId
	{
		get
		{
			return __pbn__ElfTalentId.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfTalentId = value;
		}
	}

	[ProtoMember(2, Name = "talent_level")]
	public uint TalentLevel
	{
		get
		{
			return __pbn__TalentLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__TalentLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeElfTalentId()
	{
		return __pbn__ElfTalentId.HasValue;
	}

	public void ResetElfTalentId()
	{
		__pbn__ElfTalentId = null;
	}

	public bool ShouldSerializeTalentLevel()
	{
		return __pbn__TalentLevel.HasValue;
	}

	public void ResetTalentLevel()
	{
		__pbn__TalentLevel = null;
	}
}
