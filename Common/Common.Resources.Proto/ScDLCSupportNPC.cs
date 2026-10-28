using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScDLCSupportNPC : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__NpcId;

	private uint? __pbn__Exp;

	private uint? __pbn__Level;

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

	[ProtoMember(2, Name = "exp")]
	public uint Exp
	{
		get
		{
			return __pbn__Exp.GetValueOrDefault();
		}
		set
		{
			__pbn__Exp = value;
		}
	}

	[ProtoMember(3, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
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

	public bool ShouldSerializeExp()
	{
		return __pbn__Exp.HasValue;
	}

	public void ResetExp()
	{
		__pbn__Exp = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}
}
