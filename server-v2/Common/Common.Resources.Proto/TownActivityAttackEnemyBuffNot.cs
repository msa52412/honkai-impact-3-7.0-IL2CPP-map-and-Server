using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivityAttackEnemyBuffNotify : IExtensible
{
	[ProtoContract]
	public class Attack : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__BuildingUniqueId;

		private uint? __pbn__SkillId;

		[ProtoMember(1, Name = "building_unique_id")]
		public uint BuildingUniqueId
		{
			get
			{
				return __pbn__BuildingUniqueId.GetValueOrDefault();
			}
			set
			{
				__pbn__BuildingUniqueId = value;
			}
		}

		[ProtoMember(2, Name = "skill_id")]
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

		[ProtoMember(3, Name = "attacked_unique_id_list")]
		public uint[] AttackedUniqueIdLists { get; set; }

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeBuildingUniqueId()
		{
			return __pbn__BuildingUniqueId.HasValue;
		}

		public void ResetBuildingUniqueId()
		{
			__pbn__BuildingUniqueId = null;
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

	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6480
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "head")]
	public TownCommonHead Head { get; set; }

	[ProtoMember(2, Name = "attack_list")]
	public List<Attack> AttackLists { get; } = new List<Attack>();


	[ProtoMember(3, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
