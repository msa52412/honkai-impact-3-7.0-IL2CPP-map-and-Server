using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExtraStoryChapter : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

	private uint? __pbn__MinPlayerLevel;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private bool? __pbn__IsNeedKey;

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

	[ProtoMember(2, Name = "min_player_level")]
	public uint MinPlayerLevel
	{
		get
		{
			return __pbn__MinPlayerLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MinPlayerLevel = value;
		}
	}

	[ProtoMember(3, Name = "area_list")]
	public List<ExtraStoryArea> AreaLists { get; } = new List<ExtraStoryArea>();


	[ProtoMember(4, Name = "begin_time")]
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

	[ProtoMember(5, Name = "end_time")]
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

	[ProtoMember(6, Name = "is_need_key")]
	public bool IsNeedKey
	{
		get
		{
			return __pbn__IsNeedKey.GetValueOrDefault();
		}
		set
		{
			__pbn__IsNeedKey = value;
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

	public bool ShouldSerializeMinPlayerLevel()
	{
		return __pbn__MinPlayerLevel.HasValue;
	}

	public void ResetMinPlayerLevel()
	{
		__pbn__MinPlayerLevel = null;
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

	public bool ShouldSerializeIsNeedKey()
	{
		return __pbn__IsNeedKey.HasValue;
	}

	public void ResetIsNeedKey()
	{
		__pbn__IsNeedKey = null;
	}
}
