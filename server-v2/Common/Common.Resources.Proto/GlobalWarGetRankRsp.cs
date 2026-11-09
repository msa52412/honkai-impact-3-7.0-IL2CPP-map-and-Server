using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalWarGetRankRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3958
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PointId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "point_id")]
	public uint PointId
	{
		get
		{
			return __pbn__PointId.GetValueOrDefault();
		}
		set
		{
			__pbn__PointId = value;
		}
	}

	[ProtoMember(3, Name = "rank_data")]
	public RankShowData RankData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePointId()
	{
		return __pbn__PointId.HasValue;
	}

	public void ResetPointId()
	{
		__pbn__PointId = null;
	}
}
