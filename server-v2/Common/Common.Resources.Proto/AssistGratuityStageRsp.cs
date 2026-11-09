using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AssistGratuityStageRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4105
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "STAGE_EXPIRED")]
		StageExpired,
		[ProtoEnum(Name = "STAGE_FINISH")]
		StageFinish,
		[ProtoEnum(Name = "HAVE_ASSIST")]
		HaveAssist,
		[ProtoEnum(Name = "PLAYER_FULL")]
		PlayerFull,
		[ProtoEnum(Name = "ASSIST_FULL")]
		AssistFull,
		[ProtoEnum(Name = "IS_SELF")]
		IsSelf,
		[ProtoEnum(Name = "ASSIST_STAMINA_LACK")]
		AssistStaminaLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RelationTag;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "stage")]
	public GratuityStage Stage { get; set; }

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

	public bool ShouldSerializeRelationTag()
	{
		return __pbn__RelationTag.HasValue;
	}

	public void ResetRelationTag()
	{
		__pbn__RelationTag = null;
	}
}
