using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetFastPassDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1556
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_EXIST")]
		NotExist
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__FastPassType;

	private uint? __pbn__UseTimes;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "fast_pass_type")]
	public uint FastPassType
	{
		get
		{
			return __pbn__FastPassType.GetValueOrDefault();
		}
		set
		{
			__pbn__FastPassType = value;
		}
	}

	[ProtoMember(3, Name = "use_times")]
	public uint UseTimes
	{
		get
		{
			return __pbn__UseTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__UseTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFastPassType()
	{
		return __pbn__FastPassType.HasValue;
	}

	public void ResetFastPassType()
	{
		__pbn__FastPassType = null;
	}

	public bool ShouldSerializeUseTimes()
	{
		return __pbn__UseTimes.HasValue;
	}

	public void ResetUseTimes()
	{
		__pbn__UseTimes = null;
	}
}
