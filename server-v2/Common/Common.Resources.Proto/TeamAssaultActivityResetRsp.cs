using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TeamAssaultActivityResetRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4029
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 0,
		[ProtoEnum(Name = "FAIL")]
		Fail = 1,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen = 2,
		[ProtoEnum(Name = "LACK_TIMES")]
		LackTimes = 4,
		[ProtoEnum(Name = "NOT_NEED")]
		NotNeed = 5
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
