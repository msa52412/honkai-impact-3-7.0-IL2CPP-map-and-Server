using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarTalent : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TalentId;

	private uint? __pbn__TalentLevel;

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
}
