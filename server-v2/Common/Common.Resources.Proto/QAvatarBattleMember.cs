using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleMember : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__RegionName;

	private string __pbn__Nickname;

	private uint? __pbn__Level;

	private uint? __pbn__FrameId;

	private uint? __pbn__CustomHeadId;

	private uint? __pbn__Division;

	private uint? __pbn__EloScore;

	private uint? __pbn__CampId;

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

	[ProtoMember(5, Name = "frame_id")]
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

	[ProtoMember(6, Name = "custom_head_id")]
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

	[ProtoMember(7, Name = "division")]
	public uint Division
	{
		get
		{
			return __pbn__Division.GetValueOrDefault();
		}
		set
		{
			__pbn__Division = value;
		}
	}

	[ProtoMember(8, Name = "elo_score")]
	public uint EloScore
	{
		get
		{
			return __pbn__EloScore.GetValueOrDefault();
		}
		set
		{
			__pbn__EloScore = value;
		}
	}

	[ProtoMember(9, Name = "choose_virtual_avatar")]
	public VirtualAvatar ChooseVirtualAvatar { get; set; }

	[ProtoMember(10, Name = "camp_id")]
	public uint CampId
	{
		get
		{
			return __pbn__CampId.GetValueOrDefault();
		}
		set
		{
			__pbn__CampId = value;
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

	public bool ShouldSerializeDivision()
	{
		return __pbn__Division.HasValue;
	}

	public void ResetDivision()
	{
		__pbn__Division = null;
	}

	public bool ShouldSerializeEloScore()
	{
		return __pbn__EloScore.HasValue;
	}

	public void ResetEloScore()
	{
		__pbn__EloScore = null;
	}

	public bool ShouldSerializeCampId()
	{
		return __pbn__CampId.HasValue;
	}

	public void ResetCampId()
	{
		__pbn__CampId = null;
	}
}
