using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MasterPupilApplyMsg : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private MasterPupilType? __pbn__Type;

	private uint? __pbn__Level;

	private string __pbn__Nickname;

	private uint? __pbn__TeacherRank;

	private uint? __pbn__LastOnlineTime;

	private uint? __pbn__AvatarId;

	private uint? __pbn__DressId;

	private uint? __pbn__Score;

	private uint? __pbn__FrameId;

	private uint? __pbn__CustomHeadId;

	private uint? __pbn__LastLogoutTime;

	private uint? __pbn__OnlineStatus;

	private bool? __pbn__IsLogoff;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "type")]
	[DefaultValue(MasterPupilType.MasterPupilNoneType)]
	public MasterPupilType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(3, Name = "card")]
	public MasterPupilCard Card { get; set; }

	[ProtoMember(4, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(5, Name = "nickname")]
	[DefaultValue("")]
	public string Nickname
	{
		get
		{
			return __pbn__Nickname ?? "";
		}
		set
		{
			__pbn__Nickname = value;
		}
	}

	[ProtoMember(6, Name = "teacher_rank")]
	public uint TeacherRank
	{
		get
		{
			return __pbn__TeacherRank.GetValueOrDefault();
		}
		set
		{
			__pbn__TeacherRank = value;
		}
	}

	[ProtoMember(7, Name = "last_online_time")]
	public uint LastOnlineTime
	{
		get
		{
			return __pbn__LastOnlineTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastOnlineTime = value;
		}
	}

	[ProtoMember(8, Name = "avatar_id")]
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

	[ProtoMember(9, Name = "dress_id")]
	public uint DressId
	{
		get
		{
			return __pbn__DressId.GetValueOrDefault();
		}
		set
		{
			__pbn__DressId = value;
		}
	}

	[ProtoMember(10, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(11, Name = "frame_id")]
	public uint FrameId
	{
		get
		{
			return __pbn__FrameId.GetValueOrDefault();
		}
		set
		{
			__pbn__FrameId = value;
		}
	}

	[ProtoMember(12, Name = "custom_head_id")]
	public uint CustomHeadId
	{
		get
		{
			return __pbn__CustomHeadId.GetValueOrDefault();
		}
		set
		{
			__pbn__CustomHeadId = value;
		}
	}

	[ProtoMember(13, Name = "last_logout_time")]
	public uint LastLogoutTime
	{
		get
		{
			return __pbn__LastLogoutTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastLogoutTime = value;
		}
	}

	[ProtoMember(14, Name = "online_status")]
	public uint OnlineStatus
	{
		get
		{
			return __pbn__OnlineStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__OnlineStatus = value;
		}
	}

	[ProtoMember(15, Name = "is_logoff")]
	public bool IsLogoff
	{
		get
		{
			return __pbn__IsLogoff.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLogoff = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}

	public bool ShouldSerializeTeacherRank()
	{
		return __pbn__TeacherRank.HasValue;
	}

	public void ResetTeacherRank()
	{
		__pbn__TeacherRank = null;
	}

	public bool ShouldSerializeLastOnlineTime()
	{
		return __pbn__LastOnlineTime.HasValue;
	}

	public void ResetLastOnlineTime()
	{
		__pbn__LastOnlineTime = null;
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeDressId()
	{
		return __pbn__DressId.HasValue;
	}

	public void ResetDressId()
	{
		__pbn__DressId = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeFrameId()
	{
		return __pbn__FrameId.HasValue;
	}

	public void ResetFrameId()
	{
		__pbn__FrameId = null;
	}

	public bool ShouldSerializeCustomHeadId()
	{
		return __pbn__CustomHeadId.HasValue;
	}

	public void ResetCustomHeadId()
	{
		__pbn__CustomHeadId = null;
	}

	public bool ShouldSerializeLastLogoutTime()
	{
		return __pbn__LastLogoutTime.HasValue;
	}

	public void ResetLastLogoutTime()
	{
		__pbn__LastLogoutTime = null;
	}

	public bool ShouldSerializeOnlineStatus()
	{
		return __pbn__OnlineStatus.HasValue;
	}

	public void ResetOnlineStatus()
	{
		__pbn__OnlineStatus = null;
	}

	public bool ShouldSerializeIsLogoff()
	{
		return __pbn__IsLogoff.HasValue;
	}

	public void ResetIsLogoff()
	{
		__pbn__IsLogoff = null;
	}
}
