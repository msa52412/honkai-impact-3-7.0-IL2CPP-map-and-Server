using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Avatar : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__Star;

	private uint? __pbn__Level;

	private uint? __pbn__Exp;

	private uint? __pbn__Fragment;

	private uint? __pbn__WeaponUniqueId;

	private uint? __pbn__StigmataUniqueId1;

	private uint? __pbn__StigmataUniqueId2;

	private uint? __pbn__StigmataUniqueId3;

	private uint? __pbn__TouchGoodfeel;

	private uint? __pbn__TodayHasAddGoodfeel;

	private uint? __pbn__StageGoodfeel;

	private uint? __pbn__DressId;

	private AvatarBindEquipMode? __pbn__Mode;

	private uint? __pbn__SubStar;

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

	[ProtoMember(3, Name = "level")]
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

	[ProtoMember(4, Name = "exp")]
	public uint Exp
	{
		get
		{
			return __pbn__Exp.GetValueOrDefault();
		}
		set
		{
			__pbn__Exp = value;
		}
	}

	[ProtoMember(5, Name = "fragment")]
	public uint Fragment
	{
		get
		{
			return __pbn__Fragment.GetValueOrDefault();
		}
		set
		{
			__pbn__Fragment = value;
		}
	}

	[ProtoMember(6, Name = "weapon_unique_id")]
	public uint WeaponUniqueId
	{
		get
		{
			return __pbn__WeaponUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__WeaponUniqueId = value;
		}
	}

	[ProtoMember(7, Name = "stigmata_unique_id_1")]
	public uint StigmataUniqueId1
	{
		get
		{
			return __pbn__StigmataUniqueId1.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataUniqueId1 = value;
		}
	}

	[ProtoMember(8, Name = "stigmata_unique_id_2")]
	public uint StigmataUniqueId2
	{
		get
		{
			return __pbn__StigmataUniqueId2.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataUniqueId2 = value;
		}
	}

	[ProtoMember(9, Name = "stigmata_unique_id_3")]
	public uint StigmataUniqueId3
	{
		get
		{
			return __pbn__StigmataUniqueId3.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataUniqueId3 = value;
		}
	}

	[ProtoMember(10, Name = "skill_list")]
	public List<AvatarSkill> SkillLists { get; } = new List<AvatarSkill>();


	[ProtoMember(11, Name = "touch_goodfeel")]
	public uint TouchGoodfeel
	{
		get
		{
			return __pbn__TouchGoodfeel.GetValueOrDefault();
		}
		set
		{
			__pbn__TouchGoodfeel = value;
		}
	}

	[ProtoMember(12, Name = "today_has_add_goodfeel")]
	public uint TodayHasAddGoodfeel
	{
		get
		{
			return __pbn__TodayHasAddGoodfeel.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayHasAddGoodfeel = value;
		}
	}

	[ProtoMember(13, Name = "stage_goodfeel")]
	public uint StageGoodfeel
	{
		get
		{
			return __pbn__StageGoodfeel.GetValueOrDefault();
		}
		set
		{
			__pbn__StageGoodfeel = value;
		}
	}

	[ProtoMember(14, Name = "dress_list")]
	public uint[] DressLists { get; set; }

	[ProtoMember(15, Name = "dress_id")]
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

	[ProtoMember(17, Name = "mode")]
	[DefaultValue(AvatarBindEquipMode.AvatarBindEquipCommon)]
	public AvatarBindEquipMode Mode
	{
		get
		{
			return __pbn__Mode.GetValueOrDefault();
		}
		set
		{
			__pbn__Mode = value;
		}
	}

	[ProtoMember(18, Name = "avatar_artifact")]
	public AvatarArtifactDetail AvatarArtifact { get; set; }

	[ProtoMember(19, Name = "sub_star")]
	public uint SubStar
	{
		get
		{
			return __pbn__SubStar.GetValueOrDefault();
		}
		set
		{
			__pbn__SubStar = value;
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

	public bool ShouldSerializeStar()
	{
		return __pbn__Star.HasValue;
	}

	public void ResetStar()
	{
		__pbn__Star = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeExp()
	{
		return __pbn__Exp.HasValue;
	}

	public void ResetExp()
	{
		__pbn__Exp = null;
	}

	public bool ShouldSerializeFragment()
	{
		return __pbn__Fragment.HasValue;
	}

	public void ResetFragment()
	{
		__pbn__Fragment = null;
	}

	public bool ShouldSerializeWeaponUniqueId()
	{
		return __pbn__WeaponUniqueId.HasValue;
	}

	public void ResetWeaponUniqueId()
	{
		__pbn__WeaponUniqueId = null;
	}

	public bool ShouldSerializeStigmataUniqueId1()
	{
		return __pbn__StigmataUniqueId1.HasValue;
	}

	public void ResetStigmataUniqueId1()
	{
		__pbn__StigmataUniqueId1 = null;
	}

	public bool ShouldSerializeStigmataUniqueId2()
	{
		return __pbn__StigmataUniqueId2.HasValue;
	}

	public void ResetStigmataUniqueId2()
	{
		__pbn__StigmataUniqueId2 = null;
	}

	public bool ShouldSerializeStigmataUniqueId3()
	{
		return __pbn__StigmataUniqueId3.HasValue;
	}

	public void ResetStigmataUniqueId3()
	{
		__pbn__StigmataUniqueId3 = null;
	}

	public bool ShouldSerializeTouchGoodfeel()
	{
		return __pbn__TouchGoodfeel.HasValue;
	}

	public void ResetTouchGoodfeel()
	{
		__pbn__TouchGoodfeel = null;
	}

	public bool ShouldSerializeTodayHasAddGoodfeel()
	{
		return __pbn__TodayHasAddGoodfeel.HasValue;
	}

	public void ResetTodayHasAddGoodfeel()
	{
		__pbn__TodayHasAddGoodfeel = null;
	}

	public bool ShouldSerializeStageGoodfeel()
	{
		return __pbn__StageGoodfeel.HasValue;
	}

	public void ResetStageGoodfeel()
	{
		__pbn__StageGoodfeel = null;
	}

	public bool ShouldSerializeDressId()
	{
		return __pbn__DressId.HasValue;
	}

	public void ResetDressId()
	{
		__pbn__DressId = null;
	}

	public bool ShouldSerializeMode()
	{
		return __pbn__Mode.HasValue;
	}

	public void ResetMode()
	{
		__pbn__Mode = null;
	}

	public bool ShouldSerializeSubStar()
	{
		return __pbn__SubStar.HasValue;
	}

	public void ResetSubStar()
	{
		__pbn__SubStar = null;
	}
}
