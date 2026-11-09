using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgTaleLevelUpAbilityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2383
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "TARGET_LEVEL_ERROR")]
		TargetLevelError,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "FLAG_LIMIT")]
		FlagLimit,
		[ProtoEnum(Name = "ABILITY_ERROR")]
		AbilityError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TaleId;

	private uint? __pbn__AbilityId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
		}
	}

	[ProtoMember(3, Name = "ability_id")]
	public uint AbilityId
	{
		get
		{
			return __pbn__AbilityId.GetValueOrDefault();
		}
		set
		{
			__pbn__AbilityId = value;
		}
	}

	[ProtoMember(4, Name = "limit_flag_list")]
	public List<RpgTaleFlag> LimitFlagLists { get; } = new List<RpgTaleFlag>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}

	public bool ShouldSerializeAbilityId()
	{
		return __pbn__AbilityId.HasValue;
	}

	public void ResetAbilityId()
	{
		__pbn__AbilityId = null;
	}
}
