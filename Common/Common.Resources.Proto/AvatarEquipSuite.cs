using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarEquipSuite : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SuiteId;

	private uint? __pbn__WeaponUniqueId;

	private uint? __pbn__StigmataUniqueId1;

	private uint? __pbn__StigmataUniqueId2;

	private uint? __pbn__StigmataUniqueId3;

	private string __pbn__SuiteName;

	[ProtoMember(1, Name = "suite_id")]
	public uint SuiteId
	{
		get
		{
			return __pbn__SuiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__SuiteId = value;
		}
	}

	[ProtoMember(2, Name = "weapon_unique_id")]
	public uint WeaponUniqueId
	{
		get
		{
			return __pbn__WeaponUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__WeaponUniqueId = value;
		}
	}

	[ProtoMember(3, Name = "stigmata_unique_id_1")]
	public uint StigmataUniqueId1
	{
		get
		{
			return __pbn__StigmataUniqueId1.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataUniqueId1 = value;
		}
	}

	[ProtoMember(4, Name = "stigmata_unique_id_2")]
	public uint StigmataUniqueId2
	{
		get
		{
			return __pbn__StigmataUniqueId2.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataUniqueId2 = value;
		}
	}

	[ProtoMember(5, Name = "stigmata_unique_id_3")]
	public uint StigmataUniqueId3
	{
		get
		{
			return __pbn__StigmataUniqueId3.GetValueOrDefault();
		}
		set
		{
			__pbn__StigmataUniqueId3 = value;
		}
	}

	[ProtoMember(6, Name = "suite_name")]
	[DefaultValue("")]
	public string SuiteName
	{
		get
		{
			return __pbn__SuiteName ?? "";
		}
		set
		{
			__pbn__SuiteName = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSuiteId()
	{
		return __pbn__SuiteId.HasValue;
	}

	public void ResetSuiteId()
	{
		__pbn__SuiteId = null;
	}

	public bool ShouldSerializeWeaponUniqueId()
	{
		return __pbn__WeaponUniqueId.HasValue;
	}

	public void ResetWeaponUniqueId()
	{
		__pbn__WeaponUniqueId = null;
	}

	public bool ShouldSerializeStigmataUniqueId1()
	{
		return __pbn__StigmataUniqueId1.HasValue;
	}

	public void ResetStigmataUniqueId1()
	{
		__pbn__StigmataUniqueId1 = null;
	}

	public bool ShouldSerializeStigmataUniqueId2()
	{
		return __pbn__StigmataUniqueId2.HasValue;
	}

	public void ResetStigmataUniqueId2()
	{
		__pbn__StigmataUniqueId2 = null;
	}

	public bool ShouldSerializeStigmataUniqueId3()
	{
		return __pbn__StigmataUniqueId3.HasValue;
	}

	public void ResetStigmataUniqueId3()
	{
		__pbn__StigmataUniqueId3 = null;
	}

	public bool ShouldSerializeSuiteName()
	{
		return __pbn__SuiteName != null;
	}

	public void ResetSuiteName()
	{
		__pbn__SuiteName = null;
	}
}
