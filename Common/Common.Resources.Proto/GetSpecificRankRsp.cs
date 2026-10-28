using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetSpecificRankRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1685
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

	private SpecificRankType? __pbn__Type;

	private uint? __pbn__Param;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "type")]
	[DefaultValue(SpecificRankType.SpecificRankNone)]
	public SpecificRankType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(3, Name = "param")]
	public uint Param
	{
		get
		{
			return __pbn__Param.GetValueOrDefault();
		}
		set
		{
			__pbn__Param = value;
		}
	}

	[ProtoMember(4, Name = "rank_data")]
	public RankShowData RankData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeParam()
	{
		return __pbn__Param.HasValue;
	}

	public void ResetParam()
	{
		__pbn__Param = null;
	}
}
