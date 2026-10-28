using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalExploreActionRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6908
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
		[ProtoEnum(Name = "ACTION_INVALID")]
		ActionInvalid,
		[ProtoEnum(Name = "STAMINA_LACK")]
		StaminaLack,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "STAGE_UNFINISH")]
		StageUnfinish,
		[ProtoEnum(Name = "ENTITY_HAS_ACTIVATE")]
		EntityHasActivate
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EntityId;

	private uint? __pbn__ActionId;

	private uint? __pbn__ActionParam;

	private uint? __pbn__ContributionAdd;

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

	[ProtoMember(4, Name = "action_id")]
	public uint ActionId
	{
		get
		{
			return __pbn__ActionId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActionId = value;
		}
	}

	[ProtoMember(5, Name = "action_param")]
	public uint ActionParam
	{
		get
		{
			return __pbn__ActionParam.GetValueOrDefault();
		}
		set
		{
			__pbn__ActionParam = value;
		}
	}

	[ProtoMember(6, Name = "contribution_add")]
	public uint ContributionAdd
	{
		get
		{
			return __pbn__ContributionAdd.GetValueOrDefault();
		}
		set
		{
			__pbn__ContributionAdd = value;
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

	public bool ShouldSerializeActionId()
	{
		return __pbn__ActionId.HasValue;
	}

	public void ResetActionId()
	{
		__pbn__ActionId = null;
	}

	public bool ShouldSerializeActionParam()
	{
		return __pbn__ActionParam.HasValue;
	}

	public void ResetActionParam()
	{
		__pbn__ActionParam = null;
	}

	public bool ShouldSerializeContributionAdd()
	{
		return __pbn__ContributionAdd.HasValue;
	}

	public void ResetContributionAdd()
	{
		__pbn__ContributionAdd = null;
	}
}
