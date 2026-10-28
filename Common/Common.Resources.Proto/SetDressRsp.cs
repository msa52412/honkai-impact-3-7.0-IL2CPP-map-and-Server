using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetDressRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 249
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "AVATAR_NOT_EXIST")]
		AvatarNotExist,
		[ProtoEnum(Name = "DRESS_NOT_EXIST")]
		DressNotExist,
		[ProtoEnum(Name = "DRESS_CAN_NOT_USE")]
		DressCanNotUse,
		[ProtoEnum(Name = "DRESS_IN_USE")]
		DressInUse
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
