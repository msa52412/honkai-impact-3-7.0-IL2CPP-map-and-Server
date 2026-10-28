using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GrandKeyBreachRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 758
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "WEAPON_LIMIT")]
		WeaponLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__KeyId;

	private uint? __pbn__BreachLevel;

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

	[ProtoMember(3, Name = "breach_level")]
	public uint BreachLevel
	{
		get
		{
			return __pbn__BreachLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__BreachLevel = value;
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

	public bool ShouldSerializeBreachLevel()
	{
		return __pbn__BreachLevel.HasValue;
	}

	public void ResetBreachLevel()
	{
		__pbn__BreachLevel = null;
	}
}
