using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishGuideReportReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 129
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsForceFinish;

	[ProtoMember(1, Name = "guide_id_list")]
	public uint[] GuideIdLists { get; set; }

	[ProtoMember(2, Name = "is_force_finish")]
	public bool IsForceFinish
	{
		get
		{
			return __pbn__IsForceFinish.GetValueOrDefault();
		}
		set
		{
			__pbn__IsForceFinish = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsForceFinish()
	{
		return __pbn__IsForceFinish.HasValue;
	}

	public void ResetIsForceFinish()
	{
		__pbn__IsForceFinish = null;
	}
}
