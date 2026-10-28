using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpGetLobbyRecommendRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 471
	}

	[ProtoContract]
	public enum RecommendType
	{
		[ProtoEnum(Name = "RECOMMEND_NONE")]
		RecommendNone,
		[ProtoEnum(Name = "RECOMMEND_CREATE")]
		RecommendCreate,
		[ProtoEnum(Name = "RECOMMEND_ENTER")]
		RecommendEnter
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

	private uint? __pbn__StageId;

	private RecommendType? __pbn__recommend_type;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(3)]
	[DefaultValue(RecommendType.RecommendNone)]
	public RecommendType recommend_type
	{
		get
		{
			return __pbn__recommend_type.GetValueOrDefault();
		}
		set
		{
			__pbn__recommend_type = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializerecommend_type()
	{
		return __pbn__recommend_type.HasValue;
	}

	public void Resetrecommend_type()
	{
		__pbn__recommend_type = null;
	}
}
