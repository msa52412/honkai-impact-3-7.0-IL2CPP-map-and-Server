using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldBossHunt : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__CurBossId;

	private uint? __pbn__LastChooseBossTime;

	private bool? __pbn__IsCallBoss;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(2, Name = "cur_boss_id")]
	public uint CurBossId
	{
		get
		{
			return __pbn__CurBossId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurBossId = value;
		}
	}

	[ProtoMember(3, Name = "boss_list")]
	public List<OpenworldBoss> BossLists { get; } = new List<OpenworldBoss>();


	[ProtoMember(4, Name = "last_choose_boss_time")]
	public uint LastChooseBossTime
	{
		get
		{
			return __pbn__LastChooseBossTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastChooseBossTime = value;
		}
	}

	[ProtoMember(5, Name = "have_take_rating_reward_list")]
	public uint[] HaveTakeRatingRewardLists { get; set; }

	[ProtoMember(6, Name = "is_call_boss")]
	public bool IsCallBoss
	{
		get
		{
			return __pbn__IsCallBoss.GetValueOrDefault();
		}
		set
		{
			__pbn__IsCallBoss = value;
		}
	}

	[ProtoMember(7, Name = "killed_boss_id_list")]
	public uint[] KilledBossIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeCurBossId()
	{
		return __pbn__CurBossId.HasValue;
	}

	public void ResetCurBossId()
	{
		__pbn__CurBossId = null;
	}

	public bool ShouldSerializeLastChooseBossTime()
	{
		return __pbn__LastChooseBossTime.HasValue;
	}

	public void ResetLastChooseBossTime()
	{
		__pbn__LastChooseBossTime = null;
	}

	public bool ShouldSerializeIsCallBoss()
	{
		return __pbn__IsCallBoss.HasValue;
	}

	public void ResetIsCallBoss()
	{
		__pbn__IsCallBoss = null;
	}
}
