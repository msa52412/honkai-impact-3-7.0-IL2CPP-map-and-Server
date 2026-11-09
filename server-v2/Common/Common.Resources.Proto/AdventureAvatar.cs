using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AdventureAvatar : IExtensible
{
	[ProtoContract]
	public class Skill : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__SkillId;

		private uint? __pbn__SkillLevel;

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

		[ProtoMember(2, Name = "skill_level")]
		public uint SkillLevel
		{
			get
			{
				return __pbn__SkillLevel.GetValueOrDefault();
			}
			set
			{
				__pbn__SkillLevel = value;
			}
		}

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

		public bool ShouldSerializeSkillLevel()
		{
			return __pbn__SkillLevel.HasValue;
		}

		public void ResetSkillLevel()
		{
			__pbn__SkillLevel = null;
		}
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__Star;

	[ProtoMember(1, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(2, Name = "star")]
	public uint Star
	{
		get
		{
			return __pbn__Star.GetValueOrDefault();
		}
		set
		{
			__pbn__Star = value;
		}
	}

	[ProtoMember(3, Name = "skill_list")]
	public List<Skill> SkillLists { get; } = new List<Skill>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeStar()
	{
		return __pbn__Star.HasValue;
	}

	public void ResetStar()
	{
		__pbn__Star = null;
	}
}
