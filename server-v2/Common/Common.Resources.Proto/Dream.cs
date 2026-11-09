using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Dream : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__DreamId;

	private uint? __pbn__UnlockId;

	private bool? __pbn__IsOpen;

	private uint? __pbn__SelectedDreamRewardId;

	private uint? __pbn__Score;

	private bool? __pbn__IsRewardTaken;

	private uint? __pbn__ExchangeScoreTimes;

	[ProtoMember(1, Name = "dream_id")]
	public uint DreamId
	{
		get
		{
			return __pbn__DreamId.GetValueOrDefault();
		}
		set
		{
			__pbn__DreamId = value;
		}
	}

	[ProtoMember(2, Name = "unlock_id")]
	public uint UnlockId
	{
		get
		{
			return __pbn__UnlockId.GetValueOrDefault();
		}
		set
		{
			__pbn__UnlockId = value;
		}
	}

	[ProtoMember(3, Name = "is_open")]
	public bool IsOpen
	{
		get
		{
			return __pbn__IsOpen.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOpen = value;
		}
	}

	[ProtoMember(4, Name = "selected_dream_reward_id")]
	public uint SelectedDreamRewardId
	{
		get
		{
			return __pbn__SelectedDreamRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__SelectedDreamRewardId = value;
		}
	}

	[ProtoMember(5, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(6, Name = "is_reward_taken")]
	public bool IsRewardTaken
	{
		get
		{
			return __pbn__IsRewardTaken.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRewardTaken = value;
		}
	}

	[ProtoMember(7, Name = "exchange_score_times")]
	public uint ExchangeScoreTimes
	{
		get
		{
			return __pbn__ExchangeScoreTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ExchangeScoreTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDreamId()
	{
		return __pbn__DreamId.HasValue;
	}

	public void ResetDreamId()
	{
		__pbn__DreamId = null;
	}

	public bool ShouldSerializeUnlockId()
	{
		return __pbn__UnlockId.HasValue;
	}

	public void ResetUnlockId()
	{
		__pbn__UnlockId = null;
	}

	public bool ShouldSerializeIsOpen()
	{
		return __pbn__IsOpen.HasValue;
	}

	public void ResetIsOpen()
	{
		__pbn__IsOpen = null;
	}

	public bool ShouldSerializeSelectedDreamRewardId()
	{
		return __pbn__SelectedDreamRewardId.HasValue;
	}

	public void ResetSelectedDreamRewardId()
	{
		__pbn__SelectedDreamRewardId = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeIsRewardTaken()
	{
		return __pbn__IsRewardTaken.HasValue;
	}

	public void ResetIsRewardTaken()
	{
		__pbn__IsRewardTaken = null;
	}

	public bool ShouldSerializeExchangeScoreTimes()
	{
		return __pbn__ExchangeScoreTimes.HasValue;
	}

	public void ResetExchangeScoreTimes()
	{
		__pbn__ExchangeScoreTimes = null;
	}
}
