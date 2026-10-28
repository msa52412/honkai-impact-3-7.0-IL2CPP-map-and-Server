using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarSkillDetailData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SkillId;

	[ProtoMember(1, Name = "skill_id")]
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

	[ProtoMember(2, Name = "sub_skill_list")]
	public List<AvatarSubSkillDetailData> SubSkillLists { get; } = new List<AvatarSubSkillDetailData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSkillId()
	{
		return __pbn__SkillId.HasValue;
	}

	public void ResetSkillId()
	{
		__pbn__SkillId = null;
	}
}
