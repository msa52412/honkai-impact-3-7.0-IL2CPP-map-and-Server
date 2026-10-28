using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetHasGotFurnitureIdListRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 620
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "furniture_id_list")]
	public uint[] FurnitureIdLists { get; set; }

	[ProtoMember(3, Name = "has_unlock_furniture_id_list")]
	public uint[] HasUnlockFurnitureIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
