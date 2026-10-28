using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMinuteTimesRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 545
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

	private uint? __pbn__LeftTimes;

	private uint? __pbn__RecoverTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "left_times")]
	public uint LeftTimes
	{
		get
		{
			return __pbn__LeftTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftTimes = value;
		}
	}

	[ProtoMember(3, Name = "recover_time")]
	public uint RecoverTime
	{
		get
		{
			return __pbn__RecoverTime.GetValueOrDefault();
		}
		set
		{
			__pbn__RecoverTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLeftTimes()
	{
		return __pbn__LeftTimes.HasValue;
	}

	public void ResetLeftTimes()
	{
		__pbn__LeftTimes = null;
	}

	public bool ShouldSerializeRecoverTime()
	{
		return __pbn__RecoverTime.HasValue;
	}

	public void ResetRecoverTime()
	{
		__pbn__RecoverTime = null;
	}
}
