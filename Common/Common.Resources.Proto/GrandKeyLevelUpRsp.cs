using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GrandKeyLevelUpRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 754
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "WEAPON_LEVEL_LIMIT")]
		WeaponLevelLimit,
		[ProtoEnum(Name = "MATERIAL_LIMIT")]
		MaterialLimit,
		[ProtoEnum(Name = "MAX_LEVEL")]
		MaxLevel,
		[ProtoEnum(Name = "PLAYER_LEVEL_LIMIT")]
		PlayerLevelLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__KeyId;

	private uint? __pbn__Level;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "key_id")]
	public uint KeyId
	{
		get
		{
			return __pbn__KeyId.GetValueOrDefault();
		}
		set
		{
			__pbn__KeyId = value;
		}
	}

	[ProtoMember(3, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeKeyId()
	{
		return __pbn__KeyId.HasValue;
	}

	public void ResetKeyId()
	{
		__pbn__KeyId = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}
}
