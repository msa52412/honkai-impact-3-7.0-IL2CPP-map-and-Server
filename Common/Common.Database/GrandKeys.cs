using System;
using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace Common.Database;

internal class GrandKeys
{
	public class GrandKeysScheme : IRow
	{
		public long Id { get; set; }

		public long CreatedAt { get; set; }

		public uint OwnerUid { get; set; }

		public List<GrandKey> GrandKeyList { get; set; }

		public void Save()
		{
			collection.ReplaceOne(this);
		}

		public GrandKey AddGrandKey(int grandKeyId)
		{
			GrandKeyDataExcel grandKeyDataExcel = BaseExcelReader<GrandKeyData, GrandKeyDataExcel>.GetInstance().FromId(grandKeyId);
			if (grandKeyDataExcel == null)
			{
				throw new ArgumentException("Invalid grandKeyId");
			}
			GrandKey grandKey = new GrandKey
			{
				Id = (uint)grandKeyDataExcel.GrandKeyId,
				Level = 1u,
				EndTime = 0u,
				UnlockLevel = 50u,
				ActivateLevel = 0u,
				BreachLevel = 0u,
				Skill = 
				{
					SkillId = 1u
				},
				ScoinNum = 0u
			};
			GrandKeyList.Add(grandKey);
			return grandKey;
		}

		public void LevelUpGrandKey(int id)
		{
			GrandKeyList.ForEach(delegate(GrandKey grandKey)
			{
				if (grandKey.Id == id)
				{
					grandKey.Level++;
				}
			});
		}

		public void ActivateGrandKeySkill(int keyId, int skillId, int lastTime)
		{
		}

		public void SetGrandKeySkill(int keyId, int skillId)
		{
		}
	}

	public static readonly SqlTable<GrandKeysScheme> collection = Global.Table<GrandKeysScheme>("GrandKeys", ("owner_uid", "INTEGER", x => x.OwnerUid, true));

	public static GrandKeysScheme FromUid(uint uid)
	{
		return (from collection in collection.All()
			where collection.OwnerUid == uid
			select collection).FirstOrDefault() ?? Create(uid);
	}

	public static GrandKeysScheme Create(uint uid)
	{
		GrandKeysScheme grandKeysScheme = (from collection in collection.All()
			where collection.OwnerUid == uid
			select collection).FirstOrDefault();
		if (grandKeysScheme != null)
		{
			return grandKeysScheme;
		}
		GrandKeysScheme grandKeysScheme2 = new GrandKeysScheme();
		collection.InsertOne(grandKeysScheme2);
		return grandKeysScheme2;
	}
}
