using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardSelectProphesyRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7924
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
		[ProtoEnum(Name = "NOT_IN_SITE")]
		NotInSite,
		[ProtoEnum(Name = "NOT_IN_ROOM")]
		NotInRoom,
		[ProtoEnum(Name = "NO_PROPHESY_TO_SELECT")]
		NoProphesyToSelect,
		[ProtoEnum(Name = "PROPHESY_ID_ERROR")]
		ProphesyIdError
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
