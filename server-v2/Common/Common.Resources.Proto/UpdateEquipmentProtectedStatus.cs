using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UpdateEquipmentProtectedStatusReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 74
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsProtected;

	[ProtoMember(1, Name = "is_protected")]
	public bool IsProtected
	{
		get
		{
			return __pbn__IsProtected.GetValueOrDefault();
		}
		set
		{
			__pbn__IsProtected = value;
		}
	}

	[ProtoMember(2, Name = "weapon_unique_id_list")]
	public uint[] WeaponUniqueIdLists { get; set; }

	[ProtoMember(3, Name = "stigmata_unique_id_list")]
	public uint[] StigmataUniqueIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsProtected()
	{
		return __pbn__IsProtected.HasValue;
	}

	public void ResetIsProtected()
	{
		__pbn__IsProtected = null;
	}
}
[ProtoContract]
public class UpdateEquipmentProtectedStatusRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 75
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "EQUIPMENT_NOT_EXIST")]
		EquipmentNotExist
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
