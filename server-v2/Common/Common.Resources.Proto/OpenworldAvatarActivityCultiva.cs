using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldAvatarActivityCultivateReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4558
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__CultivateId;

	[ProtoMember(1, Name = "map_id")]
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

	[ProtoMember(2, Name = "cultivate_id")]
	public uint CultivateId
	{
		get
		{
			return __pbn__CultivateId.GetValueOrDefault();
		}
		set
		{
			__pbn__CultivateId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeCultivateId()
	{
		return __pbn__CultivateId.HasValue;
	}

	public void ResetCultivateId()
	{
		__pbn__CultivateId = null;
	}
}
[ProtoContract]
public class OpenworldAvatarActivityCultivateRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4559
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
		[ProtoEnum(Name = "CONDITION_NOT_MEET")]
		ConditionNotMeet,
		[ProtoEnum(Name = "MAX_LEVEL")]
		MaxLevel
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
[ProtoContract]
public enum OpenworldAvatarActivityCultivateType
{
	[ProtoEnum(Name = "OPENWORLD_AVATAR_ACTIVITY_CULTIVATE_AVATAR")]
	OpenworldAvatarActivityCultivateAvatar = 1,
	[ProtoEnum(Name = "OPENWORLD_AVATAR_ACTIVITY_CULTIVATE_STIGMATA")]
	OpenworldAvatarActivityCultivateStigmata,
	[ProtoEnum(Name = "OPENWORLD_AVATAR_ACTIVITY_CULTIVATE_WEAPON")]
	OpenworldAvatarActivityCultivateWeapon
}
