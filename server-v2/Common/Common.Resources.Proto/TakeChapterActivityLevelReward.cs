using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeChapterActivityLevelRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4202
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

	private uint? __pbn__Level;

	private bool? __pbn__IsTakeAll;

	[ProtoMember(1, Name = "chapter_id")]
	public uint ChapterId
	{
		get
		{
			return __pbn__ChapterId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChapterId = value;
		}
	}

	[ProtoMember(2, Name = "level")]
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

	[ProtoMember(3, Name = "is_take_all")]
	public bool IsTakeAll
	{
		get
		{
			return __pbn__IsTakeAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeAll = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChapterId()
	{
		return __pbn__ChapterId.HasValue;
	}

	public void ResetChapterId()
	{
		__pbn__ChapterId = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeIsTakeAll()
	{
		return __pbn__IsTakeAll.HasValue;
	}

	public void ResetIsTakeAll()
	{
		__pbn__IsTakeAll = null;
	}
}
[ProtoContract]
public class TakeChapterActivityLevelRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4203
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LACK_LEVEL")]
		LackLevel,
		[ProtoEnum(Name = "HAVE_TAKE")]
		HaveTake,
		[ProtoEnum(Name = "NO_REWARD")]
		NoReward
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

	private bool? __pbn__IsTakeAll;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chapter_id")]
	public uint ChapterId
	{
		get
		{
			return __pbn__ChapterId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChapterId = value;
		}
	}

	[ProtoMember(3, Name = "level_list")]
	public uint[] LevelLists { get; set; }

	[ProtoMember(4, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(5, Name = "is_take_all")]
	public bool IsTakeAll
	{
		get
		{
			return __pbn__IsTakeAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeAll = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChapterId()
	{
		return __pbn__ChapterId.HasValue;
	}

	public void ResetChapterId()
	{
		__pbn__ChapterId = null;
	}

	public bool ShouldSerializeIsTakeAll()
	{
		return __pbn__IsTakeAll.HasValue;
	}

	public void ResetIsTakeAll()
	{
		__pbn__IsTakeAll = null;
	}
}
