using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DLCAvatarTalent : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TalentId;

	private uint? __pbn__Level;

	private uint? __pbn__AffixSetId;

	private uint? __pbn__WaitSelectAffixSetId;

	private bool? __pbn__IsMask;

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

	[ProtoMember(4, Name = "affix_set_id")]
	public uint AffixSetId
	{
		get
		{
			return __pbn__AffixSetId.GetValueOrDefault();
		}
		set
		{
			__pbn__AffixSetId = value;
		}
	}

	[ProtoMember(6, Name = "wait_select_affix_set_id")]
	public uint WaitSelectAffixSetId
	{
		get
		{
			return __pbn__WaitSelectAffixSetId.GetValueOrDefault();
		}
		set
		{
			__pbn__WaitSelectAffixSetId = value;
		}
	}

	[ProtoMember(7, Name = "is_mask")]
	public bool IsMask
	{
		get
		{
			return __pbn__IsMask.GetValueOrDefault();
		}
		set
		{
			__pbn__IsMask = value;
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

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeAffixSetId()
	{
		return __pbn__AffixSetId.HasValue;
	}

	public void ResetAffixSetId()
	{
		__pbn__AffixSetId = null;
	}

	public bool ShouldSerializeWaitSelectAffixSetId()
	{
		return __pbn__WaitSelectAffixSetId.HasValue;
	}

	public void ResetWaitSelectAffixSetId()
	{
		__pbn__WaitSelectAffixSetId = null;
	}

	public bool ShouldSerializeIsMask()
	{
		return __pbn__IsMask.HasValue;
	}

	public void ResetIsMask()
	{
		__pbn__IsMask = null;
	}
}
