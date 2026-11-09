using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatReportRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1151
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "CHAT_BLOCKED")]
		ChatBlocked,
		[ProtoEnum(Name = "IN_REPORT_CD")]
		InReportCd,
		[ProtoEnum(Name = "SENSITIVE_WORDS")]
		SensitiveWords
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CdLeftTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "cd_left_time")]
	public uint CdLeftTime
	{
		get
		{
			return __pbn__CdLeftTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CdLeftTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCdLeftTime()
	{
		return __pbn__CdLeftTime.HasValue;
	}

	public void ResetCdLeftTime()
	{
		__pbn__CdLeftTime = null;
	}
}
