using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleStartTimeLineNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6595
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleBeginTime;

	private uint? __pbn__BattleEndTime;

	[ProtoMember(1, Name = "battle_begin_time")]
	public uint BattleBeginTime
	{
		get
		{
			return __pbn__BattleBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleBeginTime = value;
		}
	}

	[ProtoMember(2, Name = "battle_end_time")]
	public uint BattleEndTime
	{
		get
		{
			return __pbn__BattleEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleEndTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleBeginTime()
	{
		return __pbn__BattleBeginTime.HasValue;
	}

	public void ResetBattleBeginTime()
	{
		__pbn__BattleBeginTime = null;
	}

	public bool ShouldSerializeBattleEndTime()
	{
		return __pbn__BattleEndTime.HasValue;
	}

	public void ResetBattleEndTime()
	{
		__pbn__BattleEndTime = null;
	}
}
