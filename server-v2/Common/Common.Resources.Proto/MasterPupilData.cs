using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MasterPupilData : IExtensible
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

	private MasterPupilStatus? __pbn__Status;

	private uint? __pbn__RelationTime;

	private uint? __pbn__Score;

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

	[ProtoMember(10, Name = "status")]
	[DefaultValue(MasterPupilStatus.MasterPupilStatusNone)]
	public MasterPupilStatus Status
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

	[ProtoMember(11, Name = "relation_time")]
	public uint RelationTime
	{
		get
		{
			return __pbn__RelationTime.GetValueOrDefault();
		}
		set
		{
			__pbn__RelationTime = value;
		}
	}

	[ProtoMember(12, Name = "brief_data")]
	public PlayerFriendBriefData BriefData { get; set; }

	[ProtoMember(13, Name = "score")]
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

	[ProtoMember(14, Name = "is_logoff")]
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

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeRelationTime()
	{
		return __pbn__RelationTime.HasValue;
	}

	public void ResetRelationTime()
	{
		__pbn__RelationTime = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
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
