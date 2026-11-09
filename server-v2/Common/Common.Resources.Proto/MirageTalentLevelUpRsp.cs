using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MirageTalentLevelUpRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7003
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "TALENT_NOT_EXIST")]
		TalentNotExist,
		[ProtoEnum(Name = "TALENT_LOCKED")]
		TalentLocked,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "LEVEL_LIMIT")]
		LevelLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TalentId;

	private uint? __pbn__CurLevel;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "talent_id")]
	public uint TalentId
	{
		get
		{
			return __pbn__TalentId.GetValueOrDefault();
		}
		set
		{
			__pbn__TalentId = value;
		}
	}

	[ProtoMember(3, Name = "cur_level")]
	public uint CurLevel
	{
		get
		{
			return __pbn__CurLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__CurLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTalentId()
	{
		return __pbn__TalentId.HasValue;
	}

	public void ResetTalentId()
	{
		__pbn__TalentId = null;
	}

	public bool ShouldSerializeCurLevel()
	{
		return __pbn__CurLevel.HasValue;
	}

	public void ResetCurLevel()
	{
		__pbn__CurLevel = null;
	}
}
