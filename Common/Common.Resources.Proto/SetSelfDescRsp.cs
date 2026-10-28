using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetSelfDescRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 79
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "FORMAT_ERROR")]
		FormatError,
		[ProtoEnum(Name = "SENSITIVE_WORDS")]
		SensitiveWords,
		[ProtoEnum(Name = "IN_AUDIT")]
		InAudit,
		[ProtoEnum(Name = "IN_CD")]
		InCd
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CdEndTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "cd_end_time")]
	public uint CdEndTime
	{
		get
		{
			return __pbn__CdEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CdEndTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCdEndTime()
	{
		return __pbn__CdEndTime.HasValue;
	}

	public void ResetCdEndTime()
	{
		__pbn__CdEndTime = null;
	}
}
