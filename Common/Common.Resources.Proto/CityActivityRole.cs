using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CityActivityRole : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__RoleId;

	private uint? __pbn__Affinity;

	private uint? __pbn__AffinityLevel;

	private bool? __pbn__IsLikeTypeGiftReceived;

	private bool? __pbn__IsFavouriteGiftReceived;

	[ProtoMember(1, Name = "role_id")]
	public uint RoleId
	{
		get
		{
			return __pbn__RoleId.GetValueOrDefault();
		}
		set
		{
			__pbn__RoleId = value;
		}
	}

	[ProtoMember(2, Name = "affinity")]
	public uint Affinity
	{
		get
		{
			return __pbn__Affinity.GetValueOrDefault();
		}
		set
		{
			__pbn__Affinity = value;
		}
	}

	[ProtoMember(3, Name = "affinity_level")]
	public uint AffinityLevel
	{
		get
		{
			return __pbn__AffinityLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__AffinityLevel = value;
		}
	}

	[ProtoMember(4, Name = "is_like_type_gift_received")]
	public bool IsLikeTypeGiftReceived
	{
		get
		{
			return __pbn__IsLikeTypeGiftReceived.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLikeTypeGiftReceived = value;
		}
	}

	[ProtoMember(5, Name = "is_favourite_gift_received")]
	public bool IsFavouriteGiftReceived
	{
		get
		{
			return __pbn__IsFavouriteGiftReceived.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFavouriteGiftReceived = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRoleId()
	{
		return __pbn__RoleId.HasValue;
	}

	public void ResetRoleId()
	{
		__pbn__RoleId = null;
	}

	public bool ShouldSerializeAffinity()
	{
		return __pbn__Affinity.HasValue;
	}

	public void ResetAffinity()
	{
		__pbn__Affinity = null;
	}

	public bool ShouldSerializeAffinityLevel()
	{
		return __pbn__AffinityLevel.HasValue;
	}

	public void ResetAffinityLevel()
	{
		__pbn__AffinityLevel = null;
	}

	public bool ShouldSerializeIsLikeTypeGiftReceived()
	{
		return __pbn__IsLikeTypeGiftReceived.HasValue;
	}

	public void ResetIsLikeTypeGiftReceived()
	{
		__pbn__IsLikeTypeGiftReceived = null;
	}

	public bool ShouldSerializeIsFavouriteGiftReceived()
	{
		return __pbn__IsFavouriteGiftReceived.HasValue;
	}

	public void ResetIsFavouriteGiftReceived()
	{
		__pbn__IsFavouriteGiftReceived = null;
	}
}
