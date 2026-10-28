using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivityCheckInOptionalReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6474
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BuildingUniqueId;

	private uint? __pbn__AvatarId;

	[ProtoMember(1, Name = "head")]
	public TownCommonHead Head { get; set; }

	[ProtoMember(2, Name = "building_unique_id")]
	public uint BuildingUniqueId
	{
		get
		{
			return __pbn__BuildingUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuildingUniqueId = value;
		}
	}

	[ProtoMember(3, Name = "avatar_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBuildingUniqueId()
	{
		return __pbn__BuildingUniqueId.HasValue;
	}

	public void ResetBuildingUniqueId()
	{
		__pbn__BuildingUniqueId = null;
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}
}
