using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuffAssistPublishBuffPoolRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5948
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
		[ProtoEnum(Name = "ASSIST_WORD_ERROR")]
		AssistWordError,
		[ProtoEnum(Name = "POOL_ERROR")]
		PoolError,
		[ProtoEnum(Name = "BUFF_SCHEDULE_ERROR")]
		BuffScheduleError,
		[ProtoEnum(Name = "BUFF_POOL_FULL")]
		BuffPoolFull,
		[ProtoEnum(Name = "LACK_TIMES")]
		LackTimes,
		[ProtoEnum(Name = "REPEATED_PUBLISH")]
		RepeatedPublish
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PoolId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "pool_id")]
	public uint PoolId
	{
		get
		{
			return __pbn__PoolId.GetValueOrDefault();
		}
		set
		{
			__pbn__PoolId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePoolId()
	{
		return __pbn__PoolId.HasValue;
	}

	public void ResetPoolId()
	{
		__pbn__PoolId = null;
	}
}
