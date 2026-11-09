using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RogueTowerSaveReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4372
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__Index;

	private uint? __pbn__StagePassCount;

	private uint? __pbn__Score;

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

	[ProtoMember(2, Name = "index")]
	public uint Index
	{
		get
		{
			return __pbn__Index.GetValueOrDefault();
		}
		set
		{
			__pbn__Index = value;
		}
	}

	[ProtoMember(3, Name = "stage_pass_count")]
	public uint StagePassCount
	{
		get
		{
			return __pbn__StagePassCount.GetValueOrDefault();
		}
		set
		{
			__pbn__StagePassCount = value;
		}
	}

	[ProtoMember(4, Name = "score")]
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

	public bool ShouldSerializeIndex()
	{
		return __pbn__Index.HasValue;
	}

	public void ResetIndex()
	{
		__pbn__Index = null;
	}

	public bool ShouldSerializeStagePassCount()
	{
		return __pbn__StagePassCount.HasValue;
	}

	public void ResetStagePassCount()
	{
		__pbn__StagePassCount = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}
}
