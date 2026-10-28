using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CheckExtraStoryLockedAvatarBindEquipReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1000
	}

	private IExtension __pbn__extensionData;

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
[ProtoContract]
public class CheckExtraStoryLockedAvatarBindEquipRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1001
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

	private bool? __pbn__NeedSwitch;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "need_switch")]
	public bool NeedSwitch
	{
		get
		{
			return __pbn__NeedSwitch.GetValueOrDefault();
		}
		set
		{
			__pbn__NeedSwitch = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeNeedSwitch()
	{
		return __pbn__NeedSwitch.HasValue;
	}

	public void ResetNeedSwitch()
	{
		__pbn__NeedSwitch = null;
	}
}
