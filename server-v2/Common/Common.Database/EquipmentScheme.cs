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

	/// <summary>
	/// 扩充补给碎片（协议里的 <c>CustomGachaData.ex_energy</c> / <c>GachaExFragment.ex_energy</c>）。
	///
	/// 每抽一次 +1，攒到 <c>CustomGachaExFragment[EventID=1].MaterialAmountRequest</c>（210）
	/// 可以把 1 个 up 的 S 级女武神换出来 —— 也就是补给页右上角那个「0/210 → x1」。
	/// 存进存档表的 payload（JSON），老存档没有这个字段会反序列化成 0，不需要迁移。
	/// </summary>
	public uint GachaExEnergy { get; set; }

	/// <summary>
	/// 低档保底计数器：距离上次抽到「保底池成员」已经抽了多少次。
	///
	/// 由 <c>GachaService.Draw</c> 维护：抽到 <c>pity</c> / <c>featured</c> 掉落就清零，否则 +1；
	/// 到了池里配置的 <c>guarantee_count</c> 还没出，就强制从保底池里摇一个。
	/// 没有配这一档保底的池不会动这个数。
	///
	/// 同样存进 payload（JSON），老存档没有这个字段会反序列化成 0，不需要迁移。
	/// </summary>
	public uint GachaGuaranteeCounter { get; set; }

	/// <summary>
	/// 高档保底计数器：距离上次抽到「本期 UP」已经抽了多少次（角色池 100 抽保底用）。
	///
	/// 抽到 <c>featured</c> 掉落清零，否则 +1；到了 <c>guarantee_up_count</c> 还没出，
	/// 就强制从 UP 里摇。两档保底各自独立计数 —— 低档出个 A 级不能把高档的进度推掉。
	///
	/// 存进 payload（JSON），老存档反序列化成 0，不需要迁移。
	/// </summary>
	public uint GachaUpGuaranteeCounter { get; set; }

	/// <summary>
	/// 按卡池分档的抽卡进度（8.x 起补给改成「每个池各自保底」）。
	///
	/// 老存档只有全局的 <see cref="GachaGuaranteeCounter"/> /
	/// <see cref="GachaUpGuaranteeCounter"/>，第一次读到本字段为空时由
	/// <c>GachaService.Progress</c> 从全局计数迁移过来，并把
	/// <see cref="LegacyGachaProgressMigrated"/> 置真，避免重复迁移。
	/// </summary>
	public Dictionary<uint, GachaProgress> GachaProgressByType { get; set; }

	/// <summary>全局保底计数是否已经迁进 <see cref="GachaProgressByType"/>。</summary>
	public bool LegacyGachaProgressMigrated { get; set; }

	/// <summary>
	/// 日常任务 / 每日委托 / 战令经验。和装备放一张表，领奖时才能和背包装在一个事务里。
	/// 老存档为 null，用的时候 <c>??= new()</c> 补。
	/// </summary>
	public DailyGameplayState DailyGameplay { get; set; }

	/// <summary>
	/// 「心愿井」类卡池已经许过愿的档位键（pool + set 拼出来的字符串）。
	/// 每个档位只能许一次，所以只记键不记次数。
	/// </summary>
	public HashSet<string> UsedGachaWishes { get; set; } = new HashSet<string>();

	/// <summary>
	/// 当前正在进行中的圣痕词缀三选一的组 id（0 表示没有待选）。
	/// 客户端刷新界面后靠这个值把「选择词缀」弹窗恢复出来。
	/// </summary>
	public uint RuneCandidateSequence { get; set; }

	/// <summary>单个卡池的抽卡进度。抽到什么就记什么，切池不清零。</summary>
	public class GachaProgress
	{
		/// <summary>低档（A 级保底）计数。</summary>
		public uint Low { get; set; }

		/// <summary>高档（本期 UP 保底）计数。</summary>
		public uint Up { get; set; }

		/// <summary>该池累计抽数。</summary>
		public uint Total { get; set; }

		/// <summary>上一次抽卡时这一池的 banner 指纹，换期就重置保底。</summary>
		public string Banner { get; set; } = "";

		/// <summary>扩充补给已攒的碎片数。</summary>
		public uint ExFragments { get; set; }

		/// <summary>最近一次抽卡获得的副能量 / 碎片，只用于回包展示。</summary>
		public uint LastEnergyAdd { get; set; }

		/// <summary>最近一次抽卡获得的碎片数，只用于回包展示。</summary>
		public uint LastFragmentAdd { get; set; }

		/// <summary>「集齐 UP 装备」类卡池里已经拿到过的 UP 物品 id。</summary>
		public HashSet<uint> ObtainedUpItems { get; set; } = new HashSet<uint>();
	}


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
