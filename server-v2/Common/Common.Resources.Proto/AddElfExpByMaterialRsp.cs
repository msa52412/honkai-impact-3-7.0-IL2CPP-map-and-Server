using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AddElfExpByMaterialRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2108
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
		[ProtoEnum(Name = "ELF_LEVEL_FULL")]
		ElfLevelFull,
		[ProtoEnum(Name = "MATERIAL_NOT_ENOUGH")]
		MaterialNotEnough,
		[ProtoEnum(Name = "MATERIAL_CAN_NOT_ADD_EXP")]
		MaterialCanNotAddExp,
		[ProtoEnum(Name = "SCOIN_LACK")]
		ScoinLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__OldLevel;

	private uint? __pbn__OldExp;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "old_level")]
	public uint OldLevel
	{
		get
		{
			return __pbn__OldLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__OldLevel = value;
		}
	}

	[ProtoMember(3, Name = "old_exp")]
	public uint OldExp
	{
		get
		{
			return __pbn__OldExp.GetValueOrDefault();
		}
		set
		{
			__pbn__OldExp = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeOldLevel()
	{
		return __pbn__OldLevel.HasValue;
	}

	public void ResetOldLevel()
	{
		__pbn__OldLevel = null;
	}

	public bool ShouldSerializeOldExp()
	{
		return __pbn__OldExp.HasValue;
	}

	public void ResetOldExp()
	{
		__pbn__OldExp = null;
	}
}
