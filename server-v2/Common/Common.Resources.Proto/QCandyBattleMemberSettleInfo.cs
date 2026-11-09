using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleMemberSettleInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__Nickname;

	private uint? __pbn__AvatarId;

	private uint? __pbn__CurRankScore;

	private bool? __pbn__IsAbandoned;

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

	[ProtoMember(2, Name = "nickname")]
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

	[ProtoMember(3, Name = "avatar_id")]
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

	[ProtoMember(4, Name = "cur_rank_score")]
	public uint CurRankScore
	{
		get
		{
			return __pbn__CurRankScore.GetValueOrDefault();
		}
		set
		{
			__pbn__CurRankScore = value;
		}
	}

	[ProtoMember(5, Name = "is_abandoned")]
	public bool IsAbandoned
	{
		get
		{
			return __pbn__IsAbandoned.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAbandoned = value;
		}
	}

	[ProtoMember(6, Name = "settle_info")]
	public QCandyBattleSettleInfo SettleInfo { get; set; }

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

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeCurRankScore()
	{
		return __pbn__CurRankScore.HasValue;
	}

	public void ResetCurRankScore()
	{
		__pbn__CurRankScore = null;
	}

	public bool ShouldSerializeIsAbandoned()
	{
		return __pbn__IsAbandoned.HasValue;
	}

	public void ResetIsAbandoned()
	{
		__pbn__IsAbandoned = null;
	}
}
