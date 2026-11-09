using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EndlessSingleModeActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CurFloor;

	private bool? __pbn__IsSettled;

	private uint? __pbn__ResetTimes;

	private uint? __pbn__MonsterRandomId;

	private uint? __pbn__ActivityType;

	[ProtoMember(1, Name = "cur_floor")]
	public uint CurFloor
	{
		get
		{
			return __pbn__CurFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__CurFloor = value;
		}
	}

	[ProtoMember(2, Name = "floor_score_list")]
	public List<EndlessSingleModeScore> FloorScoreLists { get; } = new List<EndlessSingleModeScore>();


	[ProtoMember(3, Name = "is_settled")]
	public bool IsSettled
	{
		get
		{
			return __pbn__IsSettled.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSettled = value;
		}
	}

	[ProtoMember(4, Name = "reset_times")]
	public uint ResetTimes
	{
		get
		{
			return __pbn__ResetTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ResetTimes = value;
		}
	}

	[ProtoMember(5, Name = "monster_random_id")]
	public uint MonsterRandomId
	{
		get
		{
			return __pbn__MonsterRandomId.GetValueOrDefault();
		}
		set
		{
			__pbn__MonsterRandomId = value;
		}
	}

	[ProtoMember(6, Name = "activity_type")]
	public uint ActivityType
	{
		get
		{
			return __pbn__ActivityType.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityType = value;
		}
	}

	[ProtoMember(7, Name = "ow_monster_score_list")]
	public List<EndlessSingleModeMonsterScore> OwMonsterScoreLists { get; } = new List<EndlessSingleModeMonsterScore>();


	[ProtoMember(8, Name = "ow_item_list")]
	public List<EndlessSingleModeItem> OwItemLists { get; } = new List<EndlessSingleModeItem>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurFloor()
	{
		return __pbn__CurFloor.HasValue;
	}

	public void ResetCurFloor()
	{
		__pbn__CurFloor = null;
	}

	public bool ShouldSerializeIsSettled()
	{
		return __pbn__IsSettled.HasValue;
	}

	public void ResetIsSettled()
	{
		__pbn__IsSettled = null;
	}

	public bool ShouldSerializeResetTimes()
	{
		return __pbn__ResetTimes.HasValue;
	}

	public void ResetResetTimes()
	{
		__pbn__ResetTimes = null;
	}

	public bool ShouldSerializeMonsterRandomId()
	{
		return __pbn__MonsterRandomId.HasValue;
	}

	public void ResetMonsterRandomId()
	{
		__pbn__MonsterRandomId = null;
	}

	public bool ShouldSerializeActivityType()
	{
		return __pbn__ActivityType.HasValue;
	}

	public void ResetActivityType()
	{
		__pbn__ActivityType = null;
	}
}
