using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldStance : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StanceId;

	private uint? __pbn__HoldingStanceCampTag;

	private uint? __pbn__Camp1StanceProgress;

	private uint? __pbn__Camp2StanceProgress;

	[ProtoMember(1, Name = "stance_id")]
	public uint StanceId
	{
		get
		{
			return __pbn__StanceId.GetValueOrDefault();
		}
		set
		{
			__pbn__StanceId = value;
		}
	}

	[ProtoMember(2, Name = "position")]
	public ChatworldPosition Position { get; set; }

	[ProtoMember(3, Name = "holding_stance_camp_tag")]
	public uint HoldingStanceCampTag
	{
		get
		{
			return __pbn__HoldingStanceCampTag.GetValueOrDefault();
		}
		set
		{
			__pbn__HoldingStanceCampTag = value;
		}
	}

	[ProtoMember(4, Name = "camp_1_stance_progress")]
	public uint Camp1StanceProgress
	{
		get
		{
			return __pbn__Camp1StanceProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__Camp1StanceProgress = value;
		}
	}

	[ProtoMember(5, Name = "camp_2_stance_progress")]
	public uint Camp2StanceProgress
	{
		get
		{
			return __pbn__Camp2StanceProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__Camp2StanceProgress = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStanceId()
	{
		return __pbn__StanceId.HasValue;
	}

	public void ResetStanceId()
	{
		__pbn__StanceId = null;
	}

	public bool ShouldSerializeHoldingStanceCampTag()
	{
		return __pbn__HoldingStanceCampTag.HasValue;
	}

	public void ResetHoldingStanceCampTag()
	{
		__pbn__HoldingStanceCampTag = null;
	}

	public bool ShouldSerializeCamp1StanceProgress()
	{
		return __pbn__Camp1StanceProgress.HasValue;
	}

	public void ResetCamp1StanceProgress()
	{
		__pbn__Camp1StanceProgress = null;
	}

	public bool ShouldSerializeCamp2StanceProgress()
	{
		return __pbn__Camp2StanceProgress.HasValue;
	}

	public void ResetCamp2StanceProgress()
	{
		__pbn__Camp2StanceProgress = null;
	}
}
