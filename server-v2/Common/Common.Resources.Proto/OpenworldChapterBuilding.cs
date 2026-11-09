using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterBuilding : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MainId;

	private uint? __pbn__Level;

	private uint? __pbn__LevelUpFinishTime;

	[ProtoMember(1, Name = "main_id")]
	public uint MainId
	{
		get
		{
			return __pbn__MainId.GetValueOrDefault();
		}
		set
		{
			__pbn__MainId = value;
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

	[ProtoMember(3, Name = "level_up_finish_time")]
	public uint LevelUpFinishTime
	{
		get
		{
			return __pbn__LevelUpFinishTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LevelUpFinishTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMainId()
	{
		return __pbn__MainId.HasValue;
	}

	public void ResetMainId()
	{
		__pbn__MainId = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeLevelUpFinishTime()
	{
		return __pbn__LevelUpFinishTime.HasValue;
	}

	public void ResetLevelUpFinishTime()
	{
		__pbn__LevelUpFinishTime = null;
	}
}
