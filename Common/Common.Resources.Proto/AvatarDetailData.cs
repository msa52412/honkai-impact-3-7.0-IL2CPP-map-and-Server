using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarDetailData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__AvatarStar;

	private uint? __pbn__AvatarLevel;

	private uint? __pbn__DressId;

	private uint? __pbn__AvatarCombat;

	private uint? __pbn__AvatarSubStar;

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

	[ProtoMember(2, Name = "avatar_star")]
	public uint AvatarStar
	{
		get
		{
			return __pbn__AvatarStar.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarStar = value;
		}
	}

	[ProtoMember(3, Name = "avatar_level")]
	public uint AvatarLevel
	{
		get
		{
			return __pbn__AvatarLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarLevel = value;
		}
	}

	[ProtoMember(4, Name = "weapon")]
	public WeaponDetailData Weapon { get; set; }

	[ProtoMember(5, Name = "stigmata_1")]
	public StigmataDetailData Stigmata1 { get; set; }

	[ProtoMember(6, Name = "stigmata_2")]
	public StigmataDetailData Stigmata2 { get; set; }

	[ProtoMember(7, Name = "stigmata_3")]
	public StigmataDetailData Stigmata3 { get; set; }

	[ProtoMember(8, Name = "skill_list")]
	public List<AvatarSkillDetailData> SkillLists { get; } = new List<AvatarSkillDetailData>();


	[ProtoMember(9, Name = "dress_id")]
	public uint DressId
	{
		get
		{
			return __pbn__DressId.GetValueOrDefault();
		}
		set
		{
			__pbn__DressId = value;
		}
	}

	[ProtoMember(10, Name = "avatar_combat")]
	public uint AvatarCombat
	{
		get
		{
			return __pbn__AvatarCombat.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarCombat = value;
		}
	}

	[ProtoMember(12, Name = "avatar_artifact")]
	public AvatarArtifactDetail AvatarArtifact { get; set; }

	[ProtoMember(13, Name = "avatar_sub_star")]
	public uint AvatarSubStar
	{
		get
		{
			return __pbn__AvatarSubStar.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarSubStar = value;
		}
	}

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

	public bool ShouldSerializeAvatarStar()
	{
		return __pbn__AvatarStar.HasValue;
	}

	public void ResetAvatarStar()
	{
		__pbn__AvatarStar = null;
	}

	public bool ShouldSerializeAvatarLevel()
	{
		return __pbn__AvatarLevel.HasValue;
	}

	public void ResetAvatarLevel()
	{
		__pbn__AvatarLevel = null;
	}

	public bool ShouldSerializeDressId()
	{
		return __pbn__DressId.HasValue;
	}

	public void ResetDressId()
	{
		__pbn__DressId = null;
	}

	public bool ShouldSerializeAvatarCombat()
	{
		return __pbn__AvatarCombat.HasValue;
	}

	public void ResetAvatarCombat()
	{
		__pbn__AvatarCombat = null;
	}

	public bool ShouldSerializeAvatarSubStar()
	{
		return __pbn__AvatarSubStar.HasValue;
	}

	public void ResetAvatarSubStar()
	{
		__pbn__AvatarSubStar = null;
	}
}
