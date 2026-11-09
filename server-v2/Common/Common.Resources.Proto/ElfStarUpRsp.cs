using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ElfStarUpRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2106
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ELF_NOT_EXIST")]
		ElfNotExist,
		[ProtoEnum(Name = "FRAGMENT_LACK")]
		FragmentLack,
		[ProtoEnum(Name = "STAR_FULL")]
		StarFull,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ElfId;

	private bool? __pbn__IsUnlock;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "elf_id")]
	public uint ElfId
	{
		get
		{
			return __pbn__ElfId.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfId = value;
		}
	}

	[ProtoMember(3, Name = "is_unlock")]
	public bool IsUnlock
	{
		get
		{
			return __pbn__IsUnlock.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUnlock = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeElfId()
	{
		return __pbn__ElfId.HasValue;
	}

	public void ResetElfId()
	{
		__pbn__ElfId = null;
	}

	public bool ShouldSerializeIsUnlock()
	{
		return __pbn__IsUnlock.HasValue;
	}

	public void ResetIsUnlock()
	{
		__pbn__IsUnlock = null;
	}
}
