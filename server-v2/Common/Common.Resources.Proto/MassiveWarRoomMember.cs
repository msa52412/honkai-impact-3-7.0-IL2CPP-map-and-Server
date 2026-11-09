using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MassiveWarRoomMember : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__Nickname;

	private uint? __pbn__ScoreRank;

	private uint? __pbn__PlayerGroupId;

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

	[ProtoMember(3, Name = "score_rank")]
	public uint ScoreRank
	{
		get
		{
			return __pbn__ScoreRank.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoreRank = value;
		}
	}

	[ProtoMember(4, Name = "player_group_id")]
	public uint PlayerGroupId
	{
		get
		{
			return __pbn__PlayerGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerGroupId = value;
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

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}

	public bool ShouldSerializeScoreRank()
	{
		return __pbn__ScoreRank.HasValue;
	}

	public void ResetScoreRank()
	{
		__pbn__ScoreRank = null;
	}

	public bool ShouldSerializePlayerGroupId()
	{
		return __pbn__PlayerGroupId.HasValue;
	}

	public void ResetPlayerGroupId()
	{
		__pbn__PlayerGroupId = null;
	}
}
