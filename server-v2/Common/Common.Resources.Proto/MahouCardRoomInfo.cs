using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardRoomInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "room_id")]
	public uint RoomId { get; set; }

	[ProtoMember(2, Name = "battle_random_seed")]
	public uint BattleRandomSeed { get; set; }

	[ProtoMember(3, Name = "transaction_str")]
	[DefaultValue("")]
	public string TransactionStr { get; set; } = "";


	[ProtoMember(4, Name = "retry_times")]
	public uint RetryTimes { get; set; }

	[ProtoMember(5, Name = "is_done")]
	public bool IsDone { get; set; }

	[ProtoMember(6, Name = "is_in_battle")]
	public bool IsInBattle { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
