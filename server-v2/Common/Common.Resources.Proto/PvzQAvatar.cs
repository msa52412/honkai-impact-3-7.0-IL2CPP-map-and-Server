using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PvzQAvatar : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__QavatarId;

	private uint? __pbn__Level;

	private bool? __pbn__IsSpecialMaterialUnlocked;

	[ProtoMember(1, Name = "qavatar_id")]
	public uint QavatarId
	{
		get
		{
			return __pbn__QavatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__QavatarId = value;
		}
	}

	[ProtoMember(2, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(3, Name = "is_special_material_unlocked")]
	public bool IsSpecialMaterialUnlocked
	{
		get
		{
			return __pbn__IsSpecialMaterialUnlocked.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSpecialMaterialUnlocked = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeQavatarId()
	{
		return __pbn__QavatarId.HasValue;
	}

	public void ResetQavatarId()
	{
		__pbn__QavatarId = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeIsSpecialMaterialUnlocked()
	{
		return __pbn__IsSpecialMaterialUnlocked.HasValue;
	}

	public void ResetIsSpecialMaterialUnlocked()
	{
		__pbn__IsSpecialMaterialUnlocked = null;
	}
}
