using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetSecurityPasswordRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5022
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private SecurityPasswordStatus? __pbn__Status;

	private SecurityPasswordDeviceStatus? __pbn__DeviceStatus;

	private uint? __pbn__ResetEndTime;

	private string __pbn__Salt;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "status")]
	[DefaultValue(SecurityPasswordStatus.SecurityPasswordNotSet)]
	public SecurityPasswordStatus Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(3, Name = "device_status")]
	[DefaultValue(SecurityPasswordDeviceStatus.SecurityPasswordDeviceUnlocked)]
	public SecurityPasswordDeviceStatus DeviceStatus
	{
		get
		{
			return __pbn__DeviceStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__DeviceStatus = value;
		}
	}

	[ProtoMember(4, Name = "reset_end_time")]
	public uint ResetEndTime
	{
		get
		{
			return __pbn__ResetEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ResetEndTime = value;
		}
	}

	[ProtoMember(5, Name = "salt")]
	[DefaultValue("")]
	public string Salt
	{
		get
		{
			return __pbn__Salt ?? "";
		}
		set
		{
			__pbn__Salt = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeDeviceStatus()
	{
		return __pbn__DeviceStatus.HasValue;
	}

	public void ResetDeviceStatus()
	{
		__pbn__DeviceStatus = null;
	}

	public bool ShouldSerializeResetEndTime()
	{
		return __pbn__ResetEndTime.HasValue;
	}

	public void ResetResetEndTime()
	{
		__pbn__ResetEndTime = null;
	}

	public bool ShouldSerializeSalt()
	{
		return __pbn__Salt != null;
	}

	public void ResetSalt()
	{
		__pbn__Salt = null;
	}
}
