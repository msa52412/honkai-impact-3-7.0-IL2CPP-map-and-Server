using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PublishGratuityStageRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4115
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_ACCEPTOR")]
		NotAcceptor
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private uint? __pbn__StageUniqueId;

	private uint? __pbn__RelationTag;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
		}
	}

	[ProtoMember(3, Name = "stage_unique_id")]
	public uint StageUniqueId
	{
		get
		{
			return __pbn__StageUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageUniqueId = value;
		}
	}

	[ProtoMember(4, Name = "relation_tag")]
	public uint RelationTag
	{
		get
		{
			return __pbn__RelationTag.GetValueOrDefault();
		}
		set
		{
			__pbn__RelationTag = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}

	public bool ShouldSerializeStageUniqueId()
	{
		return __pbn__StageUniqueId.HasValue;
	}

	public void ResetStageUniqueId()
	{
		__pbn__StageUniqueId = null;
	}

	public bool ShouldSerializeRelationTag()
	{
		return __pbn__RelationTag.HasValue;
	}

	public void ResetRelationTag()
	{
		__pbn__RelationTag = null;
	}
}
