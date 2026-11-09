using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InviteToMultiChatgroupReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2216
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private bool? __pbn__IsHasRelation;

	[ProtoMember(1, Name = "target_uid")]
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

	[ProtoMember(2, Name = "chatgroup_id_list")]
	public uint[] ChatgroupIdLists { get; set; }

	[ProtoMember(3, Name = "is_has_relation")]
	public bool IsHasRelation
	{
		get
		{
			return __pbn__IsHasRelation.GetValueOrDefault();
		}
		set
		{
			__pbn__IsHasRelation = value;
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

	public bool ShouldSerializeIsHasRelation()
	{
		return __pbn__IsHasRelation.HasValue;
	}

	public void ResetIsHasRelation()
	{
		__pbn__IsHasRelation = null;
	}
}
