using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScDLCActiveFeverAbilityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6346
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVE_NUM_LIMIT")]
		ActiveNumLimit,
		[ProtoEnum(Name = "ACTIVE_LIST_EMPTY")]
		ActiveListEmpty,
		[ProtoEnum(Name = "GROUP_INVALID")]
		GroupInvalid
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
