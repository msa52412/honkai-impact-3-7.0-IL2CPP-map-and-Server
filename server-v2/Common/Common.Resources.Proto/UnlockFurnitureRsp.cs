using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UnlockFurnitureRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 681
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "UNLOCK_FORBID")]
		UnlockForbid,
		[ProtoEnum(Name = "HAS_UNLOCK")]
		HasUnlock,
		[ProtoEnum(Name = "HCOIN_LACK")]
		HcoinLack,
		[ProtoEnum(Name = "SCOIN_LACK")]
		ScoinLack,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
