using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EndlessSelectTypeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3463
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "IN_ULTRA")]
		InUltra,
		[ProtoEnum(Name = "NOT_IN_NEXT_SCHEDULE")]
		NotInNextSchedule,
		[ProtoEnum(Name = "LACK_LEVEL")]
		LackLevel
	}

	private IExtension __pbn__extensionData;

	private EndlessType? __pbn__EndlessType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "endless_type")]
	[DefaultValue(EndlessType.EndlessTypeNone)]
	public EndlessType EndlessType
	{
		get
		{
			return __pbn__EndlessType.GetValueOrDefault();
		}
		set
		{
			__pbn__EndlessType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEndlessType()
	{
		return __pbn__EndlessType.HasValue;
	}

	public void ResetEndlessType()
	{
		__pbn__EndlessType = null;
	}
}
