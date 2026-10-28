using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetGachaProbRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4709
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

	private uint? __pbn__GachaType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "gacha_type")]
	public uint GachaType
	{
		get
		{
			return __pbn__GachaType.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaType = value;
		}
	}

	[ProtoMember(3, Name = "total_prob_list")]
	public List<GachaTotalProb> TotalProbLists { get; } = new List<GachaTotalProb>();


	[ProtoMember(4, Name = "detail_prob_list")]
	public List<GachaDetailProb> DetailProbLists { get; } = new List<GachaDetailProb>();


	[ProtoMember(5, Name = "baodi_prob_list")]
	public List<GachaDetailProb> BaodiProbLists { get; } = new List<GachaDetailProb>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGachaType()
	{
		return __pbn__GachaType.HasValue;
	}

	public void ResetGachaType()
	{
		__pbn__GachaType = null;
	}
}
