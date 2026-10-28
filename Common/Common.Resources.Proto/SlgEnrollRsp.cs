using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgEnrollRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6056
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "BATTLE_SCHEDULE_ID_ERROR")]
		BattleScheduleIdError,
		[ProtoEnum(Name = "HAS_ENROLL")]
		HasEnroll,
		[ProtoEnum(Name = "TOO_FREQUENT")]
		TooFrequent,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleScheduleId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "battle_schedule_id")]
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
