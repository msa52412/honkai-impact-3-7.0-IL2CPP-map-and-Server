using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivityBuildRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6455
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "INVALID_BRICK")]
		InvalidBrick,
		[ProtoEnum(Name = "BLUEPRINT_ERROR")]
		BlueprintError,
		[ProtoEnum(Name = "INVALID_POSITION")]
		InvalidPosition,
		[ProtoEnum(Name = "LIMIT")]
		Limit
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "head")]
	public TownCommonHead Head { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
