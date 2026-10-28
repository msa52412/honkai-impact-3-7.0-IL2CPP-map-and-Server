using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalExploreMoveRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6904
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "STAMINA_LACK")]
		StaminaLack,
		[ProtoEnum(Name = "OUT_OF_RANGE")]
		OutOfRange,
		[ProtoEnum(Name = "PATH_INVALID")]
		PathInvalid,
		[ProtoEnum(Name = "UNEXPLOERD_LIMIT")]
		UnexploerdLimit,
		[ProtoEnum(Name = "ENTITY_OVERLAP")]
		EntityOverlap,
		[ProtoEnum(Name = "STAGE_UNFINISH")]
		StageUnfinish,
		[ProtoEnum(Name = "TOO_FREQUENT")]
		TooFrequent
	}

	private IExtension __pbn__extensionData;

	private GlobalExploreMoveType? __pbn__MoveType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "move_pos_list")]
	public List<GlobalExplorePos> MovePosLists { get; } = new List<GlobalExplorePos>();


	[ProtoMember(3, Name = "move_type")]
	[DefaultValue(GlobalExploreMoveType.GlobalExploreMoveWalk)]
	public GlobalExploreMoveType MoveType
	{
		get
		{
			return __pbn__MoveType ?? GlobalExploreMoveType.GlobalExploreMoveWalk;
		}
		set
		{
			__pbn__MoveType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMoveType()
	{
		return __pbn__MoveType.HasValue;
	}

	public void ResetMoveType()
	{
		__pbn__MoveType = null;
	}
}
