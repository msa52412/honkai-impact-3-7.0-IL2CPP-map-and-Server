using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class IslandAvatarPos : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarClass;

	private uint? __pbn__PosType;

	[ProtoMember(1, Name = "avatar_class")]
	public uint AvatarClass
	{
		get
		{
			return __pbn__AvatarClass.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarClass = value;
		}
	}

	[ProtoMember(2, Name = "pos_type")]
	public uint PosType
	{
		get
		{
			return __pbn__PosType.GetValueOrDefault();
		}
		set
		{
			__pbn__PosType = value;
		}
	}

	[ProtoMember(3, Name = "event_id_list")]
	public uint[] EventIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarClass()
	{
		return __pbn__AvatarClass.HasValue;
	}

	public void ResetAvatarClass()
	{
		__pbn__AvatarClass = null;
	}

	public bool ShouldSerializePosType()
	{
		return __pbn__PosType.HasValue;
	}

	public void ResetPosType()
	{
		__pbn__PosType = null;
	}
}
