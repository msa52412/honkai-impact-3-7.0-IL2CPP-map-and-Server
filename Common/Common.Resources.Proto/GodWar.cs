using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWar : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__GodWarId;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__MaxSupportPoint;

	private uint? __pbn__LobbyId;

	private uint? __pbn__CurChapterId;

	[ProtoMember(1, Name = "god_war_id")]
	public uint GodWarId
	{
		get
		{
			return __pbn__GodWarId.GetValueOrDefault();
		}
		set
		{
			__pbn__GodWarId = value;
		}
	}

	[ProtoMember(2, Name = "tale_list")]
	public List<GodWarTale> TaleLists { get; } = new List<GodWarTale>();


	[ProtoMember(3, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(4, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(5, Name = "talent_list")]
	public List<GodWarTalent> TalentLists { get; } = new List<GodWarTalent>();


	[ProtoMember(6, Name = "max_support_point")]
	public uint MaxSupportPoint
	{
		get
		{
			return __pbn__MaxSupportPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxSupportPoint = value;
		}
	}

	[ProtoMember(9, Name = "lobby_id")]
	public uint LobbyId
	{
		get
		{
			return __pbn__LobbyId.GetValueOrDefault();
		}
		set
		{
			__pbn__LobbyId = value;
		}
	}

	[ProtoMember(14, Name = "chapter_list")]
	public List<GodWarChapter> ChapterLists { get; } = new List<GodWarChapter>();


	[ProtoMember(15, Name = "cur_chapter_id")]
	public uint CurChapterId
	{
		get
		{
			return __pbn__CurChapterId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurChapterId = value;
		}
	}

	[ProtoMember(16, Name = "role_info")]
	public GodWarRoleInfo RoleInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGodWarId()
	{
		return __pbn__GodWarId.HasValue;
	}

	public void ResetGodWarId()
	{
		__pbn__GodWarId = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeMaxSupportPoint()
	{
		return __pbn__MaxSupportPoint.HasValue;
	}

	public void ResetMaxSupportPoint()
	{
		__pbn__MaxSupportPoint = null;
	}

	public bool ShouldSerializeLobbyId()
	{
		return __pbn__LobbyId.HasValue;
	}

	public void ResetLobbyId()
	{
		__pbn__LobbyId = null;
	}

	public bool ShouldSerializeCurChapterId()
	{
		return __pbn__CurChapterId.HasValue;
	}

	public void ResetCurChapterId()
	{
		__pbn__CurChapterId = null;
	}
}
