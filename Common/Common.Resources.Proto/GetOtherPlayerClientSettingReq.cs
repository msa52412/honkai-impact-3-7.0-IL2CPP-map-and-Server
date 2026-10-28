using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetOtherPlayerClientSettingReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1273
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private uint? __pbn__ClientSettingType;

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

	[ProtoMember(2, Name = "client_setting_type")]
	public uint ClientSettingType
	{
		get
		{
			return __pbn__ClientSettingType.GetValueOrDefault();
		}
		set
		{
			__pbn__ClientSettingType = value;
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

	public bool ShouldSerializeClientSettingType()
	{
		return __pbn__ClientSettingType.HasValue;
	}

	public void ResetClientSettingType()
	{
		__pbn__ClientSettingType = null;
	}
}
