using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncTvtBattleInfoNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3350
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "battle_info")]
	public TvtBattleInfo BattleInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
