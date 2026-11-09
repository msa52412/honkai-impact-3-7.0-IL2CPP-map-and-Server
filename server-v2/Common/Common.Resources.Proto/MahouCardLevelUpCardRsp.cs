using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardLevelUpCardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7918
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "NOT_IN_SITE")]
		NotInSite,
		[ProtoEnum(Name = "NOT_IN_ROOM")]
		NotInRoom,
		[ProtoEnum(Name = "LEVEL_LIMIT")]
		LevelLimit,
		[ProtoEnum(Name = "COST_ERROR")]
		CostError,
		[ProtoEnum(Name = "TARGET_ERROR")]
		TargetError,
		[ProtoEnum(Name = "DAILY_SITE_FORBIDDEN")]
		DailySiteForbidden
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "cost_unique_id")]
	public uint CostUniqueId { get; set; }

	[ProtoMember(3, Name = "target_unique_id")]
	public uint TargetUniqueId { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
