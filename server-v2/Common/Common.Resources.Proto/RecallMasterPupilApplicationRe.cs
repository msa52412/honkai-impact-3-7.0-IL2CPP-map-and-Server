using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RecallMasterPupilApplicationReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 970
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private string __pbn__SelfNickname;

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

	[ProtoMember(2, Name = "self_nickname")]
	[DefaultValue("")]
	public string SelfNickname
	{
		get
		{
			return __pbn__SelfNickname ?? "";
		}
		set
		{
			__pbn__SelfNickname = value;
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

	public bool ShouldSerializeSelfNickname()
	{
		return __pbn__SelfNickname != null;
	}

	public void ResetSelfNickname()
	{
		__pbn__SelfNickname = null;
	}
}
