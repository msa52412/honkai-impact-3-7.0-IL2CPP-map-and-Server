using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldBattleCollectItemReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2425
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ItemId;

	private uint? __pbn__ItemUniqueId;

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

	[ProtoMember(2, Name = "item_unique_id")]
	public uint ItemUniqueId
	{
		get
		{
			return __pbn__ItemUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemUniqueId = value;
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

	public bool ShouldSerializeItemUniqueId()
	{
		return __pbn__ItemUniqueId.HasValue;
	}

	public void ResetItemUniqueId()
	{
		__pbn__ItemUniqueId = null;
	}
}
