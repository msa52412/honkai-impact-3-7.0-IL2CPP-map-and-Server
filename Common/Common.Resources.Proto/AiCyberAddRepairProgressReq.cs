using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AiCyberAddRepairProgressReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6654
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetProgress;

	[ProtoMember(1, Name = "target_progress")]
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
