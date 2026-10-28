using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardBattleEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7932
	}

	[ProtoContract]
	public enum Status
	{
		[ProtoEnum(Name = "BATTLE_LOSE")]
		BattleLose,
		[ProtoEnum(Name = "BATTLE_WIN")]
		BattleWin,
		[ProtoEnum(Name = "BATTLE_LOSE_AND_RETRY")]
		BattleLoseAndRetry
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId { get; set; }

	[ProtoMember(2, Name = "site_id")]
	public uint SiteId { get; set; }

	[ProtoMember(3, Name = "room_id")]
	public uint RoomId { get; set; }

	[ProtoMember(4, Name = "score")]
	public uint Score { get; set; }

	[ProtoMember(5, Name = "end_status")]
	public Status EndStatus { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
