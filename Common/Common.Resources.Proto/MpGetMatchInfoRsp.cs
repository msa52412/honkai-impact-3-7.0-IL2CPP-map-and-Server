using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpGetMatchInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 467
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_MATCH_ROOM")]
		NoMatchRoom
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleBeginTime;

	private uint? __pbn__LobbyIdx;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "match_info_list")]
	public List<MpMatchInfo> MatchInfoLists { get; } = new List<MpMatchInfo>();


	[ProtoMember(3, Name = "battle_begin_time")]
	public uint BattleBeginTime
	{
		get
		{
			return __pbn__BattleBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleBeginTime = value;
		}
	}

	[ProtoMember(4, Name = "lobby_idx")]
	public uint LobbyIdx
	{
		get
		{
			return __pbn__LobbyIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__LobbyIdx = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleBeginTime()
	{
		return __pbn__BattleBeginTime.HasValue;
	}

	public void ResetBattleBeginTime()
	{
		__pbn__BattleBeginTime = null;
	}

	public bool ShouldSerializeLobbyIdx()
	{
		return __pbn__LobbyIdx.HasValue;
	}

	public void ResetLobbyIdx()
	{
		__pbn__LobbyIdx = null;
	}
}
