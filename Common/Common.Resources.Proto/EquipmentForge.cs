using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EquipmentForge : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ForgeId;

	private uint? __pbn__Times;

	[ProtoMember(1, Name = "forge_id")]
	public uint ForgeId
	{
		get
		{
			return __pbn__ForgeId.GetValueOrDefault();
		}
		set
		{
			__pbn__ForgeId = value;
		}
	}

	[ProtoMember(2, Name = "times")]
	public uint Times
	{
		get
		{
			return __pbn__Times.GetValueOrDefault();
		}
		set
		{
			__pbn__Times = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeForgeId()
	{
		return __pbn__ForgeId.HasValue;
	}

	public void ResetForgeId()
	{
		__pbn__ForgeId = null;
	}

	public bool ShouldSerializeTimes()
	{
		return __pbn__Times.HasValue;
	}

	public void ResetTimes()
	{
		__pbn__Times = null;
	}
}
