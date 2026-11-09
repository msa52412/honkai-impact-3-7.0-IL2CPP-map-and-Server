using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatchThreeRoleLevelUpRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7604
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ROLE_NOT_FOUND")]
		RoleNotFound,
		[ProtoEnum(Name = "TARGET_LEVEL_ERROR")]
		TargetLevelError,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "role")]
	public MatchThreeRole Role { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}
}
