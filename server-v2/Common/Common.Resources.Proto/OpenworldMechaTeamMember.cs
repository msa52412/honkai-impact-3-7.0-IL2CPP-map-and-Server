using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldMechaTeamMember : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Pos;

	private uint? __pbn__MechaUniqueId;

	[ProtoMember(1, Name = "pos")]
	public uint Pos
	{
		get
		{
			return __pbn__Pos.GetValueOrDefault();
		}
		set
		{
			__pbn__Pos = value;
		}
	}

	[ProtoMember(2, Name = "mecha_unique_id")]
	public uint MechaUniqueId
	{
		get
		{
			return __pbn__MechaUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__MechaUniqueId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePos()
	{
		return __pbn__Pos.HasValue;
	}

	public void ResetPos()
	{
		__pbn__Pos = null;
	}

	public bool ShouldSerializeMechaUniqueId()
	{
		return __pbn__MechaUniqueId.HasValue;
	}

	public void ResetMechaUniqueId()
	{
		__pbn__MechaUniqueId = null;
	}
}
