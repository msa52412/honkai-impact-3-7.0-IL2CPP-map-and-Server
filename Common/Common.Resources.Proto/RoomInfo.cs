using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RoomInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SubType;

	private uint? __pbn__StageId;

	[ProtoMember(1, Name = "sub_type")]
	public uint SubType
	{
		get
		{
			return __pbn__SubType.GetValueOrDefault();
		}
		set
		{
			__pbn__SubType = value;
		}
	}

	[ProtoMember(2, Name = "stage_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSubType()
	{
		return __pbn__SubType.HasValue;
	}

	public void ResetSubType()
	{
		__pbn__SubType = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}
}
