using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleEntity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EntityId;

	private QAvatarBattleEntityType? __pbn__Type;

	private uint? __pbn__EntityNameId;

	private ulong? __pbn__EntityExpireTimeMsU64;

	private uint? __pbn__OwnerId;

	[ProtoMember(1, Name = "entity_id")]
	public uint EntityId
	{
		get
		{
			return __pbn__EntityId.GetValueOrDefault();
		}
		set
		{
			__pbn__EntityId = value;
		}
	}

	[ProtoMember(2, Name = "type")]
	[DefaultValue(QAvatarBattleEntityType.QavatarBattleEntityTypeNone)]
	public QAvatarBattleEntityType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(3, Name = "entity_name_id")]
	public uint EntityNameId
	{
		get
		{
			return __pbn__EntityNameId.GetValueOrDefault();
		}
		set
		{
			__pbn__EntityNameId = value;
		}
	}

	[ProtoMember(4, Name = "entity_expire_time_ms_u64")]
	public ulong EntityExpireTimeMsU64
	{
		get
		{
			return __pbn__EntityExpireTimeMsU64.GetValueOrDefault();
		}
		set
		{
			__pbn__EntityExpireTimeMsU64 = value;
		}
	}

	[ProtoMember(5, Name = "component_set")]
	public QAvatarBattleComponentSet ComponentSet { get; set; }

	[ProtoMember(7, Name = "fight_info")]
	public QAvatarBattleFightInfo FightInfo { get; set; }

	[ProtoMember(8, Name = "owner_id")]
	public uint OwnerId
	{
		get
		{
			return __pbn__OwnerId.GetValueOrDefault();
		}
		set
		{
			__pbn__OwnerId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEntityId()
	{
		return __pbn__EntityId.HasValue;
	}

	public void ResetEntityId()
	{
		__pbn__EntityId = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeEntityNameId()
	{
		return __pbn__EntityNameId.HasValue;
	}

	public void ResetEntityNameId()
	{
		__pbn__EntityNameId = null;
	}

	public bool ShouldSerializeEntityExpireTimeMsU64()
	{
		return __pbn__EntityExpireTimeMsU64.HasValue;
	}

	public void ResetEntityExpireTimeMsU64()
	{
		__pbn__EntityExpireTimeMsU64 = null;
	}

	public bool ShouldSerializeOwnerId()
	{
		return __pbn__OwnerId.HasValue;
	}

	public void ResetOwnerId()
	{
		__pbn__OwnerId = null;
	}
}
