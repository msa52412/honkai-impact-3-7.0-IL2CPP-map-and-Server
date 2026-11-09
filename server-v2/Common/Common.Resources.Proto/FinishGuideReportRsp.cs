using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishGuideReportRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 130
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

	private bool? __pbn__IsFinish;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "guide_id_list")]
	public uint[] GuideIdLists { get; set; }

	[ProtoMember(3, Name = "is_finish")]
	public bool IsFinish
	{
		get
		{
			return __pbn__IsFinish.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFinish = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsFinish()
	{
		return __pbn__IsFinish.HasValue;
	}

	public void ResetIsFinish()
	{
		__pbn__IsFinish = null;
	}
}
