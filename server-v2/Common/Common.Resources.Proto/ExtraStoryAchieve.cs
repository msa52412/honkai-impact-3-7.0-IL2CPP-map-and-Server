using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExtraStoryAchieve : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AchieveId;

	private bool? __pbn__IsTake;

	[ProtoMember(1, Name = "achieve_id")]
	public uint AchieveId
	{
		get
		{
			return __pbn__AchieveId.GetValueOrDefault();
		}
		set
		{
			__pbn__AchieveId = value;
		}
	}

	[ProtoMember(2, Name = "is_take")]
	public bool IsTake
	{
		get
		{
			return __pbn__IsTake.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTake = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAchieveId()
	{
		return __pbn__AchieveId.HasValue;
	}

	public void ResetAchieveId()
	{
		__pbn__AchieveId = null;
	}

	public bool ShouldSerializeIsTake()
	{
		return __pbn__IsTake.HasValue;
	}

	public void ResetIsTake()
	{
		__pbn__IsTake = null;
	}
}
