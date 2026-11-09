using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncOpenworldBossHuntNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4530
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "boss_hunt")]
	public OpenworldBossHunt BossHunt { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
