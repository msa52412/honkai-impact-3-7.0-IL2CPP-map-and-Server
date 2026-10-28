using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SkipGodWarStoryNodeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6208
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "FEATURE_CLOSE")]
		FeatureClose,
		[ProtoEnum(Name = "NODE_NOT_EXIST")]
		NodeNotExist,
		[ProtoEnum(Name = "CHAPTER_MISMATCH")]
		ChapterMismatch,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "NODE_SKIP_INVALID")]
		NodeSkipInvalid,
		[ProtoEnum(Name = "MISSION_STATUS_ERROR")]
		MissionStatusError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GodWarId;

	private uint? __pbn__NodeId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "god_war_id")]
	public uint GodWarId
	{
		get
		{
			return __pbn__GodWarId.GetValueOrDefault();
		}
		set
		{
			__pbn__GodWarId = value;
		}
	}

	[ProtoMember(3, Name = "node_id")]
	public uint NodeId
	{
		get
		{
			return __pbn__NodeId.GetValueOrDefault();
		}
		set
		{
			__pbn__NodeId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGodWarId()
	{
		return __pbn__GodWarId.HasValue;
	}

	public void ResetGodWarId()
	{
		__pbn__GodWarId = null;
	}

	public bool ShouldSerializeNodeId()
	{
		return __pbn__NodeId.HasValue;
	}

	public void ResetNodeId()
	{
		__pbn__NodeId = null;
	}
}
