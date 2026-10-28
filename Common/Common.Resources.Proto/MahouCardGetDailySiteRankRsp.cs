using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardGetDailySiteRankRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7937
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId { get; set; }

	[ProtoMember(3, Name = "area_id")]
	public uint AreaId { get; set; }

	[ProtoMember(4, Name = "rank")]
	public uint Rank { get; set; }

	[ProtoMember(5, Name = "rank_percent")]
	public uint RankPercent { get; set; }

	[ProtoMember(6, Name = "rank_data")]
	public RankShowData RankData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
