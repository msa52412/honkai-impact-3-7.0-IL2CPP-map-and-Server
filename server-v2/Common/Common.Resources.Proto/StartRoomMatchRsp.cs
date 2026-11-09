using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StartRoomMatchRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3166
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "TOO_FREQUENT")]
		TooFrequent,
		[ProtoEnum(Name = "ALREADY_IN_MATCH")]
		AlreadyInMatch,
		[ProtoEnum(Name = "IS_IN_BATTLE")]
		IsInBattle,
		[ProtoEnum(Name = "IN_CD")]
		InCd,
		[ProtoEnum(Name = "TIMES_LACK")]
		TimesLack,
		[ProtoEnum(Name = "STAGE_NOT_IN_SCHEDULE")]
		StageNotInSchedule,
		[ProtoEnum(Name = "IS_NOT_READY")]
		IsNotReady,
		[ProtoEnum(Name = "NO_SCHEDULE")]
		NoSchedule,
		[ProtoEnum(Name = "FEATURE_LIMIT")]
		FeatureLimit,
		[ProtoEnum(Name = "PUNISHED")]
		Punished
	}

	private IExtension __pbn__extensionData;

	private RoomMode? __pbn__RoomMode;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "room_mode")]
	[DefaultValue(RoomMode.RoomModeInvalid)]
	public RoomMode RoomMode
	{
		get
		{
			return __pbn__RoomMode.GetValueOrDefault();
		}
		set
		{
			__pbn__RoomMode = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRoomMode()
	{
		return __pbn__RoomMode.HasValue;
	}

	public void ResetRoomMode()
	{
		__pbn__RoomMode = null;
	}
}
