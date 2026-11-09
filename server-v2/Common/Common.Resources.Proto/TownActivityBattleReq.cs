using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivityBattleReq : IExtensible
{
	[ProtoContract]
	public class AvatarBattleResult : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__AvatarId;

		private uint? __pbn__LeftHpRatio;

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

		[ProtoMember(2, Name = "left_hp_ratio")]
		public uint LeftHpRatio
		{
			get
			{
				return __pbn__LeftHpRatio.GetValueOrDefault();
			}
			set
			{
				__pbn__LeftHpRatio = value;
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

		public bool ShouldSerializeLeftHpRatio()
		{
			return __pbn__LeftHpRatio.HasValue;
		}

		public void ResetLeftHpRatio()
		{
			__pbn__LeftHpRatio = null;
		}
	}

	[ProtoContract]
	public class MonsterBattleResult : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__MonsterUniqueId;

		private uint? __pbn__LeftHpRatio;

		[ProtoMember(1, Name = "monster_unique_id")]
		public uint MonsterUniqueId
		{
			get
			{
				return __pbn__MonsterUniqueId.GetValueOrDefault();
			}
			set
			{
				__pbn__MonsterUniqueId = value;
			}
		}

		[ProtoMember(2, Name = "left_hp_ratio")]
		public uint LeftHpRatio
		{
			get
			{
				return __pbn__LeftHpRatio.GetValueOrDefault();
			}
			set
			{
				__pbn__LeftHpRatio = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeMonsterUniqueId()
		{
			return __pbn__MonsterUniqueId.HasValue;
		}

		public void ResetMonsterUniqueId()
		{
			__pbn__MonsterUniqueId = null;
		}

		public bool ShouldSerializeLeftHpRatio()
		{
			return __pbn__LeftHpRatio.HasValue;
		}

		public void ResetLeftHpRatio()
		{
			__pbn__LeftHpRatio = null;
		}
	}

	[ProtoContract]
	public enum BattleEndStatus
	{
		[ProtoEnum(Name = "WIN")]
		Win = 1,
		[ProtoEnum(Name = "ALL_DEAD")]
		AllDead
	}

	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6456
	}

	private IExtension __pbn__extensionData;

	private BattleEndStatus? __pbn__EndStatus;

	[ProtoMember(1, Name = "head")]
	public TownCommonHead Head { get; set; }

	[ProtoMember(2, Name = "end_status")]
	[DefaultValue(BattleEndStatus.Win)]
	public BattleEndStatus EndStatus
	{
		get
		{
			return __pbn__EndStatus ?? BattleEndStatus.Win;
		}
		set
		{
			__pbn__EndStatus = value;
		}
	}

	[ProtoMember(3, Name = "avatar_result_list")]
	public List<AvatarBattleResult> AvatarResultLists { get; } = new List<AvatarBattleResult>();


	[ProtoMember(4, Name = "monster_result")]
	public MonsterBattleResult MonsterResult { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEndStatus()
	{
		return __pbn__EndStatus.HasValue;
	}

	public void ResetEndStatus()
	{
		__pbn__EndStatus = null;
	}
}
