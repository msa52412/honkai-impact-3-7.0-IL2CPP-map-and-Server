using System;
using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace Common.Database;

public class EquipmentScheme : IRow
{
	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	public Common.Resources.Proto.Material[] MaterialList { get; set; }

	public Mecha[] MechaList { get; set; }

	public Stigmata[] StigmataList { get; set; }

	public Weapon[] WeaponList { get; set; }

	public Weapon AddWeapon(int weaponId)
	{
		WeaponDataExcel weaponDataExcel = BaseExcelReader<WeaponData, WeaponDataExcel>.GetInstance().FromId(weaponId);
		if (weaponDataExcel == null)
		{
			throw new ArgumentException("Invalid weaponId");
		}
		Weapon weapon = new Weapon
		{
			UniqueId = (uint)AutoIncrement.GetNextNumber("Weapon"),
			Id = (uint)weaponDataExcel.Id,
			Level = 1u,
			Exp = 0u,
			IsExtracted = false,
			IsProtected = false
		};
		WeaponList = WeaponList.Append(weapon).ToArray();
		return weapon;
	}

	public Stigmata AddStigmata(int stigmataId)
	{
		StigmataDataExcel stigmataDataExcel = BaseExcelReader<StigmataData, StigmataDataExcel>.GetInstance().FromId(stigmataId);
		if (stigmataDataExcel == null)
		{
			throw new ArgumentException("Invalid stigmataId");
		}
		Stigmata stigmata = new Stigmata
		{
			UniqueId = (uint)AutoIncrement.GetNextNumber("Stigmata"),
			Id = (uint)stigmataDataExcel.Id,
			Level = 1u,
			Exp = 0u,
			IsProtected = false,
			SlotNum = 0u,
			RefineValue = 0u,
			PromoteTimes = 0u
		};
		StigmataList = StigmataList.Append(stigmata).ToArray();
		return stigmata;
	}

	public Common.Resources.Proto.Material[] AddWeaponExpByConsumeItem(uint uniqueId, List<EquipmentItem> consumeItemList)
	{
		List<EquipmentItem> consumeItemList2 = consumeItemList;
		int exp = consumeItemList2.Select(delegate(EquipmentItem item)
		{
			MaterialDataExcel materialDataExcel = BaseExcelReader<MaterialData, MaterialDataExcel>.GetInstance().FromId(item.IdOrUniqueId);
			if (materialDataExcel == null)
			{
				return 0;
			}
			int num = MaterialList.ToList().FindIndex((Common.Resources.Proto.Material mat) => mat.Id == item.IdOrUniqueId);
			MaterialList[num].Num -= item.Num;
			return materialDataExcel.GearExpProvideBase * (int)item.Num;
		}).Sum();
		AddWeaponExp(uniqueId, exp);
		return MaterialList.Where((Common.Resources.Proto.Material material) => consumeItemList2.Select((EquipmentItem item) => item.IdOrUniqueId).ToArray().Contains(material.Id)).ToArray();
	}

	public void AddWeaponExp(uint uniqueId, int exp)
	{
		int num = WeaponList.ToList().FindIndex((Weapon weapon) => weapon.UniqueId == uniqueId);
		if (num == -1)
		{
			return;
		}
		WeaponDataExcel weaponDataExcel = BaseExcelReader<WeaponData, WeaponDataExcel>.GetInstance().FromId((int)WeaponList[num].Id);
		if (weaponDataExcel == null)
		{
			return;
		}
		PlayerLevelData.LevelData levelData = BaseExcelReader<EquipmentLevelData, EquipmentLevelDataExcel>.GetInstance().CalculateLevel((int)WeaponList[num].Level, (int)WeaponList[num].Exp + exp, weaponDataExcel.ExpType);
		if (levelData.Level > weaponDataExcel.MaxLv)
		{
			levelData.Level = weaponDataExcel.MaxLv;
			EquipmentLevelDataExcel equipmentLevelDataExcel = BaseExcelReader<EquipmentLevelData, EquipmentLevelDataExcel>.GetInstance().FromLevel(levelData.Level);
			if (equipmentLevelDataExcel == null)
			{
				return;
			}
			levelData.Exp = equipmentLevelDataExcel.Type1[weaponDataExcel.ExpType];
		}
		WeaponList[num].Level = (uint)levelData.Level;
		WeaponList[num].Exp = (uint)levelData.Exp;
	}

	public Common.Resources.Proto.Material[] AddStigmataExpByConsumeItem(uint uniqueId, List<EquipmentItem> consumeItemList)
	{
		List<EquipmentItem> consumeItemList2 = consumeItemList;
		int exp = consumeItemList2.Select(delegate(EquipmentItem item)
		{
			MaterialDataExcel materialDataExcel = BaseExcelReader<MaterialData, MaterialDataExcel>.GetInstance().FromId(item.IdOrUniqueId);
			if (materialDataExcel == null)
			{
				return 0;
			}
			int num = MaterialList.ToList().FindIndex((Common.Resources.Proto.Material mat) => mat.Id == item.IdOrUniqueId);
			MaterialList[num].Num -= item.Num;
			return materialDataExcel.GearExpProvideBase * (int)item.Num;
		}).Sum();
		AddStigmataExp(uniqueId, exp);
		return MaterialList.Where((Common.Resources.Proto.Material material) => consumeItemList2.Select((EquipmentItem item) => item.IdOrUniqueId).ToArray().Contains(material.Id)).ToArray();
	}

	public void AddStigmataExp(uint uniqueId, int exp)
	{
		int num = StigmataList.ToList().FindIndex((Stigmata stigmata) => stigmata.UniqueId == uniqueId);
		if (num == -1)
		{
			return;
		}
		StigmataDataExcel stigmataDataExcel = BaseExcelReader<StigmataData, StigmataDataExcel>.GetInstance().FromId((int)StigmataList[num].Id);
		if (stigmataDataExcel == null)
		{
			return;
		}
		PlayerLevelData.LevelData levelData = BaseExcelReader<EquipmentLevelData, EquipmentLevelDataExcel>.GetInstance().CalculateLevel((int)StigmataList[num].Level, (int)StigmataList[num].Exp + exp, stigmataDataExcel.ExpType);
		if (levelData.Level > stigmataDataExcel.MaxLv)
		{
			levelData.Level = stigmataDataExcel.MaxLv;
			EquipmentLevelDataExcel equipmentLevelDataExcel = BaseExcelReader<EquipmentLevelData, EquipmentLevelDataExcel>.GetInstance().FromLevel(levelData.Level);
			if (equipmentLevelDataExcel == null)
			{
				return;
			}
			levelData.Exp = equipmentLevelDataExcel.Type1[stigmataDataExcel.ExpType];
		}
		StigmataList[num].Level = (uint)levelData.Level;
		StigmataList[num].Exp = (uint)levelData.Exp;
	}

	public Common.Resources.Proto.Material AddMaterial(int materialId, int num = 1)
	{
		int num2 = Array.FindIndex(MaterialList, (Common.Resources.Proto.Material material) => material.Id == materialId);
		if (num2 == -1)
		{
			MaterialList = MaterialList.Append(new Common.Resources.Proto.Material
			{
				Id = (uint)materialId,
				Num = ((num >= 0) ? ((uint)num) : 0u)
			}).ToArray();
		}
		else if (num < 0)
		{
			MaterialList[num2].Num -= (uint)Math.Abs(num);
		}
		else
		{
			MaterialList[num2].Num += (uint)num;
		}
		return MaterialList.Where((Common.Resources.Proto.Material material) => material.Id == materialId).First();
	}

	public void Save()
	{
		Equipment.collection.ReplaceOne(this);
	}
}
