using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ConfirmRecommendGoodsRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1540
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

	private uint? __pbn__RecommendId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "recommend_id")]
	public uint RecommendId
	{
		get
		{
			return __pbn__RecommendId.GetValueOrDefault();
		}
		set
		{
			__pbn__RecommendId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRecommendId()
	{
		return __pbn__RecommendId.HasValue;
	}

	public void ResetRecommendId()
	{
		__pbn__RecommendId = null;
	}
}
