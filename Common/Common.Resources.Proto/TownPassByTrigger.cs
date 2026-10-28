using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownPassByTrigger : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Position;

	private uint? __pbn__SkillId;

	private uint? __pbn__BuildingUniqueId;

	[ProtoMember(1, Name = "position")]
	public uint Position
	{
		get
		{
			return __pbn__Position.GetValueOrDefault();
		}
		set
		{
			__pbn__Position = value;
		}
	}

	[ProtoMember(2, Name = "skill_id")]
	public uint SkillId
	{
		get
		{
			return __pbn__SkillId.GetValueOrDefault();
		}
		set
		{
			__pbn__SkillId = value;
		}
	}

	[ProtoMember(3, Name = "building_unique_id")]
	public uint BuildingUniqueId
	{
		get
		{
			return __pbn__BuildingUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuildingUniqueId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePosition()
	{
		return __pbn__Position.HasValue;
	}

	public void ResetPosition()
	{
		__pbn__Position = null;
	}

	public bool ShouldSerializeSkillId()
	{
		return __pbn__SkillId.HasValue;
	}

	public void ResetSkillId()
	{
		__pbn__SkillId = null;
	}

	public bool ShouldSerializeBuildingUniqueId()
	{
		return __pbn__BuildingUniqueId.HasValue;
	}

	public void ResetBuildingUniqueId()
	{
		__pbn__BuildingUniqueId = null;
	}
}
