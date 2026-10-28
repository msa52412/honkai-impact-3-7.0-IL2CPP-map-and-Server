using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeOpenworldKeyQuestRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4495
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_KEY_QUEST")]
		NotKeyQuest,
		[ProtoEnum(Name = "NOT_FINISH")]
		NotFinish,
		[ProtoEnum(Name = "MAP_NOT_FOUND")]
		MapNotFound
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Level;

	private uint? __pbn__Star;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(3, Name = "level")]
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

	[ProtoMember(4, Name = "star")]
	public uint Star
	{
		get
		{
			return __pbn__Star.GetValueOrDefault();
		}
		set
		{
			__pbn__Star = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeStar()
	{
		return __pbn__Star.HasValue;
	}

	public void ResetStar()
	{
		__pbn__Star = null;
	}
}
