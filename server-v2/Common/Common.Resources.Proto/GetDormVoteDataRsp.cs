using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetDormVoteDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1139
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	[ProtoContract]
	public enum Status
	{
		[ProtoEnum(Name = "NOT_START")]
		NotStart,
		[ProtoEnum(Name = "VOTING")]
		Voting,
		[ProtoEnum(Name = "REVIEWING")]
		Reviewing,
		[ProtoEnum(Name = "DISPLAYING")]
		Displaying
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private Status? __pbn__status;

	private uint? __pbn__EndTime;

	private string __pbn__Url;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3)]
	[DefaultValue(Status.NotStart)]
	public Status status
	{
		get
		{
			return __pbn__status.GetValueOrDefault();
		}
		set
		{
			__pbn__status = value;
		}
	}

	[ProtoMember(4, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(5, Name = "url")]
	[DefaultValue("")]
	public string Url
	{
		get
		{
			return __pbn__Url ?? "";
		}
		set
		{
			__pbn__Url = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializestatus()
	{
		return __pbn__status.HasValue;
	}

	public void Resetstatus()
	{
		__pbn__status = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeUrl()
	{
		return __pbn__Url != null;
	}

	public void ResetUrl()
	{
		__pbn__Url = null;
	}
}
