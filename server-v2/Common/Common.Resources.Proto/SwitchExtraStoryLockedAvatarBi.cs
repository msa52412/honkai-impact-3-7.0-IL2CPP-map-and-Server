using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SwitchExtraStoryLockedAvatarBindEquipReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1002
	}

	private IExtension __pbn__extensionData;

	private AvatarBindEquipMode? __pbn__Mode;

	[ProtoMember(1, Name = "mode")]
	[DefaultValue(AvatarBindEquipMode.AvatarBindEquipCommon)]
	public AvatarBindEquipMode Mode
	{
		get
		{
			return __pbn__Mode.GetValueOrDefault();
		}
		set
		{
			__pbn__Mode = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMode()
	{
		return __pbn__Mode.HasValue;
	}

	public void ResetMode()
	{
		__pbn__Mode = null;
	}
}
[ProtoContract]
public class SwitchExtraStoryLockedAvatarBindEquipRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1003
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

	private uint? __pbn__SuccCnt;

	private AvatarBindEquipMode? __pbn__Mode;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "succ_cnt")]
	public uint SuccCnt
	{
		get
		{
			return __pbn__SuccCnt.GetValueOrDefault();
		}
		set
		{
			__pbn__SuccCnt = value;
		}
	}

	[ProtoMember(3, Name = "mode")]
	[DefaultValue(AvatarBindEquipMode.AvatarBindEquipCommon)]
	public AvatarBindEquipMode Mode
	{
		get
		{
			return __pbn__Mode.GetValueOrDefault();
		}
		set
		{
			__pbn__Mode = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSuccCnt()
	{
		return __pbn__SuccCnt.HasValue;
	}

	public void ResetSuccCnt()
	{
		__pbn__SuccCnt = null;
	}

	public bool ShouldSerializeMode()
	{
		return __pbn__Mode.HasValue;
	}

	public void ResetMode()
	{
		__pbn__Mode = null;
	}
}
