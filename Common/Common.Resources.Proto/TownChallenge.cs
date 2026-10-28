using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownChallenge : IExtensible
{
	[ProtoContract]
	public enum Status
	{
		[ProtoEnum(Name = "NONE")]
		None,
		[ProtoEnum(Name = "DOING")]
		Doing,
		[ProtoEnum(Name = "FINISH")]
		Finish,
		[ProtoEnum(Name = "CLOSE")]
		Close
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChallengeId;

	private Status? __pbn__status;

	private uint? __pbn__Value;

	private uint? __pbn__TowerId;

	[ProtoMember(1, Name = "challenge_id")]
	public uint ChallengeId
	{
		get
		{
			return __pbn__ChallengeId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChallengeId = value;
		}
	}

	[ProtoMember(2)]
	[DefaultValue(Status.None)]
	public Status status
	{
		get
		{
			return __pbn__status.GetValueOrDefault();
		}
		set
		{
			__pbn__status = value;
		}
	}

	[ProtoMember(3, Name = "value")]
	public uint Value
	{
		get
		{
			return __pbn__Value.GetValueOrDefault();
		}
		set
		{
			__pbn__Value = value;
		}
	}

	[ProtoMember(4, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChallengeId()
	{
		return __pbn__ChallengeId.HasValue;
	}

	public void ResetChallengeId()
	{
		__pbn__ChallengeId = null;
	}

	public bool ShouldSerializestatus()
	{
		return __pbn__status.HasValue;
	}

	public void Resetstatus()
	{
		__pbn__status = null;
	}

	public bool ShouldSerializeValue()
	{
		return __pbn__Value.HasValue;
	}

	public void ResetValue()
	{
		__pbn__Value = null;
	}

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}
}
