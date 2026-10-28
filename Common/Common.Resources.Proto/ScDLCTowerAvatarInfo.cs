using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScDLCTowerAvatarInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__HpPercent;

	private uint? __pbn__SpPercent;

	[ProtoMember(1, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(2, Name = "hp_percent")]
	public uint HpPercent
	{
		get
		{
			return __pbn__HpPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__HpPercent = value;
		}
	}

	[ProtoMember(3, Name = "sp_percent")]
	public uint SpPercent
	{
		get
		{
			return __pbn__SpPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__SpPercent = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeHpPercent()
	{
		return __pbn__HpPercent.HasValue;
	}

	public void ResetHpPercent()
	{
		__pbn__HpPercent = null;
	}

	public bool ShouldSerializeSpPercent()
	{
		return __pbn__SpPercent.HasValue;
	}

	public void ResetSpPercent()
	{
		__pbn__SpPercent = null;
	}
}
