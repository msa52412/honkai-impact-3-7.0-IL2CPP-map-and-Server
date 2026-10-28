using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GobackFund : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private bool? __pbn__IsUnlocked;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "is_unlocked")]
	public bool IsUnlocked
	{
		get
		{
			return __pbn__IsUnlocked.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUnlocked = value;
		}
	}

	[ProtoMember(3, Name = "has_take_reward_progress_list")]
	public uint[] HasTakeRewardProgressLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeIsUnlocked()
	{
		return __pbn__IsUnlocked.HasValue;
	}

	public void ResetIsUnlocked()
	{
		__pbn__IsUnlocked = null;
	}
}
