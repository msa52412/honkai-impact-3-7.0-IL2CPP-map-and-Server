using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalExploreInteractRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6906
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "OUT_OF_RANGE")]
		OutOfRange,
		[ProtoEnum(Name = "ENTITY_NOT_EXIST")]
		EntityNotExist,
		[ProtoEnum(Name = "ENTITY_MISMATCH")]
		EntityMismatch,
		[ProtoEnum(Name = "STAGE_UNFINISH")]
		StageUnfinish
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EntityId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "pos")]
	public GlobalExplorePos Pos { get; set; }

	[ProtoMember(3, Name = "entity_id")]
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

	[ProtoMember(4, Name = "action_id_list")]
	public uint[] ActionIdLists { get; set; }

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
