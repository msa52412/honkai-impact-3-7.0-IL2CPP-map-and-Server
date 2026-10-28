using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldWoodenBoyPlayer : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__Score;

	private bool? __pbn__IsCaught;

	private uint? __pbn__SpotId;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "score")]
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

	[ProtoMember(3, Name = "is_caught")]
	public bool IsCaught
	{
		get
		{
			return __pbn__IsCaught.GetValueOrDefault();
		}
		set
		{
			__pbn__IsCaught = value;
		}
	}

	[ProtoMember(4, Name = "spot_id")]
	public uint SpotId
	{
		get
		{
			return __pbn__SpotId.GetValueOrDefault();
		}
		set
		{
			__pbn__SpotId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeIsCaught()
	{
		return __pbn__IsCaught.HasValue;
	}

	public void ResetIsCaught()
	{
		__pbn__IsCaught = null;
	}

	public bool ShouldSerializeSpotId()
	{
		return __pbn__SpotId.HasValue;
	}

	public void ResetSpotId()
	{
		__pbn__SpotId = null;
	}
}
