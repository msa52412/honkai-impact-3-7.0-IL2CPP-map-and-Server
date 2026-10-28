using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CommentReportRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 230
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

	private uint? __pbn__NextEvaluateTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "next_evaluate_time")]
	public uint NextEvaluateTime
	{
		get
		{
			return __pbn__NextEvaluateTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextEvaluateTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNextEvaluateTime()
	{
		return __pbn__NextEvaluateTime.HasValue;
	}

	public void ResetNextEvaluateTime()
	{
		__pbn__NextEvaluateTime = null;
	}
}
