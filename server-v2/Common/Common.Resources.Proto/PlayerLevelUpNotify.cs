using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlayerLevelUpNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 976
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__OldLevel;

	private uint? __pbn__NewLevel;

	[ProtoMember(1, Name = "old_level")]
	public uint OldLevel
	{
		get
		{
			return __pbn__OldLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__OldLevel = value;
		}
	}

	[ProtoMember(2, Name = "new_level")]
	public uint NewLevel
	{
		get
		{
			return __pbn__NewLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__NewLevel = value;
		}
	}

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeOldLevel()
	{
		return __pbn__OldLevel.HasValue;
	}

	public void ResetOldLevel()
	{
		__pbn__OldLevel = null;
	}

	public bool ShouldSerializeNewLevel()
	{
		return __pbn__NewLevel.HasValue;
	}

	public void ResetNewLevel()
	{
		__pbn__NewLevel = null;
	}
}
