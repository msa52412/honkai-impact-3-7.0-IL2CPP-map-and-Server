using System;
using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace Common.Database;

public class Avatar
{
	public static readonly SqlTable<AvatarScheme> collection = Global.Table<AvatarScheme>("Avatars", ("owner_uid", "INTEGER", x => x.OwnerUid, false), ("avatar_id", "INTEGER", x => x.AvatarId, false));

	public static AvatarScheme[] AvatarsFromUid(uint uid)
	{
		return (from collection in collection.All()
			where collection.OwnerUid == uid
			select collection).ToArray();
	}

	public static AvatarScheme Create(int avatarId, uint uid, EquipmentScheme equipment)
	{
		AvatarScheme avatarScheme = (from collection in collection.All()
			where (long)collection.AvatarId == (long)avatarId && collection.OwnerUid == uid
			select collection).FirstOrDefault();
		if (avatarScheme != null)
		{
			return avatarScheme;
		}
		AvatarDataExcel avatarDataExcel = BaseExcelReader<AvatarData, AvatarDataExcel>.GetInstance().FromId(avatarId);
		if (avatarDataExcel == null)
		{
			throw new ArgumentException("Invalid avatarId");
		}
		Weapon weapon = equipment.AddWeapon(avatarDataExcel.InitialWeapon);
		AvatarScheme avatarScheme2 = new AvatarScheme();
		avatarScheme2.OwnerUid = uid;
		avatarScheme2.AvatarId = (uint)avatarDataExcel.AvatarId;
		avatarScheme2.DressId = (uint)avatarDataExcel.DefaultDressId;
		avatarScheme2.DressLists = new uint[1] { (uint)avatarDataExcel.DefaultDressId };
		avatarScheme2.Exp = 0u;
		avatarScheme2.Fragment = 0u;
		avatarScheme2.Level = 1u;
		avatarScheme2.Star = (uint)avatarDataExcel.UnlockStar;
		avatarScheme2.StigmataUniqueId1 = 0u;
		avatarScheme2.StigmataUniqueId2 = 0u;
		avatarScheme2.StigmataUniqueId3 = 0u;
		avatarScheme2.SubStar = 0u;
		avatarScheme2.TouchGoodfeel = 0u;
		avatarScheme2.TodayHasAddGoodfeel = 0u;
		avatarScheme2.WeaponUniqueId = weapon.UniqueId;
		avatarScheme2.AvatarArtifact = null;
		avatarScheme2.SkillLists = new List<AvatarScheme.AvatarSkill>();
		AvatarScheme avatarScheme3 = avatarScheme2;
		if (avatarDataExcel.AvatarId == 101)
		{
			Stigmata stigmata = equipment.AddStigmata(30007);
			Stigmata stigmata2 = equipment.AddStigmata(30060);
			Stigmata stigmata3 = equipment.AddStigmata(30113);
			avatarScheme3.StigmataUniqueId1 = stigmata.UniqueId;
			avatarScheme3.StigmataUniqueId2 = stigmata2.UniqueId;
			avatarScheme3.StigmataUniqueId3 = stigmata3.UniqueId;
		}
		avatarScheme3.SkillLists.AddRange(avatarDataExcel.SkillList.Select((int skillId) => new AvatarScheme.AvatarSkill
		{
			SkillId = (uint)skillId
		}));
		collection.InsertOne(avatarScheme3);
		return avatarScheme3;
	}

	public static void SwitchSubSkill(int avatarId, int skillId, int subSkillId, uint uid)
	{
		AvatarScheme avatarScheme = (from collection in collection.All()
			where (long)collection.AvatarId == (long)avatarId && collection.OwnerUid == uid
			select collection).FirstOrDefault();
		if (avatarScheme == null)
		{
			throw new ArgumentException("Invalid avatarId");
		}
		avatarScheme?.SkillLists.ForEach(delegate(AvatarScheme.AvatarSkill skill)
		{
			if (skill.SkillId == skillId)
			{
				skill.SubSkillLists.ForEach(delegate(AvatarSubSkill subSkill)
				{
					if (subSkill.SubSkillId == subSkillId)
					{
						subSkill.IsMask = !subSkill.IsMask;
					}
				});
			}
		});
	}

	public static void SaveBulk(IEnumerable<AvatarScheme> avatars)
	{
		collection.BulkWrite(avatars);
	}
}
