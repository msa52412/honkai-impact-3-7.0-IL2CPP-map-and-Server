using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetDormSnsInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 636
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "HOUSE_NOT_EXIST")]
		HouseNotExist,
		[ProtoEnum(Name = "ROOM_NOT_EXIST")]
		RoomNotExist,
		[ProtoEnum(Name = "AVATAR_NOT_EXIST")]
		AvatarNotExist
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
