using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KingdomWarOccupyRecord : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BeginTime;

	private uint? __pbn__PlayerScore;

	[ProtoMember(1, Name = "begin_time")]
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

	[ProtoMember(2, Name = "occupy_point_list")]
	public uint[] OccupyPointLists { get; set; }

	[ProtoMember(3, Name = "player_score")]
	public uint PlayerScore
	{
		get
		{
			return __pbn__PlayerScore.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializePlayerScore()
	{
		return __pbn__PlayerScore.HasValue;
	}

	public void ResetPlayerScore()
	{
		__pbn__PlayerScore = null;
	}
}
