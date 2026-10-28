using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EquipForgeSwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EquipForgeId;

	[ProtoMember(1, Name = "equip_forge_id")]
	public uint EquipForgeId
	{
		get
		{
			return __pbn__EquipForgeId.GetValueOrDefault();
		}
		set
		{
			__pbn__EquipForgeId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEquipForgeId()
	{
		return __pbn__EquipForgeId.HasValue;
	}

	public void ResetEquipForgeId()
	{
		__pbn__EquipForgeId = null;
	}
}
