using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldBattleCollectItemRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2426
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_CHATWORLD")]
		NotInChatworld,
		[ProtoEnum(Name = "NOT_EXIST")]
		NotExist,
		[ProtoEnum(Name = "CAMP_INVALID")]
		CampInvalid,
		[ProtoEnum(Name = "LACK_TIMES")]
		LackTimes,
		[ProtoEnum(Name = "ITEM_NOT_USED")]
		ItemNotUsed,
		[ProtoEnum(Name = "ITEM_LIMIT")]
		ItemLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ItemId;

	private uint? __pbn__ItemUniqueId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "item_id")]
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

	[ProtoMember(3, Name = "item_unique_id")]
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

	[ProtoMember(4, Name = "reward_data")]
	public RewardData RewardData { get; set; }

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
