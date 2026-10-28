using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardHandCardChangeNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7912
	}

	[ProtoContract]
	public enum Reason
	{
		[ProtoEnum(Name = "NONE")]
		None,
		[ProtoEnum(Name = "DRAW")]
		Draw,
		[ProtoEnum(Name = "EXCHANGE")]
		Exchange,
		[ProtoEnum(Name = "DISCARD")]
		Discard,
		[ProtoEnum(Name = "CARD_LEVEL_UP")]
		CardLevelUp,
		[ProtoEnum(Name = "SET_TO_BATTLE")]
		SetToBattle,
		[ProtoEnum(Name = "RECALL_FROM_BATTLE")]
		RecallFromBattle,
		[ProtoEnum(Name = "EVENT")]
		Event,
		[ProtoEnum(Name = "GROWTH")]
		Growth
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId { get; set; }

	[ProtoMember(2, Name = "site_id")]
	public uint SiteId { get; set; }

	[ProtoMember(3, Name = "room_id")]
	public uint RoomId { get; set; }

	[ProtoMember(4)]
	public Reason reason { get; set; }

	[ProtoMember(5, Name = "lost_hand_card_list")]
	public List<MahouCardHandCard> LostHandCardLists { get; } = new List<MahouCardHandCard>();


	[ProtoMember(6, Name = "add_hand_card_list")]
	public List<MahouCardHandCard> AddHandCardLists { get; } = new List<MahouCardHandCard>();


	[ProtoMember(7, Name = "lost_battle_card_list")]
	public List<MahouCardBattleCard> LostBattleCardLists { get; } = new List<MahouCardBattleCard>();


	[ProtoMember(8, Name = "add_battle_card_list")]
	public List<MahouCardBattleCard> AddBattleCardLists { get; } = new List<MahouCardBattleCard>();


	[ProtoMember(9, Name = "event_id")]
	public uint EventId { get; set; }

	[ProtoMember(10, Name = "is_need_sync")]
	public bool IsNeedSync { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
