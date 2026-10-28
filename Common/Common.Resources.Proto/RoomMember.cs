using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RoomMember : IExtensible
{
	[ProtoContract]
	public enum MemberPosition
	{
		[ProtoEnum(Name = "NORMAL")]
		Normal = 1,
		[ProtoEnum(Name = "LEADER")]
		Leader
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__RegionName;

	private string __pbn__Nickname;

	private uint? __pbn__Level;

	private uint? __pbn__HeadAvatarId;

	private uint? __pbn__FrameId;

	private uint? __pbn__DressId;

	private RoomMemberStatus? __pbn__Status;

	private MemberPosition? __pbn__Position;

	private byte[] __pbn__ModeBytes;

	private uint? __pbn__CustomHeadId;

	private RoomClientStatus? __pbn__ClientStatus;

	private uint? __pbn__VoiceId;

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

	[ProtoMember(2, Name = "region_name")]
	[DefaultValue("")]
	public string RegionName
	{
		get
		{
			return __pbn__RegionName ?? "";
		}
		set
		{
			__pbn__RegionName = value;
		}
	}

	[ProtoMember(3, Name = "nickname")]
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

	[ProtoMember(5, Name = "head_avatar_id")]
	public uint HeadAvatarId
	{
		get
		{
			return __pbn__HeadAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__HeadAvatarId = value;
		}
	}

	[ProtoMember(6, Name = "frame_id")]
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

	[ProtoMember(7, Name = "dress_id")]
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

	[ProtoMember(8, Name = "status")]
	[DefaultValue(RoomMemberStatus.RoomUnready)]
	public RoomMemberStatus Status
	{
		get
		{
			return __pbn__Status ?? RoomMemberStatus.RoomUnready;
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(9, Name = "position")]
	[DefaultValue(MemberPosition.Normal)]
	public MemberPosition Position
	{
		get
		{
			return __pbn__Position ?? MemberPosition.Normal;
		}
		set
		{
			__pbn__Position = value;
		}
	}

	[ProtoMember(10, Name = "mode_bytes")]
	public byte[] ModeBytes
	{
		get
		{
			return __pbn__ModeBytes;
		}
		set
		{
			__pbn__ModeBytes = value;
		}
	}

	[ProtoMember(11, Name = "custom_head_id")]
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

	[ProtoMember(12, Name = "client_status")]
	[DefaultValue(RoomClientStatus.RoomClientNone)]
	public RoomClientStatus ClientStatus
	{
		get
		{
			return __pbn__ClientStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__ClientStatus = value;
		}
	}

	[ProtoMember(13, Name = "voice_id")]
	public uint VoiceId
	{
		get
		{
			return __pbn__VoiceId.GetValueOrDefault();
		}
		set
		{
			__pbn__VoiceId = value;
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

	public bool ShouldSerializeRegionName()
	{
		return __pbn__RegionName != null;
	}

	public void ResetRegionName()
	{
		__pbn__RegionName = null;
	}

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeHeadAvatarId()
	{
		return __pbn__HeadAvatarId.HasValue;
	}

	public void ResetHeadAvatarId()
	{
		__pbn__HeadAvatarId = null;
	}

	public bool ShouldSerializeFrameId()
	{
		return __pbn__FrameId.HasValue;
	}

	public void ResetFrameId()
	{
		__pbn__FrameId = null;
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

	public bool ShouldSerializePosition()
	{
		return __pbn__Position.HasValue;
	}

	public void ResetPosition()
	{
		__pbn__Position = null;
	}

	public bool ShouldSerializeModeBytes()
	{
		return __pbn__ModeBytes != null;
	}

	public void ResetModeBytes()
	{
		__pbn__ModeBytes = null;
	}

	public bool ShouldSerializeCustomHeadId()
	{
		return __pbn__CustomHeadId.HasValue;
	}

	public void ResetCustomHeadId()
	{
		__pbn__CustomHeadId = null;
	}

	public bool ShouldSerializeClientStatus()
	{
		return __pbn__ClientStatus.HasValue;
	}

	public void ResetClientStatus()
	{
		__pbn__ClientStatus = null;
	}

	public bool ShouldSerializeVoiceId()
	{
		return __pbn__VoiceId.HasValue;
	}

	public void ResetVoiceId()
	{
		__pbn__VoiceId = null;
	}
}
