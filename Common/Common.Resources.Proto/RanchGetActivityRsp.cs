using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RanchGetActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5834
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__NextRefreshTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "monster_list")]
	public List<RanchMonster> MonsterLists { get; } = new List<RanchMonster>();


	[ProtoMember(4, Name = "area_list")]
	public List<RanchArea> AreaLists { get; } = new List<RanchArea>();


	[ProtoMember(5, Name = "next_refresh_time")]
	public uint NextRefreshTime
	{
		get
		{
			return __pbn__NextRefreshTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextRefreshTime = value;
		}
	}

	[ProtoMember(6, Name = "stage_list")]
	public List<RanchStage> StageLists { get; } = new List<RanchStage>();


	[ProtoMember(7, Name = "battle_unique_id_list")]
	public uint[] BattleUniqueIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeNextRefreshTime()
	{
		return __pbn__NextRefreshTime.HasValue;
	}

	public void ResetNextRefreshTime()
	{
		__pbn__NextRefreshTime = null;
	}
}
