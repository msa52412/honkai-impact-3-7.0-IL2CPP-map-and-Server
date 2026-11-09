using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardBuff : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "buff_id")]
	public uint BuffId { get; set; }

	[ProtoMember(2, Name = "remain_room_cnt")]
	public uint RemainRoomCnt { get; set; }

	[ProtoMember(3, Name = "remain_battle_cnt")]
	public uint RemainBattleCnt { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
