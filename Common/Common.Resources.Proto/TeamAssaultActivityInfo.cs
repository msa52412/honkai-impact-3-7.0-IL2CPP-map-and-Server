using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TeamAssaultActivityInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EnterTimes;

	private uint? __pbn__BossIdx;

	[ProtoMember(1, Name = "enter_times")]
	public uint EnterTimes
	{
		get
		{
			return __pbn__EnterTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__EnterTimes = value;
		}
	}

	[ProtoMember(2, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	[ProtoMember(3, Name = "score_list")]
	public uint[] ScoreLists { get; set; }

	[ProtoMember(4, Name = "boss_idx")]
	public uint BossIdx
	{
		get
		{
			return __pbn__BossIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__BossIdx = value;
		}
	}

	[ProtoMember(5, Name = "pass_time_list")]
	public uint[] PassTimeLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEnterTimes()
	{
		return __pbn__EnterTimes.HasValue;
	}

	public void ResetEnterTimes()
	{
		__pbn__EnterTimes = null;
	}

	public bool ShouldSerializeBossIdx()
	{
		return __pbn__BossIdx.HasValue;
	}

	public void ResetBossIdx()
	{
		__pbn__BossIdx = null;
	}
}
