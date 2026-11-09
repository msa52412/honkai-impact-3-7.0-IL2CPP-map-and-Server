using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalExploreInteractReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6905
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EntityId;

	[ProtoMember(1, Name = "pos")]
	public GlobalExplorePos Pos { get; set; }

	[ProtoMember(2, Name = "entity_id")]
	public uint EntityId
	{
		get
		{
			return __pbn__EntityId.GetValueOrDefault();
		}
		set
		{
			__pbn__EntityId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEntityId()
	{
		return __pbn__EntityId.HasValue;
	}

	public void ResetEntityId()
	{
		__pbn__EntityId = null;
	}
}
