using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtBattleRobotSettleNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3363
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "battle_result")]
	public TvtBattleResult BattleResult { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
