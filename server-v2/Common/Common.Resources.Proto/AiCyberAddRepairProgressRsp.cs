using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AiCyberAddRepairProgressRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6655
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVITY_NOT_OPEN")]
		ActivityNotOpen,
		[ProtoEnum(Name = "TARGET_PROGRESS_ERROR")]
		TargetProgressError,
		[ProtoEnum(Name = "CONDITION_NOT_MEET")]
		ConditionNotMeet,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetProgress;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "target_progress")]
	public uint TargetProgress
	{
		get
		{
			return __pbn__TargetProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetProgress = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTargetProgress()
	{
		return __pbn__TargetProgress.HasValue;
	}

	public void ResetTargetProgress()
	{
		__pbn__TargetProgress = null;
	}
}
