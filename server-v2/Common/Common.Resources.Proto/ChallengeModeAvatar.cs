using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChallengeModeAvatar : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__HpPercent;

	private uint? __pbn__SpPercent;

	private uint? __pbn__LastRecoverTime;

	private bool? __pbn__IsDie;

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

	[ProtoMember(4, Name = "last_recover_time")]
	public uint LastRecoverTime
	{
		get
		{
			return __pbn__LastRecoverTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastRecoverTime = value;
		}
	}

	[ProtoMember(5, Name = "is_die")]
	public bool IsDie
	{
		get
		{
			return __pbn__IsDie.GetValueOrDefault();
		}
		set
		{
			__pbn__IsDie = value;
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

	public bool ShouldSerializeLastRecoverTime()
	{
		return __pbn__LastRecoverTime.HasValue;
	}

	public void ResetLastRecoverTime()
	{
		__pbn__LastRecoverTime = null;
	}

	public bool ShouldSerializeIsDie()
	{
		return __pbn__IsDie.HasValue;
	}

	public void ResetIsDie()
	{
		__pbn__IsDie = null;
	}
}
