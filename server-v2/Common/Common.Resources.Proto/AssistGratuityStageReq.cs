using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AssistGratuityStageReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4104
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StageUniqueId;

	private uint? __pbn__RelationTag;

	[ProtoMember(1, Name = "stage_unique_id")]
	public uint StageUniqueId
	{
		get
		{
			return __pbn__StageUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageUniqueId = value;
		}
	}

	[ProtoMember(2, Name = "player_info")]
	public GratuityPlayerInfo PlayerInfo { get; set; }

	[ProtoMember(3, Name = "relation_tag")]
	public uint RelationTag
	{
		get
		{
			return __pbn__RelationTag.GetValueOrDefault();
		}
		set
		{
			__pbn__RelationTag = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageUniqueId()
	{
		return __pbn__StageUniqueId.HasValue;
	}

	public void ResetStageUniqueId()
	{
		__pbn__StageUniqueId = null;
	}

	public bool ShouldSerializeRelationTag()
	{
		return __pbn__RelationTag.HasValue;
	}

	public void ResetRelationTag()
	{
		__pbn__RelationTag = null;
	}
}
