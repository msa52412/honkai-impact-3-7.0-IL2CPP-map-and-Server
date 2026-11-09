using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetEquipmentDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 27
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

	private bool? __pbn__IsAll;

	private uint? __pbn__VitalityValue;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "weapon_list")]
	public List<Weapon> WeaponLists { get; } = new List<Weapon>();


	[ProtoMember(3, Name = "stigmata_list")]
	public List<Stigmata> StigmataLists { get; } = new List<Stigmata>();


	[ProtoMember(4, Name = "material_list")]
	public List<Material> MaterialLists { get; } = new List<Material>();


	[ProtoMember(5, Name = "is_all")]
	public bool IsAll
	{
		get
		{
			return __pbn__IsAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAll = value;
		}
	}

	[ProtoMember(6, Name = "mecha_list")]
	public List<Mecha> MechaLists { get; } = new List<Mecha>();


	[ProtoMember(7, Name = "vitality_value")]
	public uint VitalityValue
	{
		get
		{
			return __pbn__VitalityValue.GetValueOrDefault();
		}
		set
		{
			__pbn__VitalityValue = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsAll()
	{
		return __pbn__IsAll.HasValue;
	}

	public void ResetIsAll()
	{
		__pbn__IsAll = null;
	}

	public bool ShouldSerializeVitalityValue()
	{
		return __pbn__VitalityValue.HasValue;
	}

	public void ResetVitalityValue()
	{
		__pbn__VitalityValue = null;
	}
}
