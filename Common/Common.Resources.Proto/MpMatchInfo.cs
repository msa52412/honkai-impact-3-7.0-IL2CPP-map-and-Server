using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpMatchInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__StartMatchTime;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "start_match_time")]
	public uint StartMatchTime
	{
		get
		{
			return __pbn__StartMatchTime.GetValueOrDefault();
		}
		set
		{
			__pbn__StartMatchTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeStartMatchTime()
	{
		return __pbn__StartMatchTime.HasValue;
	}

	public void ResetStartMatchTime()
	{
		__pbn__StartMatchTime = null;
	}
}
