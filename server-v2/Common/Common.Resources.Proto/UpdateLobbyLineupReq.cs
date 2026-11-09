using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UpdateLobbyLineupReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 307
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__AvatarTrialId;

	private uint? __pbn__ReclaimAvatarId;

	private uint? __pbn__ElfId;

	private uint? __pbn__ScDlcAvatarId;

	[ProtoMember(1, Name = "avatar_id")]
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

	[ProtoMember(2, Name = "member_info")]
	public MpTeamMemberInfo MemberInfo { get; set; }

	[ProtoMember(4, Name = "avatar_trial_id")]
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

	[ProtoMember(5, Name = "reclaim_avatar_id")]
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

	[ProtoMember(6, Name = "reclaim_avatar_info")]
	public ReclaimMpAvatar ReclaimAvatarInfo { get; set; }

	[ProtoMember(7, Name = "elf_id")]
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

	[ProtoMember(8, Name = "backup_avatar_id_list")]
	public uint[] BackupAvatarIdLists { get; set; }

	[ProtoMember(9, Name = "sc_dlc_avatar_id")]
	public uint ScDlcAvatarId
	{
		get
		{
			return __pbn__ScDlcAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScDlcAvatarId = value;
		}
	}

	[ProtoMember(10, Name = "sc_dlc_avatar_info")]
	public ScDLCMpAvatar ScDlcAvatarInfo { get; set; }

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

	public bool ShouldSerializeScDlcAvatarId()
	{
		return __pbn__ScDlcAvatarId.HasValue;
	}

	public void ResetScDlcAvatarId()
	{
		__pbn__ScDlcAvatarId = null;
	}
}
