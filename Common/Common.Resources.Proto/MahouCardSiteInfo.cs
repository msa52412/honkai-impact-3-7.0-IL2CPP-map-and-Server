using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardSiteInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "site_id")]
	public uint SiteId { get; set; }

	[ProtoMember(2, Name = "role_id")]
	public uint RoleId { get; set; }

	[ProtoMember(3, Name = "role_level")]
	public uint RoleLevel { get; set; }

	[ProtoMember(4, Name = "room_info")]
	public MahouCardRoomInfo RoomInfo { get; set; }

	[ProtoMember(5, Name = "passed_room_id_list", IsPacked = true)]
	public uint[] PassedRoomIdLists { get; set; }

	[ProtoMember(6, Name = "transaction_str")]
	[DefaultValue("")]
	public string TransactionStr { get; set; } = "";


	[ProtoMember(7, Name = "hand_card_list")]
	public List<MahouCardHandCard> HandCardLists { get; } = new List<MahouCardHandCard>();


	[ProtoMember(8, Name = "battle_card_list")]
	public List<MahouCardBattleCard> BattleCardLists { get; } = new List<MahouCardBattleCard>();


	[ProtoMember(9, Name = "battle_hp")]
	public uint BattleHp { get; set; }

	[ProtoMember(10, Name = "remain_exchange_times")]
	public uint RemainExchangeTimes { get; set; }

	[ProtoMember(11, Name = "discard_times")]
	public uint DiscardTimes { get; set; }

	[ProtoMember(12, Name = "discard_goal")]
	public uint DiscardGoal { get; set; }

	[ProtoMember(13, Name = "discard_trigger_times")]
	public uint DiscardTriggerTimes { get; set; }

	[ProtoMember(14, Name = "exchange_trigger_times")]
	public uint ExchangeTriggerTimes { get; set; }

	[ProtoMember(15, Name = "wait_select_prophesy_list", IsPacked = true)]
	public uint[] WaitSelectProphesyLists { get; set; }

	[ProtoMember(16, Name = "prophesy_list", IsPacked = true)]
	public uint[] ProphesyLists { get; set; }

	[ProtoMember(17, Name = "role_prophesy_list", IsPacked = true)]
	public uint[] RoleProphesyLists { get; set; }

	[ProtoMember(18, Name = "buff_list")]
	public List<MahouCardBuff> BuffLists { get; } = new List<MahouCardBuff>();


	[ProtoMember(19, Name = "cur_event_id")]
	public uint CurEventId { get; set; }

	[ProtoMember(20, Name = "option_list", IsPacked = true)]
	public uint[] OptionLists { get; set; }

	[ProtoMember(21, Name = "invalid_option_list", IsPacked = true)]
	public uint[] InvalidOptionLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
