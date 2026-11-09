using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldEndlessTrap : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ItemId;

	private uint? __pbn__BattleId;

	private uint? __pbn__UniqueId;

	private uint? __pbn__SetUid;

	[ProtoMember(1, Name = "item_id")]
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

	[ProtoMember(2, Name = "battle_id")]
	public uint BattleId
	{
		get
		{
			return __pbn__BattleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleId = value;
		}
	}

	[ProtoMember(3, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(4, Name = "set_uid")]
	public uint SetUid
	{
		get
		{
			return __pbn__SetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__SetUid = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeSetUid()
	{
		return __pbn__SetUid.HasValue;
	}

	public void ResetSetUid()
	{
		__pbn__SetUid = null;
	}
}
