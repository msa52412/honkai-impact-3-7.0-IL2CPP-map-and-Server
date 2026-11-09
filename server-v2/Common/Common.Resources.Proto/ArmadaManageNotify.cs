using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaManageNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2680
	}

	private IExtension __pbn__extensionData;

	private ArmadaManageNotifyType? __pbn__Type;

	private uint? __pbn__TargetUid;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(ArmadaManageNotifyType.ArmadaManageNotifyLeader)]
	public ArmadaManageNotifyType Type
	{
		get
		{
			return __pbn__Type ?? ArmadaManageNotifyType.ArmadaManageNotifyLeader;
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
}
