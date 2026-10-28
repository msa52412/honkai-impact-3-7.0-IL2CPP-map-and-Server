using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LevelUpGodWarTalentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6173
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "TALENT_ERROR")]
		TalentError,
		[ProtoEnum(Name = "TARGET_LEVEL_ERROR")]
		TargetLevelError,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "FLAG_INVALID")]
		FlagInvalid,
		[ProtoEnum(Name = "FEATURE_CLOSE")]
		FeatureClose,
		[ProtoEnum(Name = "LEVEL_FULL")]
		LevelFull
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GodWarId;

	private uint? __pbn__MaxSupportPoint;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "god_war_id")]
	public uint GodWarId
	{
		get
		{
			return __pbn__GodWarId.GetValueOrDefault();
		}
		set
		{
			__pbn__GodWarId = value;
		}
	}

	[ProtoMember(3, Name = "talent")]
	public GodWarTalent Talent { get; set; }

	[ProtoMember(4, Name = "max_support_point")]
	public uint MaxSupportPoint
	{
		get
		{
			return __pbn__MaxSupportPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxSupportPoint = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGodWarId()
	{
		return __pbn__GodWarId.HasValue;
	}

	public void ResetGodWarId()
	{
		__pbn__GodWarId = null;
	}

	public bool ShouldSerializeMaxSupportPoint()
	{
		return __pbn__MaxSupportPoint.HasValue;
	}

	public void ResetMaxSupportPoint()
	{
		__pbn__MaxSupportPoint = null;
	}
}
