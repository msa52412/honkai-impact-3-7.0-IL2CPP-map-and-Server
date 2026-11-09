using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RogueTowerLevelupSupportAvatarReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4374
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__SupportAvatarId;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "support_avatar_id")]
	public uint SupportAvatarId
	{
		get
		{
			return __pbn__SupportAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__SupportAvatarId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeSupportAvatarId()
	{
		return __pbn__SupportAvatarId.HasValue;
	}

	public void ResetSupportAvatarId()
	{
		__pbn__SupportAvatarId = null;
	}
}
[ProtoContract]
public class RogueTowerLevelupSupportAvatarRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4375
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LACK_TIMES")]
		LackTimes,
		[ProtoEnum(Name = "MAX_LEVEL")]
		MaxLevel,
		[ProtoEnum(Name = "AVATAR_LOCKED")]
		AvatarLocked
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
