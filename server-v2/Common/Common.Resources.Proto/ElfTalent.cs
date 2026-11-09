using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ElfTalent : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TalentId;

	private uint? __pbn__TalentLevel;

	private uint? __pbn__EquipElfId;

	[ProtoMember(1, Name = "talent_id")]
	public uint TalentId
	{
		get
		{
			return __pbn__TalentId.GetValueOrDefault();
		}
		set
		{
			__pbn__TalentId = value;
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

	[ProtoMember(3, Name = "equip_elf_id")]
	public uint EquipElfId
	{
		get
		{
			return __pbn__EquipElfId.GetValueOrDefault();
		}
		set
		{
			__pbn__EquipElfId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTalentId()
	{
		return __pbn__TalentId.HasValue;
	}

	public void ResetTalentId()
	{
		__pbn__TalentId = null;
	}

	public bool ShouldSerializeTalentLevel()
	{
		return __pbn__TalentLevel.HasValue;
	}

	public void ResetTalentLevel()
	{
		__pbn__TalentLevel = null;
	}

	public bool ShouldSerializeEquipElfId()
	{
		return __pbn__EquipElfId.HasValue;
	}

	public void ResetEquipElfId()
	{
		__pbn__EquipElfId = null;
	}
}
