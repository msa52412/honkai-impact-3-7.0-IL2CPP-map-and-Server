using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpRecordMember : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__AvatarId;

	private uint? __pbn__DressId;

	private bool? __pbn__IsMvp;

	private bool? __pbn__IsLeader;

	private string __pbn__NickName;

	private uint? __pbn__AvatarLevel;

	private uint? __pbn__AvatarStarLevel;

	private string __pbn__RegionName;

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

	[ProtoMember(3, Name = "dress_id")]
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

	[ProtoMember(4, Name = "is_mvp")]
	public bool IsMvp
	{
		get
		{
			return __pbn__IsMvp.GetValueOrDefault();
		}
		set
		{
			__pbn__IsMvp = value;
		}
	}

	[ProtoMember(5, Name = "is_leader")]
	public bool IsLeader
	{
		get
		{
			return __pbn__IsLeader.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLeader = value;
		}
	}

	[ProtoMember(6, Name = "nick_name")]
	[DefaultValue("")]
	public string NickName
	{
		get
		{
			return __pbn__NickName ?? "";
		}
		set
		{
			__pbn__NickName = value;
		}
	}

	[ProtoMember(7, Name = "avatar_level")]
	public uint AvatarLevel
	{
		get
		{
			return __pbn__AvatarLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarLevel = value;
		}
	}

	[ProtoMember(8, Name = "avatar_star_level")]
	public uint AvatarStarLevel
	{
		get
		{
			return __pbn__AvatarStarLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarStarLevel = value;
		}
	}

	[ProtoMember(9, Name = "region_name")]
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

	public bool ShouldSerializeIsMvp()
	{
		return __pbn__IsMvp.HasValue;
	}

	public void ResetIsMvp()
	{
		__pbn__IsMvp = null;
	}

	public bool ShouldSerializeIsLeader()
	{
		return __pbn__IsLeader.HasValue;
	}

	public void ResetIsLeader()
	{
		__pbn__IsLeader = null;
	}

	public bool ShouldSerializeNickName()
	{
		return __pbn__NickName != null;
	}

	public void ResetNickName()
	{
		__pbn__NickName = null;
	}

	public bool ShouldSerializeAvatarLevel()
	{
		return __pbn__AvatarLevel.HasValue;
	}

	public void ResetAvatarLevel()
	{
		__pbn__AvatarLevel = null;
	}

	public bool ShouldSerializeAvatarStarLevel()
	{
		return __pbn__AvatarStarLevel.HasValue;
	}

	public void ResetAvatarStarLevel()
	{
		__pbn__AvatarStarLevel = null;
	}

	public bool ShouldSerializeRegionName()
	{
		return __pbn__RegionName != null;
	}

	public void ResetRegionName()
	{
		__pbn__RegionName = null;
	}
}
