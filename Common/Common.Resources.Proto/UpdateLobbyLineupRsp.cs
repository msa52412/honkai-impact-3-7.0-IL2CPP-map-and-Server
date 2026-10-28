using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UpdateLobbyLineupRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 308
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_LOBBY")]
		NotInLobby,
		[ProtoEnum(Name = "AVATAR_NOT_EXIST")]
		AvatarNotExist,
		[ProtoEnum(Name = "STATUS_ERROR")]
		StatusError,
		[ProtoEnum(Name = "SERVER_BUSY")]
		ServerBusy,
		[ProtoEnum(Name = "ELF_NOT_EXIST")]
		ElfNotExist
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__AvatarTrialId;

	private uint? __pbn__ReclaimAvatarId;

	private uint? __pbn__ElfId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(3, Name = "avatar_trial_id")]
	public uint AvatarTrialId
	{
		get
		{
			return __pbn__AvatarTrialId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarTrialId = value;
		}
	}

	[ProtoMember(4, Name = "reclaim_avatar_id")]
	public uint ReclaimAvatarId
	{
		get
		{
			return __pbn__ReclaimAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__ReclaimAvatarId = value;
		}
	}

	[ProtoMember(5, Name = "elf_id")]
	public uint ElfId
	{
		get
		{
			return __pbn__ElfId.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfId = value;
		}
	}

	[ProtoMember(6, Name = "backup_avatar_id_list")]
	public uint[] BackupAvatarIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeAvatarTrialId()
	{
		return __pbn__AvatarTrialId.HasValue;
	}

	public void ResetAvatarTrialId()
	{
		__pbn__AvatarTrialId = null;
	}

	public bool ShouldSerializeReclaimAvatarId()
	{
		return __pbn__ReclaimAvatarId.HasValue;
	}

	public void ResetReclaimAvatarId()
	{
		__pbn__ReclaimAvatarId = null;
	}

	public bool ShouldSerializeElfId()
	{
		return __pbn__ElfId.HasValue;
	}

	public void ResetElfId()
	{
		__pbn__ElfId = null;
	}
}
