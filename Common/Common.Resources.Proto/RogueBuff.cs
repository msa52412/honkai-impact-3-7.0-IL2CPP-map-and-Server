using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RogueBuff : IExtensible
{
	[ProtoContract]
	public enum Type
	{
		[ProtoEnum(Name = "NORMAL")]
		Normal = 1,
		[ProtoEnum(Name = "SUPPORT_AVATAR")]
		SupportAvatar
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__Level;

	private Type? __pbn__type;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "level")]
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

	[ProtoMember(3)]
	[DefaultValue(Type.Normal)]
	public Type type
	{
		get
		{
			return __pbn__type ?? Type.Normal;
		}
		set
		{
			__pbn__type = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializetype()
	{
		return __pbn__type.HasValue;
	}

	public void Resettype()
	{
		__pbn__type = null;
	}
}
