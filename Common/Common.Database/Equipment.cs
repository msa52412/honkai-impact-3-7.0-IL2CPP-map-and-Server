using System;
using System.Linq;
using Common.Resources.Proto;

namespace Common.Database;

public class Equipment
{
	public static readonly SqlTable<EquipmentScheme> collection = Global.Table<EquipmentScheme>("Equipments", ("owner_uid", "INTEGER", x => x.OwnerUid, true));

	public static EquipmentScheme FromUid(uint uid)
	{
		return (from collection in collection.All()
			where collection.OwnerUid == uid
			select collection).FirstOrDefault() ?? Create(uid);
	}

	public static EquipmentScheme Create(uint uid)
	{
		EquipmentScheme equipmentScheme = (from collection in collection.All()
			where collection.OwnerUid == uid
			select collection).FirstOrDefault();
		if (equipmentScheme != null)
		{
			return equipmentScheme;
		}
		EquipmentScheme equipmentScheme2 = new EquipmentScheme();
		equipmentScheme2.OwnerUid = uid;
		equipmentScheme2.MaterialList = new Material[2]
		{
			new Material
			{
				Id = 100u,
				Num = 750u
			},
			new Material
			{
				Id = 119107u,
				Num = 6u
			}
		};
		equipmentScheme2.WeaponList = Array.Empty<Weapon>();
		equipmentScheme2.StigmataList = Array.Empty<Stigmata>();
		equipmentScheme2.MechaList = Array.Empty<Mecha>();
		EquipmentScheme equipmentScheme3 = equipmentScheme2;
		collection.InsertOne(equipmentScheme3);
		return equipmentScheme3;
	}
}
