using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaPlayerNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2681
	}

	private IExtension __pbn__extensionData;

	private ArmadaPlayerNotifyType? __pbn__Type;

	private uint? __pbn__TargetUid;

	private string __pbn__NewName;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(ArmadaPlayerNotifyType.ArmadaPlayerNotifyJoin)]
	public ArmadaPlayerNotifyType Type
	{
		get
		{
			return __pbn__Type ?? ArmadaPlayerNotifyType.ArmadaPlayerNotifyJoin;
		}
		set
		{
			__pbn__Type = value;
		}
	}

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

	[ProtoMember(3, Name = "member")]
	public ArmadaMember Member { get; set; }

	[ProtoMember(5, Name = "member_list")]
	public uint[] MemberLists { get; set; }

	[ProtoMember(6, Name = "new_name")]
	[DefaultValue("")]
	public string NewName
	{
		get
		{
			return __pbn__NewName ?? "";
		}
		set
		{
			__pbn__NewName = value;
		}
	}

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

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}

	public bool ShouldSerializeNewName()
	{
		return __pbn__NewName != null;
	}

	public void ResetNewName()
	{
		__pbn__NewName = null;
	}
}
