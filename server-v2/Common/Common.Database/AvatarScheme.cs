using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace Common.Database;

public class AvatarScheme : Common.Resources.Proto.Avatar, IRow
{
	public class AvatarSkill : Common.Resources.Proto.AvatarSkill
	{
		public new List<AvatarSubSkill> SubSkillLists { get; set; } = new List<AvatarSubSkill>();

	}

	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	public new List<AvatarSkill> SkillLists { get; set; } = new List<AvatarSkill>();


	public AvatarScheme SwitchSubSkill(uint skillId, uint subSkillId)
	{
		SkillLists.ForEach(delegate(AvatarSkill skill)
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
		Save();
		return this;
	}

	public void Save()
	{
		Avatar.collection.ReplaceOne(this);
	}


	public PlayerLevelData.LevelData AddExp(uint exp)
	{
		PlayerLevelData.LevelData result = BaseExcelReader<AvatarLevelData, AvatarLevelDataExcel>.GetInstance().CalculateLevel((int)base.Level, (int)(exp + base.Exp));
		base.Level = (uint)result.Level;
		base.Exp = (uint)result.Exp;
		return result;
	}

	public void StarUp()
	{
		AvatarDataExcel avatarDataExcel = BaseExcelReader<AvatarData, AvatarDataExcel>.GetInstance().FromId((int)base.AvatarId);
		if (avatarDataExcel != null)
		{
			AvatarStarType.StarInfo nextStar = BaseExcelReader<AvatarStarType, AvatarStarTypeExcel>.GetInstance().GetNextStar(avatarDataExcel.AvatarType, avatarDataExcel.AvatarStarUpType, new AvatarStarType.StarInfo((int)base.Star, (int)base.SubStar));
			base.SubStar = (uint)nextStar.SubStar;
			base.Star = (uint)nextStar.Star;
			base.Fragment -= (uint)nextStar.Cost;
		}
	}

	public AvatarDetailData ToDetailData(EquipmentScheme equipment)
	{
		Weapon weapon = equipment.WeaponList.Where((Weapon x) => x.UniqueId == base.WeaponUniqueId).FirstOrDefault();
		Stigmata stigmata = equipment.StigmataList.Where((Stigmata x) => x.UniqueId == base.StigmataUniqueId1).FirstOrDefault();
		Stigmata stigmata2 = equipment.StigmataList.Where((Stigmata x) => x.UniqueId == base.StigmataUniqueId2).FirstOrDefault();
		Stigmata stigmata3 = equipment.StigmataList.Where((Stigmata x) => x.UniqueId == base.StigmataUniqueId3).FirstOrDefault();
		AvatarDetailData avatarDetailData = new AvatarDetailData
		{
			AvatarId = base.AvatarId,
			AvatarArtifact = base.AvatarArtifact,
			AvatarLevel = base.Level,
			AvatarStar = base.Star,
			AvatarSubStar = base.SubStar,
			DressId = base.DressId
		};
		if (weapon != null)
		{
			avatarDetailData.Weapon = new WeaponDetailData
			{
				Id = weapon.Id,
				Level = weapon.Level,
				UniqueId = weapon.UniqueId,
				SubWeaponId = weapon.SubWeaponId
			};
		}
		if (stigmata != null)
		{
			avatarDetailData.Stigmata1 = new StigmataDetailData
			{
				Id = stigmata.Id,
				Level = stigmata.Level,
				UniqueId = stigmata.UniqueId
			};
			avatarDetailData.Stigmata1.RuneLists.AddRange(stigmata.RuneLists);
		}
		if (stigmata2 != null)
		{
			avatarDetailData.Stigmata2 = new StigmataDetailData
			{
				Id = stigmata2.Id,
				Level = stigmata2.Level,
				UniqueId = stigmata2.UniqueId
			};
			avatarDetailData.Stigmata2.RuneLists.AddRange(stigmata2.RuneLists);
		}
		if (stigmata3 != null)
		{
			avatarDetailData.Stigmata3 = new StigmataDetailData
			{
				Id = stigmata3.Id,
				Level = stigmata3.Level,
				UniqueId = stigmata3.UniqueId
			};
			avatarDetailData.Stigmata3.RuneLists.AddRange(stigmata3.RuneLists);
		}
		avatarDetailData.SkillLists.AddRange(SkillLists.Select(delegate(AvatarSkill x)
		{
			AvatarSkillDetailData avatarSkillDetailData = new AvatarSkillDetailData
			{
				SkillId = x.SkillId
			};
			avatarSkillDetailData.SubSkillLists.AddRange(x.SubSkillLists.Select((AvatarSubSkill x) => new AvatarSubSkillDetailData
			{
				IsMask = x.IsMask,
				Level = x.Level,
				SubSkillId = x.SubSkillId
			}));
			return avatarSkillDetailData;
		}));
		return avatarDetailData;
	}

	public void AddFragment(uint num)
	{
		base.Fragment += num;
	}

	public void SetDress(uint dressId)
	{
		base.DressId = dressId;
	}

	public void LevelUpSkill(uint subSkillId, bool isLevelUpAll = false)
	{
		AvatarSubSkillDataExcel subSkillData = BaseExcelReader<AvatarSubSkillData, AvatarSubSkillDataExcel>.GetInstance().FromId((int)subSkillId);
		if (subSkillData == null)
		{
			return;
		}
		AvatarSkill avatarSkill = null;
		if (subSkillData.ArtifactSkillId != 0)
		{
			avatarSkill = SkillLists.Where((AvatarSkill skill) => skill.SkillId == subSkillData.ArtifactSkillId).FirstOrDefault();
		}
		AvatarSkill avatarSkill2 = SkillLists.Where((AvatarSkill skill) => skill.SkillId == subSkillData.SkillId).FirstOrDefault();
		if (avatarSkill2 != null)
		{
			AvatarSubSkill avatarSubSkill = avatarSkill2.SubSkillLists.FirstOrDefault((AvatarSubSkill skill) => skill.SubSkillId == subSkillData.AvatarSubSkillId);
			if (avatarSubSkill != null)
			{
				if (isLevelUpAll)
				{
					avatarSubSkill.Level = (uint)subSkillData.MaxLv;
					if (avatarSkill != null)
					{
						AvatarSubSkill avatarSubSkill2 = avatarSkill.SubSkillLists.Where((AvatarSubSkill subskill) => subskill.SubSkillId == subSkillId).FirstOrDefault();
						if (avatarSubSkill2 != null)
						{
							avatarSubSkill2.Level = avatarSubSkill.Level;
						}
					}
				}
				else
				{
					if (avatarSubSkill.Level >= subSkillData.MaxLv)
					{
						return;
					}
					avatarSubSkill.Level++;
					if (avatarSkill != null)
					{
						AvatarSubSkill avatarSubSkill3 = avatarSkill.SubSkillLists.Where((AvatarSubSkill subskill) => subskill.SubSkillId == subSkillId).FirstOrDefault();
						if (avatarSubSkill3 != null)
						{
							avatarSubSkill3.Level = avatarSubSkill.Level;
						}
					}
				}
			}
			else
			{
				AvatarSubSkill item = new AvatarSubSkill
				{
					SubSkillId = (uint)subSkillData.AvatarSubSkillId,
					Level = 1u,
					IsMask = false
				};
				avatarSkill2.SubSkillLists.Add(item);
				avatarSkill?.SubSkillLists.Add(item);
			}
		}
		else
		{
			AvatarSkill avatarSkill3 = new AvatarSkill
			{
				SkillId = (uint)subSkillData.SkillId
			};
			avatarSkill3.SubSkillLists.Add(new AvatarSubSkill
			{
				SubSkillId = (uint)subSkillData.AvatarSubSkillId,
				Level = 1u,
				IsMask = false
			});
			SkillLists.Add(avatarSkill3);
		}
	}

	public int ArtifactUnlock(uint avatarId, uint artifactId)
	{
		if (base.AvatarArtifact == null)
		{
			AvatarArtifactDataExcel avatarArtifactDataExcel = BaseExcelReader<AvatarArtifactData, AvatarArtifactDataExcel>.GetInstance().FromId((int)artifactId);
			if (avatarArtifactDataExcel != null)
			{
				if (base.Level < avatarArtifactDataExcel.UnlockAvatarLevel)
				{
					return 1;
				}
				base.AvatarArtifact = new AvatarArtifactDetail
				{
					ArtifactId = artifactId,
					ArtifactLevel = 1u,
					IsArtifactSwitchOn = true
				};
				ReplaceSkill[] replaceSkillList = avatarArtifactDataExcel.ReplaceSkillList;
				foreach (ReplaceSkill replace in replaceSkillList)
				{
					AvatarSkill avatarSkill = new AvatarSkill
					{
						SkillId = (uint)replace.NewSkillId
					};
					AvatarSkill avatarSkill2 = SkillLists.Where((AvatarSkill skill) => skill.SkillId == replace.OldSkillId).FirstOrDefault();
					if (avatarSkill2 != null)
					{
						foreach (AvatarSubSkill subSkillList in avatarSkill2.SubSkillLists)
						{
							AvatarSubSkillDataExcel avatarSubSkillDataExcel = BaseExcelReader<AvatarSubSkillData, AvatarSubSkillDataExcel>.GetInstance().FromId((int)subSkillList.SubSkillId);
							if (avatarSubSkillDataExcel != null && avatarSubSkillDataExcel.ArtifactSkillId == replace.NewSkillId)
							{
								avatarSkill.SubSkillLists.Add(subSkillList);
							}
						}
						AvatarSubSkillDataExcel[] all = BaseExcelReader<AvatarSubSkillData, AvatarSubSkillDataExcel>.GetInstance().All;
						foreach (AvatarSubSkillDataExcel avatarSubSkillDataExcel2 in all)
						{
							if (avatarSubSkillDataExcel2.ArtifactSkillId != replace.NewSkillId)
							{
								continue;
							}
							UpLevelArtifactNeedList[] upLevelArtifactNeedList = avatarSubSkillDataExcel2.UpLevelArtifactNeedList;
							foreach (UpLevelArtifactNeedList upLevelArtifactNeedList2 in upLevelArtifactNeedList)
							{
								if (upLevelArtifactNeedList2.ArtifactLevel == 1)
								{
									avatarSkill.SubSkillLists.Add(new AvatarSubSkill
									{
										SubSkillId = (uint)avatarSubSkillDataExcel2.AvatarSubSkillId,
										Level = (uint)upLevelArtifactNeedList2.SubSkillLevel
									});
								}
							}
						}
					}
					if (SkillLists.Where((AvatarSkill skill) => skill.SkillId == (uint)replace.NewSkillId).FirstOrDefault() == null)
					{
						SkillLists.Add(avatarSkill);
					}
				}
			}
		}
		return 0;
	}

	public void ArtifactSwitch(AvatarArtifactSwitch aas)
	{
		if (base.AvatarId == aas.AvatarId)
		{
			base.AvatarArtifact.IsArtifactSwitchOn = aas.IsArtifactSwitchOn;
		}
	}

	public int ArtifactLevelUp(uint avatarId, uint artifactId)
	{
		if (base.AvatarArtifact != null && base.AvatarArtifact.ArtifactLevel != 6)
		{
			AvatarArtifactDataExcel avatarArtifactDataExcel = BaseExcelReader<AvatarArtifactData, AvatarArtifactDataExcel>.GetInstance().FromId((int)artifactId);
			if (avatarArtifactDataExcel != null)
			{
				AvatarArtifactLevelExcel avatarArtifactLevelExcel = BaseExcelReader<AvatarArtifactLevel, AvatarArtifactLevelExcel>.GetInstance().FromId(avatarArtifactDataExcel.LevelList[base.AvatarArtifact.ArtifactLevel]);
				if (avatarArtifactLevelExcel != null)
				{
					if (base.Level < avatarArtifactLevelExcel.UnlockAvatarLevel)
					{
						return 1;
					}
					if (base.Star < avatarArtifactLevelExcel.UnlockAvatarStar)
					{
						return 2;
					}
					base.AvatarArtifact.ArtifactLevel++;
					ReplaceSkill[] replaceSkillList = avatarArtifactDataExcel.ReplaceSkillList;
					foreach (ReplaceSkill replace in replaceSkillList)
					{
						AvatarSubSkillDataExcel[] all = BaseExcelReader<AvatarSubSkillData, AvatarSubSkillDataExcel>.GetInstance().All;
						foreach (AvatarSubSkillDataExcel asde in all)
						{
							if (asde.ArtifactSkillId != replace.NewSkillId)
							{
								continue;
							}
							UpLevelArtifactNeedList[] upLevelArtifactNeedList = asde.UpLevelArtifactNeedList;
							foreach (UpLevelArtifactNeedList upLevelArtifactNeedList2 in upLevelArtifactNeedList)
							{
								if (upLevelArtifactNeedList2.ArtifactLevel != base.AvatarArtifact.ArtifactLevel)
								{
									continue;
								}
								AvatarSkill avatarSkill = SkillLists.Where((AvatarSkill skill) => skill.SkillId == replace.NewSkillId).FirstOrDefault();
								if (avatarSkill != null)
								{
									AvatarSubSkill avatarSubSkill = avatarSkill.SubSkillLists.Where((AvatarSubSkill subSkill) => subSkill.SubSkillId == asde.AvatarSubSkillId).FirstOrDefault();
									if (avatarSubSkill != null)
									{
										avatarSubSkill.Level = (uint)upLevelArtifactNeedList2.SubSkillLevel;
										continue;
									}
									avatarSkill.SubSkillLists.Add(new AvatarSubSkill
									{
										SubSkillId = (uint)asde.AvatarSubSkillId,
										Level = (uint)upLevelArtifactNeedList2.SubSkillLevel
									});
								}
							}
						}
					}
				}
			}
		}
		return 0;
	}
}
