using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DLCAvatarReviveRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3570
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "TIMES_LACK")]
		TimesLack,
		[ProtoEnum(Name = "NOT_IN_STAGE")]
		NotInStage,
		[ProtoEnum(Name = "MAP_ERROR")]
		MapError,
		[ProtoEnum(Name = "AVATAR_ERROR")]
		AvatarError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ReviveTimes;

	private DLCAvatarReviveCostType? __pbn__CostType;

	private uint? __pbn__MapId;

	private uint? __pbn__AvatarId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "revive_times")]
	public uint ReviveTimes
	{
		get
		{
			return __pbn__ReviveTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ReviveTimes = value;
		}
	}

	[ProtoMember(3, Name = "cost_type")]
	[DefaultValue(DLCAvatarReviveCostType.DlcAvatarReviveCostTypeNone)]
	public DLCAvatarReviveCostType CostType
	{
		get
		{
			return __pbn__CostType.GetValueOrDefault();
		}
		set
		{
			__pbn__CostType = value;
		}
	}

	[ProtoMember(4, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(5, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReviveTimes()
	{
		return __pbn__ReviveTimes.HasValue;
	}

	public void ResetReviveTimes()
	{
		__pbn__ReviveTimes = null;
	}

	public bool ShouldSerializeCostType()
	{
		return __pbn__CostType.HasValue;
	}

	public void ResetCostType()
	{
		__pbn__CostType = null;
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}
}
