using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterAntiGravityData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private bool? __pbn__IsUnlock;

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

	[ProtoMember(2, Name = "is_unlock")]
	public bool IsUnlock
	{
		get
		{
			return __pbn__IsUnlock.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUnlock = value;
		}
	}

	[ProtoMember(3, Name = "finish_coin_index_list")]
	public uint[] FinishCoinIndexLists { get; set; }

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

	public bool ShouldSerializeIsUnlock()
	{
		return __pbn__IsUnlock.HasValue;
	}

	public void ResetIsUnlock()
	{
		__pbn__IsUnlock = null;
	}
}
