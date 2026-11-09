using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaSystemChatMsg : IExtensible
{
	private IExtension __pbn__extensionData;

	private ArmadaSystemChatMsgType? __pbn__Type;

	private uint? __pbn__Uid;

	private string __pbn__Nickname;

	private uint? __pbn__AvatarId;

	private uint? __pbn__DressId;

	private uint? __pbn__Time;

	private GachaType? __pbn__GachaType;

	private uint? __pbn__ItemId;

	private uint? __pbn__TargetAvatarId;

	private uint? __pbn__MissionId;

	private uint? __pbn__ArmadaLevel;

	private ArmadaCabinType? __pbn__CabinType;

	private uint? __pbn__CabinLevel;

	private ArmadaPosition? __pbn__Position;

	private uint? __pbn__TargetUid;

	private uint? __pbn__PoplCost;

	private string __pbn__OldName;

	private string __pbn__NewName;

	private string __pbn__TextMap;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(ArmadaSystemChatMsgType.ArmadaChatMsgGacha)]
	public ArmadaSystemChatMsgType Type
	{
		get
		{
			return __pbn__Type ?? ArmadaSystemChatMsgType.ArmadaChatMsgGacha;
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(3, Name = "nickname")]
	[DefaultValue("")]
	public string Nickname
	{
		get
		{
			return __pbn__Nickname ?? "";
		}
		set
		{
			__pbn__Nickname = value;
		}
	}

	[ProtoMember(4, Name = "avatar_id")]
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

	[ProtoMember(5, Name = "dress_id")]
	public uint DressId
	{
		get
		{
			return __pbn__DressId.GetValueOrDefault();
		}
		set
		{
			__pbn__DressId = value;
		}
	}

	[ProtoMember(6, Name = "time")]
	public uint Time
	{
		get
		{
			return __pbn__Time.GetValueOrDefault();
		}
		set
		{
			__pbn__Time = value;
		}
	}

	[ProtoMember(7, Name = "gacha_type")]
	[DefaultValue(GachaType.GachaHcoin)]
	public GachaType GachaType
	{
		get
		{
			return __pbn__GachaType ?? GachaType.GachaHcoin;
		}
		set
		{
			__pbn__GachaType = value;
		}
	}

	[ProtoMember(8, Name = "item_id")]
	public uint ItemId
	{
		get
		{
			return __pbn__ItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemId = value;
		}
	}

	[ProtoMember(9, Name = "target_avatar_id")]
	public uint TargetAvatarId
	{
		get
		{
			return __pbn__TargetAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetAvatarId = value;
		}
	}

	[ProtoMember(10, Name = "mission_id")]
	public uint MissionId
	{
		get
		{
			return __pbn__MissionId.GetValueOrDefault();
		}
		set
		{
			__pbn__MissionId = value;
		}
	}

	[ProtoMember(11, Name = "armada_level")]
	public uint ArmadaLevel
	{
		get
		{
			return __pbn__ArmadaLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ArmadaLevel = value;
		}
	}

	[ProtoMember(12, Name = "cabin_type")]
	[DefaultValue(ArmadaCabinType.ArmadaCabinNone)]
	public ArmadaCabinType CabinType
	{
		get
		{
			return __pbn__CabinType.GetValueOrDefault();
		}
		set
		{
			__pbn__CabinType = value;
		}
	}

	[ProtoMember(13, Name = "cabin_level")]
	public uint CabinLevel
	{
		get
		{
			return __pbn__CabinLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__CabinLevel = value;
		}
	}

	[ProtoMember(14, Name = "position")]
	[DefaultValue(ArmadaPosition.ArmadaPositionNone)]
	public ArmadaPosition Position
	{
		get
		{
			return __pbn__Position.GetValueOrDefault();
		}
		set
		{
			__pbn__Position = value;
		}
	}

	[ProtoMember(15, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
		}
	}

	[ProtoMember(16, Name = "popl_cost")]
	public uint PoplCost
	{
		get
		{
			return __pbn__PoplCost.GetValueOrDefault();
		}
		set
		{
			__pbn__PoplCost = value;
		}
	}

	[ProtoMember(17, Name = "old_name")]
	[DefaultValue("")]
	public string OldName
	{
		get
		{
			return __pbn__OldName ?? "";
		}
		set
		{
			__pbn__OldName = value;
		}
	}

	[ProtoMember(18, Name = "new_name")]
	[DefaultValue("")]
	public string NewName
	{
		get
		{
			return __pbn__NewName ?? "";
		}
		set
		{
			__pbn__NewName = value;
		}
	}

	[ProtoMember(19, Name = "text_map")]
	[DefaultValue("")]
	public string TextMap
	{
		get
		{
			return __pbn__TextMap ?? "";
		}
		set
		{
			__pbn__TextMap = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeDressId()
	{
		return __pbn__DressId.HasValue;
	}

	public void ResetDressId()
	{
		__pbn__DressId = null;
	}

	public bool ShouldSerializeTime()
	{
		return __pbn__Time.HasValue;
	}

	public void ResetTime()
	{
		__pbn__Time = null;
	}

	public bool ShouldSerializeGachaType()
	{
		return __pbn__GachaType.HasValue;
	}

	public void ResetGachaType()
	{
		__pbn__GachaType = null;
	}

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}

	public bool ShouldSerializeTargetAvatarId()
	{
		return __pbn__TargetAvatarId.HasValue;
	}

	public void ResetTargetAvatarId()
	{
		__pbn__TargetAvatarId = null;
	}

	public bool ShouldSerializeMissionId()
	{
		return __pbn__MissionId.HasValue;
	}

	public void ResetMissionId()
	{
		__pbn__MissionId = null;
	}

	public bool ShouldSerializeArmadaLevel()
	{
		return __pbn__ArmadaLevel.HasValue;
	}

	public void ResetArmadaLevel()
	{
		__pbn__ArmadaLevel = null;
	}

	public bool ShouldSerializeCabinType()
	{
		return __pbn__CabinType.HasValue;
	}

	public void ResetCabinType()
	{
		__pbn__CabinType = null;
	}

	public bool ShouldSerializeCabinLevel()
	{
		return __pbn__CabinLevel.HasValue;
	}

	public void ResetCabinLevel()
	{
		__pbn__CabinLevel = null;
	}

	public bool ShouldSerializePosition()
	{
		return __pbn__Position.HasValue;
	}

	public void ResetPosition()
	{
		__pbn__Position = null;
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}

	public bool ShouldSerializePoplCost()
	{
		return __pbn__PoplCost.HasValue;
	}

	public void ResetPoplCost()
	{
		__pbn__PoplCost = null;
	}

	public bool ShouldSerializeOldName()
	{
		return __pbn__OldName != null;
	}

	public void ResetOldName()
	{
		__pbn__OldName = null;
	}

	public bool ShouldSerializeNewName()
	{
		return __pbn__NewName != null;
	}

	public void ResetNewName()
	{
		__pbn__NewName = null;
	}

	public bool ShouldSerializeTextMap()
	{
		return __pbn__TextMap != null;
	}

	public void ResetTextMap()
	{
		__pbn__TextMap = null;
	}
}
