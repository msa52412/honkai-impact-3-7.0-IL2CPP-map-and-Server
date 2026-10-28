using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DealArmadaApplyReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2609
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private bool? __pbn__Agree;

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

	[ProtoMember(2, Name = "agree")]
	public bool Agree
	{
		get
		{
			return __pbn__Agree.GetValueOrDefault();
		}
		set
		{
			__pbn__Agree = value;
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

	public bool ShouldSerializeAgree()
	{
		return __pbn__Agree.HasValue;
	}

	public void ResetAgree()
	{
		__pbn__Agree = null;
	}
}
