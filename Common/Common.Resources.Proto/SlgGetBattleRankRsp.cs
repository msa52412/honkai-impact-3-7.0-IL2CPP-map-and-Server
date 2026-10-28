using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgGetBattleRankRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6063
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_SCHEDULE")]
		NotInSchedule,
		[ProtoEnum(Name = "NOT_IN_BATTLE")]
		NotInBattle
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleScheduleId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "rank")]
	public SlgBattleRank Rank { get; set; }

	[ProtoMember(3, Name = "battle_schedule_id")]
	public uint BattleScheduleId
	{
		get
		{
			return __pbn__BattleScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleScheduleId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleScheduleId()
	{
		return __pbn__BattleScheduleId.HasValue;
	}

	public void ResetBattleScheduleId()
	{
		__pbn__BattleScheduleId = null;
	}
}
