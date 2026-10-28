using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DLCSupportNPC : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__NpcId;

	private uint? __pbn__SupportPoint;

	private uint? __pbn__SupportLevel;

	[ProtoMember(1, Name = "npc_id")]
	public uint NpcId
	{
		get
		{
			return __pbn__NpcId.GetValueOrDefault();
		}
		set
		{
			__pbn__NpcId = value;
		}
	}

	[ProtoMember(2, Name = "support_point")]
	public uint SupportPoint
	{
		get
		{
			return __pbn__SupportPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__SupportPoint = value;
		}
	}

	[ProtoMember(3, Name = "support_level")]
	public uint SupportLevel
	{
		get
		{
			return __pbn__SupportLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__SupportLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNpcId()
	{
		return __pbn__NpcId.HasValue;
	}

	public void ResetNpcId()
	{
		__pbn__NpcId = null;
	}

	public bool ShouldSerializeSupportPoint()
	{
		return __pbn__SupportPoint.HasValue;
	}

	public void ResetSupportPoint()
	{
		__pbn__SupportPoint = null;
	}

	public bool ShouldSerializeSupportLevel()
	{
		return __pbn__SupportLevel.HasValue;
	}

	public void ResetSupportLevel()
	{
		__pbn__SupportLevel = null;
	}
}
