using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetEquipmentDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 26
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "weapon_unique_id_list")]
	public uint[] WeaponUniqueIdLists { get; set; }

	[ProtoMember(2, Name = "stigmata_unique_id_list")]
	public uint[] StigmataUniqueIdLists { get; set; }

	[ProtoMember(3, Name = "material_id_list")]
	public uint[] MaterialIdLists { get; set; }

	[ProtoMember(4, Name = "mecha_unique_id_list")]
	public uint[] MechaUniqueIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
