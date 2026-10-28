using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetSnsShowDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1267
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_PRIVILEDGE")]
		NoPriviledge
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SnsShowType;

	private uint? __pbn__TargetUid;

	private uint? __pbn__TargetUidLevel;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "sns_show_type")]
	public uint SnsShowType
	{
		get
		{
			return __pbn__SnsShowType.GetValueOrDefault();
		}
		set
		{
			__pbn__SnsShowType = value;
		}
	}

	[ProtoMember(3, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
		}
	}

	[ProtoMember(4, Name = "sns_show_endless_data")]
	public SnsShowEndlessData SnsShowEndlessData { get; set; }

	[ProtoMember(5, Name = "sns_show_ex_boss_data")]
	public SnsShowExBossData SnsShowExBossData { get; set; }

	[ProtoMember(6, Name = "wiki_detail_data_list")]
	public List<WikiDetailData> WikiDetailDataLists { get; } = new List<WikiDetailData>();


	[ProtoMember(7, Name = "card_data")]
	public PlayerCardData CardData { get; set; }

	[ProtoMember(8, Name = "player_data")]
	public PlayerDetailData PlayerData { get; set; }

	[ProtoMember(9, Name = "target_uid_level")]
	public uint TargetUidLevel
	{
		get
		{
			return __pbn__TargetUidLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUidLevel = value;
		}
	}

	[ProtoMember(10, Name = "sns_show_tower_raid_data")]
	public SnsShowTowerRaidData SnsShowTowerRaidData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSnsShowType()
	{
		return __pbn__SnsShowType.HasValue;
	}

	public void ResetSnsShowType()
	{
		__pbn__SnsShowType = null;
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}

	public bool ShouldSerializeTargetUidLevel()
	{
		return __pbn__TargetUidLevel.HasValue;
	}

	public void ResetTargetUidLevel()
	{
		__pbn__TargetUidLevel = null;
	}
}
