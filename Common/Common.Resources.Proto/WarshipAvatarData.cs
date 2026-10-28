using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WarshipAvatarData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__WarshipFirstAvatarId;

	private uint? __pbn__WarshipSecondAvatarId;

	[ProtoMember(1, Name = "warship_first_avatar_id")]
	public uint WarshipFirstAvatarId
	{
		get
		{
			return __pbn__WarshipFirstAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__WarshipFirstAvatarId = value;
		}
	}

	[ProtoMember(2, Name = "warship_second_avatar_id")]
	public uint WarshipSecondAvatarId
	{
		get
		{
			return __pbn__WarshipSecondAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__WarshipSecondAvatarId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeWarshipFirstAvatarId()
	{
		return __pbn__WarshipFirstAvatarId.HasValue;
	}

	public void ResetWarshipFirstAvatarId()
	{
		__pbn__WarshipFirstAvatarId = null;
	}

	public bool ShouldSerializeWarshipSecondAvatarId()
	{
		return __pbn__WarshipSecondAvatarId.HasValue;
	}

	public void ResetWarshipSecondAvatarId()
	{
		__pbn__WarshipSecondAvatarId = null;
	}
}
