using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EditDormRoomRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 606
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ROOM_NOT_EXIST")]
		RoomNotExist,
		[ProtoEnum(Name = "FURNITURE_LACK")]
		FurnitureLack,
		[ProtoEnum(Name = "EFFECT_LACK")]
		EffectLack
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
